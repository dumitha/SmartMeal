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
using SmartMeal.Droid.Fragments;
using SmartMeal.Core.Cache;

namespace SmartMeal.Droid
{
    [Activity(Label = "Smart Meals", Icon = "@drawable/Heart")]
    public class MainOptions : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MainOptions);

            this.Window.SetTitle(String.Format("Smart Meals - {0}",UserCache.user.DisplayName));
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            
            AddTab("Home", 0, new NewsFeedFragment());
            AddTab("My Meals", 0, new MyMealsFragment());
            AddTab("Add Meal", 0, new AddMealFragment());

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        public override void OnBackPressed()
        {
            this.ActionBar.SelectTab(this.ActionBar.GetTabAt(0));
        }

        private void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            try
            {
                var tab = this.ActionBar.NewTab();
                tab.SetText(tabText);

                if (iconResourceId > 0)
                {
                    tab.SetIcon(iconResourceId);
                }

                tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
                {
                    var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
                    if (fragment != null)
                    {
                        e.FragmentTransaction.Remove(fragment);
                    }

                    e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
                };

                tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
                {
                    e.FragmentTransaction.Remove(view);
                };

                this.ActionBar.AddTab(tab);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

        }


    }
}