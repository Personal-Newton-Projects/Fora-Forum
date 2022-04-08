global using Fora.Shared;
global using Fora.Client.Managers;
global using System.Net.Http.Json;
global using Fora.Client.Services;
using Fora.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IUserManager, UserManager>()
    .AddScoped<ILoginManager, LoginManager>()
    .AddScoped<IInterestManager, InterestManager>()
    .AddScoped<IThreadManager, ThreadManager>();

builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
