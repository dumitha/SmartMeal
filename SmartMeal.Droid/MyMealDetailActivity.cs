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
using SmartMeal.Core.Models;
using SmartMeal.Droid.Utilities;
using SmartMeal.Core.Config;
using SmartMeal.Core.Helpers;
using SmartMeal.Core.Cache;

namespace SmartMeal.Droid
{
    [Activity(Label = "Meal Details", Icon = "@drawable/Heart")]
    public class MyMealDetailActivity : Activity
    {
        ImageView MyMealDetailImageView;
        ImageView MyMealsDetailsCloseScoreSubmitted;
        ImageView MyMealsGradesImage;
        TextView MyMealDetailPostedOn;
        TextView MyMealDetailDescription;
        TextView MyMealLikesNumber;
        TextView MyMealsDetailGradeUserSubmittedValue;
        RelativeLayout MyMealsDetailsScoreSubmitted;
        LinearLayout MyMealsDetailsGradingSection;
        LinearLayout MyMealsDetailsSubmittedGradeSection;
        Button MyMealsDetailsSubmitGrade;
        RadioButton RadioA;
        RadioButton RadioB;
        RadioButton RadioC;
        RadioButton RadioD;
        RadioButton RadioF;

        MealService service;
        Meal meal;
        int MealId;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MyMealsDetail);

            try
            {
                FindViews();
                HandleEvents();
                service = new MealService();

                MealId = Intent.Extras.GetInt("MealId");
                meal = await service.GetMealById(MealId);

                if (meal != null)
                {
                    BindDetailView(meal);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        protected void HandleEvents()
        {
            MyMealsDetailsSubmitGrade.Click += MyMealsDetailsSubmitGrade_Click;
            MyMealsDetailsCloseScoreSubmitted.Click += MyMealsDetailsCloseScoreSubmitted_Click;
            MyMealsGradesImage.Click += MyMealsGradesImage_Click;
        }

        private void MyMealsGradesImage_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.PutExtra("MealId", MealId);
            intent.SetClass(this, typeof(MealGradesActivity));
            StartActivity(intent);
        }

        private void MyMealsDetailsCloseScoreSubmitted_Click(object sender, EventArgs e)
        {
            MyMealsDetailsScoreSubmitted.Visibility = ViewStates.Gone;
        }

        private async void MyMealsDetailsSubmitGrade_Click(object sender, EventArgs e)
        {
            double Grade = 0;

            if (RadioA.Checked)
            {
                Grade = 100;
            }
            else if (RadioB.Checked)
            {
                Grade = 85;
            }
            else if (RadioC.Checked)
            {
                Grade = 75;
            }
            else if (RadioD.Checked)
            {
                Grade = 60;
            }
            else if (RadioF.Checked)
            {
                Grade = 10;
            }

            MealGrade grade = new MealGrade();
            grade.SubmittedBy = UserCache.user.Id;
            grade.Grade = Grade;
            grade.MealId = MealId;

            Meal GradedMeal = await service.AddGrade(grade);

            if (GradedMeal != null && GradedMeal.GradesCount > 0)
            {
                BindDetailView(GradedMeal);
            }
            
            MyMealsDetailsGradingSection.Visibility = ViewStates.Gone;            
            MyMealsDetailsScoreSubmitted.Visibility = ViewStates.Visible;
        }

        protected void FindViews()
        {
            MyMealDetailImageView = FindViewById<ImageView>(Resource.Id.MyMealsDetailsImage);
            MyMealsGradesImage = FindViewById<ImageView>(Resource.Id.MyMealsGradesImage);
            MyMealDetailPostedOn = FindViewById<TextView>(Resource.Id.MyMealsDetailPostedOn);
            MyMealDetailDescription = FindViewById<TextView>(Resource.Id.MyMealsDetailDescription);
            MyMealLikesNumber = FindViewById<TextView>(Resource.Id.MyMealsDetailLikeCount);
            MyMealsDetailGradeUserSubmittedValue = FindViewById<TextView>(Resource.Id.MyMealsDetailGradeUserSubmittedValue);

            MyMealsDetailsSubmitGrade = FindViewById<Button>(Resource.Id.MyMealsDetailsSubmitGrade);
            MyMealsDetailsCloseScoreSubmitted = FindViewById<ImageView>(Resource.Id.MyMealsDetailsCloseScoreSubmitted);
            MyMealsDetailsScoreSubmitted = (RelativeLayout)FindViewById(Resource.Id.MyMealsDetailsScoreSubmitted);
            MyMealsDetailsGradingSection = (LinearLayout)FindViewById(Resource.Id.MyMealsDetailsGradingSection);
            MyMealsDetailsSubmittedGradeSection = (LinearLayout)FindViewById(Resource.Id.MyMealsDetailsSubmittedGradeSection);

            RadioA = FindViewById<RadioButton>(Resource.Id.radio_A);
            RadioB  = FindViewById<RadioButton>(Resource.Id.radio_B);
            RadioC = FindViewById<RadioButton>(Resource.Id.radio_C);
            RadioD = FindViewById<RadioButton>(Resource.Id.radio_D);
            RadioF = FindViewById<RadioButton>(Resource.Id.radio_F);
        }

        private void BindDetailView(Meal meal)
        {
            MyMealLikesNumber.Text = "--";

            if (meal.GradesCount > 0)
            {
                MyMealLikesNumber.Text = PhraseHelper.GetGradePhrase(Convert.ToInt32(meal.Grade));
                MyMealsGradesImage.Visibility = ViewStates.Visible;
            }

            MyMealDetailPostedOn.Text = meal.CreatedOn.ToShortDateString();
            MyMealDetailDescription.Text = meal.Description;

            FetchMealRatingByUser();
            
            if (!String.IsNullOrEmpty(meal.ImageUrl))
            {
                RenderImage(meal.ImageUrl);
            }
        }

        private async void FetchMealRatingByUser()
        {
            try
            {
                MealRating mealRating = await service.GetMealRatingByUser(UserCache.user.Id, MealId);

                if (mealRating != null && mealRating.RatingId > 0)
                {
                    //This user has already submitted a rating or a grade.
                    MyMealsDetailGradeUserSubmittedValue.Text = PhraseHelper.GetGradePhrase(Convert.ToInt32(mealRating.Grade));
                    MyMealsDetailsSubmittedGradeSection.Visibility = ViewStates.Visible;
                    MyMealsDetailsGradingSection.Visibility = ViewStates.Gone;
                }
                else
                {
                    MyMealsDetailsSubmittedGradeSection.Visibility = ViewStates.Gone;
                    MyMealsDetailsGradingSection.Visibility = ViewStates.Visible;
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }
        }

        private async void RenderImage(string imageUrl)
        {
            try
            {
                string Uri = ConfigManager.ImageUri;
                Uri += imageUrl;

                var imageBitmap = await ImageHelper.GetImageBitMapFromUrlAsync(Uri);

                if (imageBitmap != null)
                {
                     MyMealDetailImageView.SetImageBitmap(imageBitmap);
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }
        }

    }
}