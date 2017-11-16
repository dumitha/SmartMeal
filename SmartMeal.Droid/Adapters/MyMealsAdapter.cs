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
using SmartMeal.Core.Models;
using SmartMeal.Droid.Utilities;
using Android.Graphics;
using SmartMeal.Core.Config;
using SmartMeal.Core.ViewModels;

namespace SmartMeal.Droid.Adapters
{
    public class MyMealsAdapter : BaseAdapter
    {

        Activity context;
        List<TwoColumnImageView> items;
        int ItemsToShow = ConfigManager.MyMealsItemsToRenderRightWay;
        int ItemCount = 1;

        public MyMealsAdapter(Activity context, List<TwoColumnImageView> items)
        {
            this.context = context;
            this.items = items;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            string Uri = ConfigManager.ImageUri;
            
            Bitmap imageBitmapOne = null;
            Bitmap ImageBitmapTwo = null;

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.MyMealsRow, null);
            }

            ImageView ImgOne = convertView.FindViewById<ImageView>(Resource.Id.MyMealsOne);
            ImageView ImgTwo = convertView.FindViewById<ImageView>(Resource.Id.MyMealsTwo);

            if (item.RenderedImageOne != null)
            {
                imageBitmapOne = (Bitmap)item.RenderedImageOne;                
                ImgOne.SetImageBitmap(imageBitmapOne);
                ImgOne.Click += ImgOne_Click;
            }

            if (item.RenderedImageTwo != null)
            {
                ImageBitmapTwo = (Bitmap)item.RenderedImageTwo;                
                ImgTwo.SetImageBitmap(ImageBitmapTwo);
                ImgTwo.Click += ImgTwo_Click;
            }

            if (imageBitmapOne == null && item.ImageOneUrl != String.Empty)
            {
                if (ItemCount > ItemsToShow)
                {
                    RenderImage(item.ImageOneUrl, item, 1, ImgOne);
                }
                else
                {
                    Uri += item.ImageOneUrl;
                    var imageBitmap = ImageHelper.GetImageBitMapFromUrl(Uri, 190, 190);
                    ImgOne.SetImageBitmap(imageBitmap);
                    ImgOne.Click += ImgOne_Click;
                    item.RenderedImageOne = imageBitmap;
                    ItemCount++;                    
                }                    
            }
            
            if (ImageBitmapTwo == null && item.ImageTwoUrl != String.Empty)
            {
                if (ItemCount > ItemsToShow)
                {
                    RenderImage(item.ImageTwoUrl, item, 2, ImgTwo);
                }
                else
                {
                    Uri += item.ImageTwoUrl;
                    var imageBitmap = ImageHelper.GetImageBitMapFromUrl(Uri);
                    ImgTwo.SetImageBitmap(imageBitmap);
                    ImgTwo.Click += ImgTwo_Click;
                    item.RenderedImageTwo = imageBitmap;
                    ItemCount++;
                }                    
            }

            return convertView;
        }

        private async void RenderImage(string imageUrl, TwoColumnImageView item, int slotIndex, ImageView imageView)
        {
            try
            {
                string Uri = ConfigManager.ImageUri;
                Uri += imageUrl;

                var imageBitmap = await ImageHelper.GetImageBitMapFromUrlAsync(Uri, 190, 190);

                if (imageBitmap != null)
                {
                    if (slotIndex == 1)
                    {
                        imageView.SetImageBitmap(imageBitmap);
                        imageView.Click += ImgOne_Click;
                        item.RenderedImageOne = imageBitmap;
                    }

                    if (slotIndex == 2)
                    {
                        imageView.SetImageBitmap(imageBitmap);
                        imageView.Click += ImgTwo_Click;
                        item.RenderedImageTwo = imageBitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        private void ImgTwo_Click(object sender, EventArgs e)
        {
            var dialog = new AlertDialog.Builder(this.context);
            dialog.SetTitle("Info");
            dialog.SetMessage("Image preview is not available in beta. This feature will be in production.");
            dialog.Show();
        }

        private void ImgOne_Click(object sender, EventArgs e)
        {
            var dialog = new AlertDialog.Builder(this.context);
            dialog.SetTitle("Info");
            dialog.SetMessage("Image preview is not available in beta. This feature will be in production.");
            dialog.Show();
        }

        //Fill in cound here, currently 0


    }

    class MyMealsAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}