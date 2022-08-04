using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BP.Api
{
    public class Program
    {
        #region 2-)Configurations
        private static IConfiguration Configuration
        {
            get
            {
                //ortam deðiþkenleri
                //ASPNETCORE_ENVIRONMENT adýnda bir deðiþlen var.siz uygulamayý çalýþtýrýrken bunun set ederseniz eðer ben bu deðiþkenin deðerini alýcam. 
                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())//configurasyon dosyasýnýn hangi pathte olduðunu söyleyeceðiz
                    .AddJsonFile("appsettings.json", optional: false)//sen bu dosya içindeki ayarlarý oku ve bu ayarlarý ýconfiguration dan .netin kendisine bak
                    .AddJsonFile($"appsettings.{env}.json", optional: true)//öncelikli olarak buna bak
                    .AddEnvironmentVariables()
                    .Build();
            }
        }
        #endregion

        public static void Main(string[] args)
        {
            #region Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Debug(Serilog.Events.LogEventLevel.Information)
                .WriteTo.File("Logs.txt")//file'a aktarýyor
                                         //.WriteTo.Seq()
                .CreateLogger();
            #endregion
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    #region 2-)Configurations
                    webBuilder.UseConfiguration(Configuration);
                    #endregion
                    webBuilder.UseStartup<Startup>();
                });
    }
}
