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
    [Activity(Label = "My Smart Meals", MainLauncher = false, Icon = "@drawable/Heart")]
    public class CreateAccount : Activity
    {
        EditText FirstNameEditText;
        EditText LastNameEditText;
        EditText PasswordEditText;
        EditText EmailEditText;
        EditText ConfirmPasswordEditText;

        Button CreateAccountButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccount);


            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            FirstNameEditText = FindViewById<EditText>(Resource.Id.CreateFirstNameEditView);
            LastNameEditText = FindViewById<EditText>(Resource.Id.CreateLastNameEditView);
            PasswordEditText = FindViewById<EditText>(Resource.Id.CreatePasswordEditView);
            EmailEditText = FindViewById<EditText>(Resource.Id.CreateUserNameEditView);
            ConfirmPasswordEditText = FindViewById<EditText>(Resource.Id.CreateConfirmPasswordEditView);
            CreateAccountButton = FindViewById<Button>(Resource.Id.CreateAccountButton);
        }

        private void HandleEvents()
        {
            CreateAccountButton.Click += CreateAccountButton_Click;
        }

        private async void CreateAccountButton_Click(object sender, EventArgs e)
        {
            string Err = String.Empty;

            if (String.IsNullOrEmpty(FirstNameEditText.Text.Trim()))
            {
                Err += "\nA first name is required.";
            }

            if (String.IsNullOrEmpty(LastNameEditText.Text.Trim()))
            {
                Err += "\nA last name is required.";
            }

            if (String.IsNullOrEmpty(PasswordEditText.Text.Trim()))
            {
                Err += "\nA password is required.";
            }

            if (String.IsNullOrEmpty(EmailEditText.Text.Trim()))
            {
                Err += "\nAn email is required.";
            }

            if (String.IsNullOrEmpty(ConfirmPasswordEditText.Text.Trim()))
            {
                Err += "\nA confirmation password is required.";
            }
            
            if (ConfirmPasswordEditText.Text.Trim().ToLower() != PasswordEditText.Text.Trim().ToLower())
            {
                Err += "\nThe password and confirmation password must match.";
            }

            if (Err != String.Empty)
            {
                Err += "\n";

                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Error");
                dialog.SetMessage(Err);
                dialog.Show();
                return;
            }

            if (String.IsNullOrEmpty(Err))
            {
                //Create the new account.
                UserService service = new UserService();
                User user = new User();
                user.FirstName = FirstNameEditText.Text.Trim();
                user.LastName = LastNameEditText.Text.Trim();
                user.UserName = EmailEditText.Text.Trim();
                user.Password = PasswordEditText.Text.Trim();

                User NewUser = await service.CreateNewUser(user);

                if (!String.IsNullOrEmpty(NewUser.Status))
                {
                    //Had an error
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Error");
                    dialog.SetMessage(NewUser.Status);
                    dialog.Show();
                }
                else
                {
                    UserCache.user = NewUser;
                    Intent intent = new Intent();
                    intent.SetClass(this, typeof(MainOptions));
                    StartActivity(intent);
                }
            }
        }
    }
}