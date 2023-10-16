using Microsoft.Extensions.DependencyInjection;
using Proxy.Line.Core;
using Proxy.Line.Core.Interfaces;
using Proxy.Line.Settings;
using Proxy.Line.V1;

namespace Proxy.Line
{
    public static class ServiceExtensions
    {
        public static void RegisterLineService(this IServiceCollection services, LineSetting lineSetting)
        {
            if (lineSetting is null)
                throw new ArgumentNullException(nameof(lineSetting));

            services.AddSingleton(lineSetting);

            services.AddHttpClient<LineClient>(x =>
            {
                x.BaseAddress = new Uri(lineSetting.KongUrl);
            });

            services.AddTransient<ILineClient, LineClient>();
            services.AddTransient<ILineProxy, LineProxy>();
        }
    }
}
