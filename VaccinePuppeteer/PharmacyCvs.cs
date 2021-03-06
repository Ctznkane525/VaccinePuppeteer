using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VaccinePuppeteer
{
    public class PharmacyCvs : PharmacyBase
    {

        public PharmacyCvs(AppSettingsCvs cvsSettings)
        {
            CvsSettings = cvsSettings;
        }

        public AppSettingsCvs CvsSettings { get; set; }

        public override async Task ExecuteAsync()
        {
            Console.WriteLine("Beginning Cvs.ExecuteAsync");

            var browser = await this.GetBrowserAsync();
            Page page = await browser.NewPageAsync();

            // CVS Page Bug
            await page.GoToAsync("https://www.vaxxmax.com/cvs");
            await Task.Delay(2000);
            await page.GoToAsync("https://www.vaxxmax.com/cvs");
            await Task.Delay(2000);


            //await page.TypeAsync("#zipcode", this.CvsSettings.Zip); - No Zip on CVS Page
            await page.SelectAsync("#state-select-cvs", new string[] { this.CvsSettings.State });
            await Task.Delay(5000);

            var rows = await page.QuerySelectorAllAsync(".dataTable tr");
            foreach (var row in rows)
            {
                var cells = await row.QuerySelectorAllAsync("td");
                if (cells.Length >= 8)
                {
                    var name = Convert.ToString((await (await cells[1].GetPropertyAsync("innerText")).JsonValueAsync()).ToString());
                    var distance = Convert.ToInt32((await (await cells[7].GetPropertyAsync("innerText")).JsonValueAsync()).ToString());
                    Console.WriteLine($"Name {name}, Distance {distance}");
                    if (distance <= 30)
                    {
                        await AlertAsync("Cvs");
                    }
                }
            }

            /*
            var browser = await this.GetBrowserAsync();
            Page page = await browser.NewPageAsync();

            
            var url = "https://www.cvs.com/vaccine/intake/store/cvd-schedule?icid=coronavirus-lp-vaccine-nj-statetool";
            await page.GoToAsync(url);

            await page.WaitForSelectorAsync($"h1");
            var h1 = await page.QuerySelectorAsync("h1");
            var h1Text = await (await h1.GetPropertyAsync("innerText")).JsonValueAsync();
            if (!h1Text.ToString().Contains("Please check back later."))
            {
                await Alert();
            }
            


            
            await page.GoToAsync("https://www.cvs.com/immunizations/covid-19-vaccine");
            await page.WaitForSelectorAsync($"a[data-modal='vaccineinfo-{Input}']");
            await page.ClickAsync($"a[data-modal='vaccineinfo-{Input}']");
            await page.WaitForSelectorAsync($"div[data-url='/immunizations/covid-19-vaccine.vaccine-status.{Input}.json?vaccineinfo'] table tr");

            var rows = await page.QuerySelectorAllAsync($"div[data-url='/immunizations/covid-19-vaccine.vaccine-status.{Input}.json?vaccineinfo'] table tr");
            var alerted = false;
            foreach (var row in rows)
            {
                var rowHtml = await row.GetPropertyAsync("innerText");
                var cells = await row.QuerySelectorAllAsync("td span");
                if (cells.Length == 2)
                {
                    var storeName = await (await cells[0].GetPropertyAsync("innerText")).JsonValueAsync();
                    var status = await (await cells[1].GetPropertyAsync("innerText")).JsonValueAsync();
                    Console.WriteLine($"Store: {storeName}, Status: {status}");
                    if ( status.ToString() == "Available" && !alerted)
                    {
                        await AlertAsync("CVS");
                        alerted = true;
                    }
                }
                
            }

            */
            Console.WriteLine($"Ending Cvs.ExecuteAsync: {DateTime.Now.ToString()}");

        }

    }
}
