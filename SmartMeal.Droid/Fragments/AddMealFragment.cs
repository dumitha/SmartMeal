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
using Android.Provider;
using Android.Graphics;
using Java.IO;
using SmartMeal.Droid.Utilities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using SmartMeal.Core.Cache;
using SmartMeal.Core.Config;

namespace SmartMeal.Droid.Fragments
{
    public class AddMealFragment : Fragment
    {
        protected EditText AddMealDescriptionView;
        protected ImageView AddMealImageView;
        protected string SelectedCategory;
        protected Spinner AddMealCategorySpinner;
        protected Button AddMealSubmitButton;
        protected Button AddMealLaunchCameraButton;
        protected Button AddMealCancel;

        private File imageDirectory;
        private File imageFile;
        private Bitmap imageBitmap;

        public AddMealFragment()
        {

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
            
            return inflater.Inflate(Resource.Layout.AddMealFragment, container, false);

        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            try
            {
                FindViews();
                HandleEvents();

                imageDirectory = new File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "SmartMeal");

                if (!imageDirectory.Exists())
                {
                    imageDirectory.Mkdir();
                }

                //TODO: Get the categories from a service.
                List<string> MealCategories = new List<string>();
                MealCategories.Add("Breakfast");
                MealCategories.Add("Lunch");
                MealCategories.Add("Dinner");
                MealCategories.Add("Snack");

            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        public async override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            int height = AddMealImageView.Height;
            int width = AddMealImageView.Width;
            imageBitmap = ImageHelper.GetImgeBitmapFromFilePath(imageFile.Path, width, height);

            if (imageBitmap != null)
            {
                AddMealImageView.SetImageBitmap(imageBitmap);

                //Java.IO.FileInputStream fis = new Java.IO.FileInputStream(imageFile.Path);
                System.IO.MemoryStream stream = new System.IO.MemoryStream();

                //BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
                //Bitmap imageToUpload = BitmapFactory.DecodeFile(imageFile.Path, options);

                imageBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                
                //Create byte output stream
                //int bytesRead = 0;
                //Java.IO.ByteArrayOutputStream bos = new Java.IO.ByteArrayOutputStream();
                //byte[] b = new byte[1024];

                //while ((bytesRead = fis.Read(b)) != -1)
                //{
                //    bos.Write(b, 0, bytesRead);
                //}

                //byte[] bytes = bos.ToByteArray();
                //Rezise the image.  Its way too big coming from the camera
                //byte[] ResizedBytes = ImageHelper.ResizeImage(bytes, 500, 500);
                byte[] bytes = stream.ToArray();

                string FileName = String.Format("SmartMealImage_{0}.jpg", Guid.NewGuid());
                await performBlobOperation(FileName, bytes);

                MyMealsCache.NewMealName = FileName;
                imageBitmap = null;
            }

            //Call this to avoid memory leaks.
            GC.Collect();
        }

        public static async Task performBlobOperation(string name, byte[] DataArrayBytes)
        {
            try
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigManager.UploadImageKey);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("mysmartmeals");

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);

                if (blockBlob != null)
                {
                    // Create the blob with the given name.
                    await blockBlob.UploadFromByteArrayAsync(DataArrayBytes, 0, DataArrayBytes.Length);
                }
            }
            catch(Exception ex)
            {
                string err = ex.Message;            
            }
        }

        private void FindViews()
        {
            AddMealDescriptionView = this.View.FindViewById<EditText>(Resource.Id.AddMealDescription);
            AddMealImageView = this.View.FindViewById<ImageView>(Resource.Id.AddMealImage);
            AddMealSubmitButton = this.View.FindViewById<Button>(Resource.Id.AddMealSubmit);
            AddMealLaunchCameraButton = this.View.FindViewById<Button>(Resource.Id.AddMealLaunchCamera);
            AddMealCancel = this.View.FindViewById<Button>(Resource.Id.AddMealCancel);

            //AddMealCategorySpinner = this.View.FindViewById<Spinner>(Resource.Id.AddMealCategorySpinner);
        }

        private void HandleEvents()
        {
            AddMealLaunchCameraButton.Click += AddMealLaunchCameraButton_Click;
            AddMealSubmitButton.Click += AddMealSubmitButton_Click;
            AddMealCancel.Click += AddMealCancel_Click;
        }

        private void AddMealCancel_Click(object sender, EventArgs e)
        {
            this.Activity.ActionBar.SelectTab(this.Activity.ActionBar.GetTabAt(0));
        }

        #region EventHandlers
        private async void AddMealSubmitButton_Click(object sender, EventArgs e)
        {

            string Err = String.Empty;

            if (imageFile == null || String.IsNullOrEmpty(imageFile.Name))
            {
                Err += "\nA picture is required.";
            }

            if (String.IsNullOrEmpty(AddMealDescriptionView.Text.Trim()))
            {
                Err += "\nA description value is required.";
            }
            
            if (!String.IsNullOrEmpty(Err))
            {
                //Add more padding at the bottom by adding a new line.
                Err += "\n";

                var dialog = new AlertDialog.Builder(this.Activity);
                dialog.SetTitle("Error");
                dialog.SetMessage(Err);
                dialog.Show();
                return;
            }

            Meal meal = new Meal();
            meal.UserId = UserCache.user.Id.ToString();
            meal.ImageUrl = MyMealsCache.NewMealName;
            meal.Description = AddMealDescriptionView.Text.Trim();

            MealService service = new MealService();
            bool Created = await service.CreateNew(meal);

            //Reset the cache
            CacheManager.ResetSessionCache();

            this.Activity.ActionBar.SelectTab(this.Activity.ActionBar.GetTabAt(1));
            if (!Created)
            {
                //Need to do some error handling and stuff
            }
        }

        private void AddMealLaunchCameraButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            imageFile = new File(imageDirectory, String.Format("SmartMeal_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(imageFile));
            StartActivityForResult(intent, 200);
        }
        #endregion


    }
}