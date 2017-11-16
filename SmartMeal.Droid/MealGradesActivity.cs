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
using SmartMeal.Core.Services;
using SmartMeal.Droid.Adapters;
using SmartMeal.Core.Models;

namespace SmartMeal.Droid
{
    [Activity(Label = "Meal Grades", Icon = "@drawable/Heart")]
    public class MealGradesActivity : Activity
    {
        ListView gradesListView;
        MealService service;
        List<MealGrade> GradesList = new List<MealGrade>();
        int MealId = 0;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GradesList);

            FindViews();
            HandleEvents();

            service = new MealService();

            MealId = Intent.Extras.GetInt("MealId");

            if (MealId > 0)
            {
                GradesList = await service.GetGrades(MealId);
                gradesListView.Adapter = new GradesListAdapter(this, GradesList);
            }

        }

        private void FindViews()
        {
            try
            {
                gradesListView = FindViewById<ListView>(Resource.Id.GradesListView);

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        private void HandleEvents()
        {

        }
    }
}