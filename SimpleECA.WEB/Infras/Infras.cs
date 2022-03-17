using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using SimpleECA.Entities;
using SimpleECA.Helpers;
using SimpleECA.Repos;
using SimpleECA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleECA.WEB
{
    public class Infras
    {
        public static void AddDatabase(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SimpleECADbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("SqlConnection"));
            });
        }
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc()
                    .AddNewtonsoftJson(options =>
                           options.SerializerSettings.ContractResolver =
                              new CamelCasePropertyNamesContractResolver());

            services.AddSingleton<AppSettingsHelper, AppSettingsHelper>();
            var secret = new Secret();
            configuration.Bind("Secret", secret);
            services.AddSingleton(secret);

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IAuthRepo, AuthRepo>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepo, UserRepo>();
        }
    }
}
