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
using System.Threading.Tasks;

namespace SmartMeal.Droid.Adapters
{
    public class NewsFeedAdapter : BaseAdapter
    {

        List<EventMessage> items;
        Activity context;
        int ItemsToShow = ConfigManager.NewsFeedItemsToRenderRightWay;
        int ItemCount = 1;

        public NewsFeedAdapter(Activity context, List<EventMessage> items)
        {
            this.context = context;
            this.items = items;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            Bitmap imageBitmap = null;
            ImageView NewsFeedImage = null;

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.NewsFeedRow, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.NewsFeedDescription).Text = String.Format("{0} ", item.Description);
            convertView.FindViewById<TextView>(Resource.Id.NewsFeedPostedOn).Text = item.PostedOn.ToShortDateString();

            if (NewsFeedImage == null)
            {
                NewsFeedImage = convertView.FindViewById<ImageView>(Resource.Id.NewsFeedImageView);
            }
                      
            if (item.RenderedImagesList.Count > 0)
            {
                //If this image has already been renered.  Get it from the rendered images list.
                imageBitmap = (Bitmap)item.RenderedImagesList[0];
                NewsFeedImage.SetImageBitmap(imageBitmap);
            }
            else if (item.ImageList.Count > 0)
            {
                if (ItemCount > ItemsToShow)
                {
                    //Show these in async mode.  Get the image from the server in async mode.
                    RenderImage(item.ImageList[0], item, NewsFeedImage);
                }
                else
                {
                    //Render these before we show anything.  This number is coming from the config manager.
                    string Uri = ConfigManager.ImageUri;
                    Uri += item.ImageList[0];
                    imageBitmap = ImageHelper.GetImageBitMapFromUrl(Uri, 80, 80);

                    if (imageBitmap != null)
                    {
                        Console.WriteLine("The bytes for item {0} is {1} ",item.Description, imageBitmap.ByteCount.ToString());
                        NewsFeedImage.SetImageBitmap(imageBitmap);
                        //Save it in the cache list for next time.
                        item.RenderedImagesList.Add(imageBitmap);
                    }
                }                 
            }

            ItemCount += 1;

            return convertView;
        }

        private async void RenderImage(string imageUrl, EventMessage item, ImageView NewsFeedImage)
        {
            try
            {
                string Uri = ConfigManager.ImageUri;
                Uri += imageUrl;

                var imageBitmap = await ImageHelper.GetImageBitMapFromUrlAsync(Uri);

                if (imageBitmap != null)
                {
                    NewsFeedImage.SetImageBitmap(imageBitmap);
                    item.RenderedImagesList.Add(imageBitmap);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
    }

    class NewsFeedAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}