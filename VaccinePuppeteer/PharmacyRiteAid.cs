using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VaccinePuppeteer
{
    public class PharmacyRiteAid : PharmacyBase
    {
        public PharmacyRiteAid(AppSettingsRiteAid ra)
        {
            this.RiteAidSettings = ra;
        }

        public override async Task ExecuteAsync()
        {
            Console.WriteLine("Beginning RiteAid.ExecuteAsync");
            var browser = await this.GetBrowserAsync();
            Page page = await browser.NewPageAsync();
            await page.GoToAsync("https://www.riteaid.com/pharmacy/covid-qualifier");

            // Text Inputs
            await Task.Delay(2000);
            await page.TypeAsync("#dateOfBirth", this.RiteAidSettings.Dob);
            await page.TypeAsync("#city", this.RiteAidSettings.City);
            await page.TypeAsync("#zip", this.RiteAidSettings.Zip);

            await page.ClickAsync("#Occupation");
            await page.TypeAsync("#Occupation", this.RiteAidSettings.Occupation);
            await Task.Delay(200);
            await page.ClickAsync(".typeahead__result ul li a");

            await page.ClickAsync("#mediconditions");
            await page.TypeAsync("#mediconditions", this.RiteAidSettings.MedicalConditions);
            await Task.Delay(200);

            await page.ClickAsync("#eligibility_state");
            await page.TypeAsync("#eligibility_state", this.RiteAidSettings.State);
            await Task.Delay(200);

            await page.ClickAsync("#zip");
            await page.Keyboard.PressAsync("Tab");
            await page.Keyboard.PressAsync("Tab");
            await page.Keyboard.PressAsync("Tab");
            await Task.Delay(2000);



            // Click 1st Button
            await page.WaitForSelectorAsync("#continue");
            await Task.Delay(2000);
            await page.ClickAsync("#continue");

            // Click 2nd Button
            await page.WaitForSelectorAsync("a[href='/pharmacy/apt-scheduler']", new WaitForSelectorOptions() { Visible = true });
            await Task.Delay(3000);
            await page.ClickAsync("a[href='/pharmacy/apt-scheduler']");

            // $(".covid-store__store__anchor")
            await page.WaitForSelectorAsync(".covid-store__store__anchor");
            var firstButton = await page.QuerySelectorAsync($".covid-store__store__anchor--unselected");
            var currentUrl = page.Url;
            await Task.Delay(2000);
            await firstButton.ClickAsync();
            await Task.Delay(2000);

            // Continue
            await page.WaitForSelectorAsync("#continue");
            await Task.Delay(1000);
            await page.ClickAsync("#continue");
            await Task.Delay(1000);
            await page.ClickAsync("#continue");

            await Task.Delay(5000);
            if (currentUrl != page.Url)
            {
                await AlertAsync("RiteAid");
            }
            Console.WriteLine("Ending RiteAid.ExecuteAsync");
        }
        public AppSettingsRiteAid RiteAidSettings { get; }
    }
}
