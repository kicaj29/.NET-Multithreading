using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using System.IO;

namespace WhenToUseTaskRun
{
    public class ImageService
    {
        // correct implementation: it does not do CPU-bound operations so we do not use Task.Run
        // this method calls GetByteArrayAsync but does it whithout await so there is no async keyword
        public Task<byte[]> DownloadImageAsync(string url)
        {
            var http = new HttpClientStub();
            return http.GetByteArrayAsync(url);
        }

        // correct implementation: it does CPU-bound operations (blur image) so it makes sesne use Task.Run
        // we use async/await here
        public async Task<byte[]> BlurImageAsync(string path)
        {
            return await Task.Run(() =>
            {
                var image = SixLabors.ImageSharp.Image.Load(path);
                image.Mutate(ctx => ctx.GaussianBlur());
                using (var memoryStream = new MemoryStream())
                {
                    image.SaveAsJpeg(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            );
        }

        public Task<byte[]> DownloadImageAsyncIncorrect(string url)
        {
            var http = new HttpClientStub();
            return Task.Run(() => http.GetByteArrayAsync(url));
        }



    }
}
