using System;
using System.Collections.Generic;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace VaccinePuppeteer
{
    public class PharmacyBase : IDisposable
    {
        public PharmacyBase()
        {
            
        }

        public Browser Browser { get; private set; }

        public async void Dispose()
        {
            try
            {
               await Browser.CloseAsync();
               if (Browser == null ) Browser.Dispose();
            }
            catch (Exception e)
            {

            }
        }

        public async Task<Browser> GetBrowserAsync()
        {
            if (Browser == null)
            {

                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

                Browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    DefaultViewport = null,
                    Headless = false, //;true,
                    Args = new string[] {"--start-maximized"}
                });
            }
            
            return Browser;
        }

        public async Task AlertAsync()
        {
            await Task.Run(() => {

                for (int i = 0; i < 2; i++)
                {
                    SoundPlayer simpleSound = new SoundPlayer(@"Alarm01.wav");
                    simpleSound.Play();
                    Task.Delay(5000).GetAwaiter().GetResult();
                }
               

            });
        }

        public virtual async Task ExecuteAsync()
        {
            await Task.Run(() =>
            {
                throw new NotImplementedException();
            });
        }
    }
}
