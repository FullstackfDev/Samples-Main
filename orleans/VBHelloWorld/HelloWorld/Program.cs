using System;
using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Hosting;
using VBGrains;

[assembly: GenerateCodeForDeclaringAssembly(typeof(HelloGrain))]
[assembly: GenerateCodeForDeclaringAssembly(typeof(IHelloGrain))]

using var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
    })
    .UseConsoleLifetime()
    .Build();

await host.StartAsync();

var grainFactory = host.Services.GetRequiredService<IGrainFactory>();
var friend = grainFactory.GetGrain<IHelloGrain>(0);
Console.WriteLine("\n\n{0}\n\n", await friend.SayHello("Good morning!"));

Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");
