using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SmartMeal.Core.Models;
using SmartMeal.Core.Services;
using SmartMeal.Droid.Adapters;
using SmartMeal.Core.Cache;
using SmartMeal.Droid.Utilities;
using System.Threading.Tasks;
using SmartMeal.Core.Config;

namespace SmartMeal.Droid.Fragments
{
    public class NewsFeedFragment : Fragment
    {

        protected NewsFeedService newsFeedService;
        protected List<EventMessage> newsFeedList = new List<EventMessage>();
        protected ListView listView;
        protected int ItemsToShow = 8;

        public NewsFeedFragment()
        {
            newsFeedService = new NewsFeedService();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            return inflater.Inflate(Resource.Layout.NewsFeedFragment, container, false);
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            try
            {
                FindViews();
                HandleEvents();

                if (NewsFeedCache.NewsList == null || NewsFeedCache.NewsList.Count == 0)
                {
                    newsFeedList = await newsFeedService.GetNewsFeedForUser(UserCache.user.Id.ToString());
                   // var newFeedListWithImages = await FetchImagesParallelAsync(newsFeedList);
                    NewsFeedCache.NewsList = newsFeedList;                    
                }
                else
                {
                    newsFeedList = NewsFeedCache.NewsList;
                }

                //List<EventMessage> filteredNewsFeedList = new List<EventMessage>();
                //int count = 1;

                //foreach (EventMessage itm in newsFeedList)
                //{
                //    if (count <= ItemsToShow)
                //    {
                //        filteredNewsFeedList.Add(itm);
                //    }
                //    count++;
                //}

                listView.Adapter = new NewsFeedAdapter(this.Activity, newsFeedList);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        private async Task<List<EventMessage>> FetchImagesParallelAsync(List<EventMessage> newsList)
        {
            int ImagesToShow = 5;
            List<EventMessage> ShortList = new List<EventMessage>();
            Dictionary<int, object> RenderedImages = new Dictionary<int, object>();

            for (int i = 0; i < ImagesToShow; i++)
            {
                ShortList.Add(newsList[i]);
            }

            try
            {
                var tasks = ShortList.Select(
                        async newsItem => new
                        {
                            newsItem,
                            img = await ImageHelper.GetImageBitMapFromUrlAsync(ConfigManager.ImageUri + newsItem.ImageList[0])
                        }).ToList();

                foreach (var task in tasks)
                {
                    if (task.Status != TaskStatus.Faulted)
                    {
                        var result = await task;

                        var NewsItm = result.newsItem;
                        var RenderedImg = result.img;

                        RenderedImages.Add(NewsItm.ObjectId, RenderedImg);
                    }
                }

                int Counter = 0;

                foreach (EventMessage itm in newsList)
                {
                    if (RenderedImages.ContainsKey(itm.ObjectId))
                    {
                        var Img = RenderedImages[itm.ObjectId];
                        if (Img != null)
                        {
                            itm.RenderedImagesList.Add(Img);
                        }
                    }

                    if (Counter >= ImagesToShow)
                    {
                        break;
                    }

                    Counter++;
                }

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return newsList;
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {

            }
        }

        protected void HandleEvents()
        {
            listView.ItemClick += ListView_ItemClick;
            //listView.Scroll += ListView_Scroll;
            //listView.ScrollChange += ListView_ScrollChange;
        }

        private void ListView_ScrollChange(object sender, View.ScrollChangeEventArgs e)
        {
            Console.WriteLine("Inside scroll change");
        }

        private void ListView_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            Console.WriteLine("Inside scroll");
        }

        protected void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.NewsFragmentListView);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            EventMessage msg = newsFeedList[e.Position];

            Intent intent = new Intent();
            intent.SetClass(this.Activity, typeof(MyMealDetailActivity));
            intent.PutExtra("MealId", msg.ObjectId);

            StartActivityForResult(intent, 100);
        }

    }
}