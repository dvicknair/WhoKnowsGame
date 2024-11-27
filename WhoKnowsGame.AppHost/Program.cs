var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("whoknowssql")
    .WithDataVolume()
    .AddDatabase("whoknowsdatabase");

builder.AddProject<Projects.WhoKnowsGame>("whoknowsgame")
    .WithReference(sqlServer)
    .WaitFor(sqlServer);

builder.Build().Run();
