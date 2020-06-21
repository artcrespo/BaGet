using System;
using BaGet.Gcp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BaGet.Core
{
    public static class GoogleCloudApplicationExtensions
    {
        public static BaGetApplication AddGoogleCloudStorage(this BaGetApplication app)
        {
            app.Services.AddBaGetOptions<GoogleCloudStorageOptions>(nameof(BaGetOptions.Storage));
            app.Services.AddTransient<GoogleCloudStorageService>();

            app.Services.TryAddTransient<IStorageService>(provider => provider.GetRequiredService<GoogleCloudStorageService>());

            return app;
        }

        public static BaGetApplication AddMySqlDatabase(
            this BaGetApplication app,
            Action<GoogleCloudStorageOptions> configure)
        {
            app.AddGoogleCloudStorage();
            app.Services.Configure(configure);
            return app;
        }
    }
}
