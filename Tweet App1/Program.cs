using Microsoft.Extensions.DependencyInjection;
using System;
using com.tweetapp.DALClasses;
using com.tweetapp.ServiceLayer;

namespace com.tweetapp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();
            Configure(services);
            IServiceProvider provider = services.BuildServiceProvider();
            var service = provider.GetService<ITweetAppService>();
            service.MenuNonLoggedUser();
        }

        private static void Configure(IServiceCollection service)
        {
            service.AddSingleton<ITweetAppRepository, TweetAppRepository>();
            service.AddScoped<ITweetAppService, TweetAppService>();
        }
    }
}
