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
using SmartMeal.Core.Services;
using SmartMeal.Core.Models;
using SmartMeal.Droid.Adapters;
using SmartMeal.Core.Cache;
using SmartMeal.Core.ViewModels;

namespace SmartMeal.Droid.Fragments
{
    public class MyMealsFragment : Fragment
    {
        protected MealService mealsService;
        protected List<Meal> mealList = new List<Meal>();
        protected ListView listView;


        public MyMealsFragment()
        {
            mealsService = new MealService();
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

            return inflater.Inflate(Resource.Layout.MyMealsFragment, container, false);
        }

        public override async void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            try
            {
                bool Refresh = false;

                FindViews();
                HandleEvents();

                if (MyMealsCache.MyMealsList == null || MyMealsCache.MyMealsList.Count == 0)
                {
                    mealList = await mealsService.GetMealsByUserId(UserCache.user.Id.ToString());
                    MyMealsCache.MyMealsList = mealList;
                    Refresh = true;
                }
                else
                {
                    mealList = MyMealsCache.MyMealsList;
                }

                List<TwoColumnImageView> TwoColumnImageList = new List<TwoColumnImageView>();
                
                if (Refresh)
                {
                    TwoColumnImageList = mealsService.GetTwoColumnImageView(mealList);
                    MyMealsCache.MyMealsTwoColumnGallery = TwoColumnImageList;
                }
                else if (MyMealsCache.MyMealsTwoColumnGallery != null)
                {
                    TwoColumnImageList = MyMealsCache.MyMealsTwoColumnGallery;
                }

                listView.Adapter = new MyMealsAdapter(this.Activity, TwoColumnImageList);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        protected void HandleEvents()
        {

        }

        protected void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.MyMealsFragmentListView);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //HotDog hotDog = hotDogs[e.Position];

            //Intent intent = new Intent();
            //intent.SetClass(this.Activity, typeof(HotDogDetailActivity));
            //intent.PutExtra("selectedHotDogId", hotDog.HotDogId);

            //StartActivityForResult(intent, 100);
        }
    }
}