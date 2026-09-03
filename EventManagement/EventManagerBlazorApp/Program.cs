using EventManagerBlazorApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/* Step 6.1:Start:Configure HttpClient in the Blazor application to connect Web API (EventManagementWebAPI)
* hosted in the same solution. The base address of the HttpClient should be set to the URL
* of the Web API project (e.g., https://localhost:4000).
*/
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:4000") });

await builder.Build().RunAsync();
