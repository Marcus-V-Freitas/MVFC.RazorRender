var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("cache");

builder.AddProject<Projects.MVFC_RazorRender_Playground_Api>("razor-render-api")
       .WaitFor(redis)
       .WithReference(redis);

await builder.Build().RunAsync();
