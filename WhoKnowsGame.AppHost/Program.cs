var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);

var sqlServer = builder.AddSqlServer("whoknowssql", password)
    .WithDataVolume()
    .AddDatabase("whoknowsdatabase");

builder.AddProject<Projects.WhoKnowsGame>("whoknowsgame")
    .WithReference(sqlServer)
    .WaitFor(sqlServer);

builder.Build().Run();
