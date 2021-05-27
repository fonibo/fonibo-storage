using Amazon;
using Amazon.S3;
using Fonibo.Storage;
using Fonibo.Storage.S3;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddS3Client(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var accessKey = configuration["AccessKey"];
            var secretKey = configuration["SecretKey"];
            var region = configuration["Region"];

            return services
                .AddScoped<IStorageManager, S3StorageManager>()
                .AddTransient<IAmazonS3, AmazonS3Client>(sp => new AmazonS3Client(
                    accessKey,
                    secretKey,
                    RegionEndpoint.GetBySystemName(region)));
        }
    }
}
