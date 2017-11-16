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
using SmartMeal.Core.Helpers;

namespace SmartMeal.Droid.Adapters
{
    class GradesListAdapter : BaseAdapter
    {

        List<MealGrade> items;
        Activity context;
        
        public GradesListAdapter(Activity context, List<MealGrade> items)
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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            if (convertView == null)
            {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.GradesRow, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.GradesRowPostedBy).Text = String.Format("{0} ", item.SubmittedByName);
            convertView.FindViewById<TextView>(Resource.Id.GradesRowPostedOn).Text = item.SubmittedOn.ToShortDateString();
            convertView.FindViewById<TextView>(Resource.Id.GradesRowGrade).Text = PhraseHelper.GetGradePhrase(Convert.ToInt32(item.Grade));

            //ImageView imgView = convertView.FindViewById<ImageView>(Resource.Id.GradesListImageView);

            //Set the image of the view according to the grade the user gave.

            return convertView;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

    }

    class GradesListAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}