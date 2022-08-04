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
                //ortam de�i�kenleri
                //ASPNETCORE_ENVIRONMENT ad�nda bir de�i�len var.siz uygulamay� �al��t�r�rken bunun set ederseniz e�er ben bu de�i�kenin de�erini al�cam. 
                string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

                return new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())//configurasyon dosyas�n�n hangi pathte oldu�unu s�yleyece�iz
                    .AddJsonFile("appsettings.json", optional: false)//sen bu dosya i�indeki ayarlar� oku ve bu ayarlar� �configuration dan .netin kendisine bak
                    .AddJsonFile($"appsettings.{env}.json", optional: true)//�ncelikli olarak buna bak
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
                .WriteTo.File("Logs.txt")//file'a aktar�yor
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
