using APBDProject.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APBDProject.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("APBDProject.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("APBDProject.ServerAPI"));
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<IStockDetailsService, StockDetailsService>();
            builder.Services.AddScoped<IPriceDetailsService, PriceDetailsService>();
            builder.Services.AddScoped<IArticleDetailsService, ArticleDetailsService>();
            builder.Services.AddScoped<IWatchlistDetailsService, WatchlistDetailsService>();
            builder.Services.AddApiAuthorization();
            builder.Services.AddSyncfusionBlazor();

            await builder.Build().RunAsync();
        }
    }
}
