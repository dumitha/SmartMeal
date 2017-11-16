using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;

namespace SmartMeal.Droid.Utilities
{
    public class ImageHelper
    {

        public ImageHelper()
        {

        }
         
        public static Bitmap GetImageBitMapFromUrl(string url, int height = 0, int width = 0)
        {

            Bitmap img = null;

            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        if(height > 0 && width > 0)
                        {
                            byte[] resizedImgData = ResizeImage(imageBytes, width, height);
                            img = BitmapFactory.DecodeByteArray(resizedImgData, 0, resizedImgData.Length);
                        }
                        else
                        {
                            img = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return img;
        }

        public static async Task<Bitmap> GetImageBitMapFromUrlAsync(string url, int height = 0, int width = 0)
        {

            Bitmap img = null;

            try
            {
                using (var webClient = new HttpClient())
                {
                    var imageBytes = await webClient.GetByteArrayAsync(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        if (height > 0 && width > 0)
                        {
                            byte[] resizedImgData = ResizeImage(imageBytes, width, height);
                            img = BitmapFactory.DecodeByteArray(resizedImgData, 0, resizedImgData.Length);
                        }
                        else
                        {
                            img = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return img;
        }

        public static Bitmap GetImgeBitmapFromFilePath(string fileName, int width, int height)
        {
            Bitmap resizedBitmap = null;
 
            try
            {
                BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
                BitmapFactory.DecodeFile(fileName, options);

                int outHeight = options.OutHeight;
                int outWidth = options.OutWidth;
                int inSampleSize = 1;

                if (outHeight > height || outWidth > width)
                {
                    inSampleSize = outWidth > outHeight ? outHeight / height : outWidth / width;
                }

                options.InSampleSize = inSampleSize;
                options.InJustDecodeBounds = false;
                resizedBitmap = BitmapFactory.DecodeFile(fileName, options);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return null;
            }

            return resizedBitmap;
        }

        public static byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            byte[] resizedImageData = null;

            try
            {
                // Load the bitmap 
                BitmapFactory.Options options = new BitmapFactory.Options();// Create object of bitmapfactory's option method for further option use
                options.InPurgeable = true; // inPurgeable is used to free up memory while required
                Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length, options);

                float newHeight = 0;
                float newWidth = 0;

                var originalHeight = originalImage.Height;
                var originalWidth = originalImage.Width;

                if (originalHeight > originalWidth)
                {
                    newHeight = height;
                    float ratio = originalHeight / height;
                    newWidth = originalWidth / ratio;
                }
                else
                {
                    newWidth = width;
                    float ratio = originalWidth / width;
                    newHeight = originalHeight / ratio;
                }

                Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, true);

                originalImage.Recycle();

                using (MemoryStream ms = new MemoryStream())
                {
                    resizedImage.Compress(Bitmap.CompressFormat.Png, 100, ms);
                    resizedImage.Recycle();
                    resizedImageData = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return resizedImageData;
        }
    }

}
