using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace testDotNetCoreAdApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    IConfigurationRoot configRoot = config.Build();

                    string keyVaultUri = $"https://{configRoot["KeyVault:Name"]}.vault.azure.net/";

                    if (!context.HostingEnvironment.IsDevelopment())
                    {
                        AzureServiceTokenProvider tokenProvider = new AzureServiceTokenProvider();

                        KeyVaultClient keyVaultClient = new KeyVaultClient(
                                                            new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));

                        config.AddAzureKeyVault(keyVaultUri,
                                                keyVaultClient,
                                                new DefaultKeyVaultSecretManager());
                    }
                })
                .UseStartup<Startup>();
    }
}