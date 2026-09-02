using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.Run();

var psi = new ProcessStartInfo("EventsList.html") { UseShellExecute = true };
System.Diagnostics.Process.Start(psi);
