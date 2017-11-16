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

namespace SmartMeal.Droid.Fragments
{
    public class BaseFragment : Fragment
    {
        protected ListView listView;


        public BaseFragment()
        {
            //hotDogDataService = new HotDogDataService();
        }

        protected void HandleEvents()
        {
            //listView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //HotDog hotDog = hotDogs[e.Position];

            //Intent intent = new Intent();
            //intent.SetClass(this.Activity, typeof(HotDogDetailActivity));
            //intent.PutExtra("selectedHotDogId", hotDog.HotDogId);

            //StartActivityForResult(intent, 100);
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 100)
            {
                //HotDog selectedHotDog = hotDogDataService.GetHotDogById(data.GetIntExtra("selectedHotDogId", 0));

                //var dialog = new AlertDialog.Builder(this.Activity);
                //dialog.SetTitle("Confirmation");
                //dialog.SetMessage(string.Format("You've added {0} time(s) the {1} hot dog.", 1, selectedHotDog.Name));
                //dialog.Show();
            }
        }

        protected void FindViews()
        {
            //listView = this.View.FindViewById<ListView>(Resource.Id.HotDogListView);
        }
    }
}