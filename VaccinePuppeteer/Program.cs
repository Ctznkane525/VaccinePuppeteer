using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace VaccinePuppeteer
{
    class Program
    {
        static async Task Main(string[] args)
        {

            // Get Application Settings
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
                IConfiguration configuration = builder.Build();
                AppSettings a = new AppSettings();
                ConfigurationBinder.Bind(configuration, a);

            while (true)
            {
                try
                {

                    await RiteAid(a);

                    // Executing CVS
                    await Cvs(a);

                    // Executing Walgreens
                    await Walgreens(a);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine(e);
                }

                // Delaying Till Next Evaluation
                await Task.Delay(60 * 1000 * a.RefreshRate);
            }

        }

        private static async Task RiteAid(AppSettings a)
        {
            if (a.RiteAid.Enabled)
            {
                using (PharmacyBase p = new PharmacyRiteAid(a.RiteAid))
                {
                    await p.ExecuteAsync();
                }
            }          
        }

        private static async Task Walgreens(AppSettings a)
        {
            if (a.Walgreens.Enabled)
            {
                using (PharmacyBase p = new PharmacyWalgreens(a.Walgreens.Input))
                {
                    await p.ExecuteAsync();
                }
            }
            
        }

        private static async Task Cvs(AppSettings a)
        {
            if (a.Cvs.Enabled)
            {
                using (PharmacyBase p = new PharmacyCvs(a.Cvs.Input))
                {
                    await p.ExecuteAsync();
                }
            }
            
        }
    }
}
