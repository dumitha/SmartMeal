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
using SmartMeal.Core.Cache;

namespace SmartMeal.Droid
{
    [Activity(Label = "My Smart Meals", MainLauncher = true, Icon = "@drawable/Heart")]
    public class SignInActivity : Activity
    {

        private EditText signInEmailText;
        private EditText signInPasswordText;
        private Button LogInButton;
        private Button CreateNewAccountButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SignIn);

            CheckLoginStatus();

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            LogInButton = FindViewById<Button>(Resource.Id.LogIn);
            CreateNewAccountButton = FindViewById<Button>(Resource.Id.CreateNewAccount);
            signInEmailText = FindViewById<EditText>(Resource.Id.SignInEmail);
            signInPasswordText = FindViewById<EditText>(Resource.Id.SignInPassword);
        }
            
        private void HandleEvents()
        {
            LogInButton.Click += LogInButton_Click;
            CreateNewAccountButton.Click += CreateNewAccountButton_Click;
        }

        private void CheckLoginStatus()
        {
            if (UserCache.user != null && !String.IsNullOrEmpty(UserCache.user.Token))
            {
                //The user is already logged in but got here through by using the back buttons.  Send back to main.
                Intent intent = new Intent();
                intent.SetClass(this, typeof(MainOptions));
                StartActivity(intent);
            }
        }

        private void CreateNewAccountButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetClass(this, typeof(CreateAccount));
            StartActivity(intent);            
        }

        private async void LogInButton_Click(object sender, EventArgs e)
        {
            string Err = String.Empty;

            if (signInEmailText.Text.Trim()== String.Empty)
            {
                Err += "A user name is required.";
            }

            if (signInPasswordText.Text.Trim() == String.Empty)
            {
                Err += "\nA password is required.";
            }

            if (Err != String.Empty)
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Login Error");
                dialog.SetMessage(Err);
                dialog.Show();
                return;
            }

            UserService service = new UserService();
            User user = await service.GetUser(signInEmailText.Text, signInPasswordText.Text);

            if (String.IsNullOrEmpty(user.Token))
            {
                //The user validation failed.  Show the user the error message
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Login Error");
                dialog.SetMessage("The user name or password you entered is incorrect.  Please try again.");
                dialog.Show();
            }
            else
            {
                //Validation was successfull.  Save the token into cache and send the user to the main screen.
                UserCache.user = user;
                Intent intent = new Intent();
                intent.SetClass(this, typeof(MainOptions));
                StartActivity(intent);
            }
        }
    }
}