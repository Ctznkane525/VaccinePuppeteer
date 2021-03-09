using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccinePuppeteer
{
    public class PharmacyWalgreens : PharmacyBase
    {

        public PharmacyWalgreens(string zipCode)
        {
            this.Input = zipCode;
        }

        public string Input { get; }

        public override async Task ExecuteAsync()
        {
            Console.WriteLine("Beginning Walgreens.ExecuteAsync");
            var browser = await this.GetBrowserAsync();
            Page page = await browser.NewPageAsync();
            await page.GoToAsync("https://www.walgreens.com/findcare/vaccination/covid-19");
            await page.WaitForSelectorAsync("a[href='/findcare/vaccination/covid-19/location-screening']");
            await page.ClickAsync("a[href='/findcare/vaccination/covid-19/location-screening']");
            await page.WaitForSelectorAsync("#inputLocation");
            await Task.Delay(5000);
            await page.ClickAsync("#inputLocation");
            foreach (var number in Enumerable.Range(1, 5))
            {
                await page.Keyboard.PressAsync("Backspace");
                await Task.Delay(100);
            }
            await page.TypeAsync("#inputLocation", Input);
            await page.ClickAsync("button.btn");

            await page.WaitForSelectorAsync(".alert");
            var alertItem = await page.QuerySelectorAsync(".alert");
            var alertText = await (await alertItem.GetPropertyAsync("innerText")).JsonValueAsync();
            if (!alertText.ToString().Contains("unavailable"))
                await AlertAsync("Walgreens");

            Console.WriteLine("Ending Walgreens.ExecuteAsync");

        }


    }
}
