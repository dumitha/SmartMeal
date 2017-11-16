using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Config
{
    public static class ConfigManager
    {

        private static string _ApiUri = "http://mysmartmeals.azurewebsites.net/api/";
        //private static string _ApiUri = "http://169.254.80.80/api/";
        private static string _ImageUri = "https://laboneblob.blob.core.windows.net/mysmartmeals/";
        private static string _UploadImageKey = "DefaultEndpointsProtocol=http;AccountName=laboneblob;AccountKey=OjU7L69fm6t7MZEB+fJK14WIiUDr6KCPAveO0/ccx8CWL9G4FIfjnJ9OkQaBECeHhQYGdSuJqqYU5XFsr6MEWw==;EndpointSuffix=core.windows.net";
        private static int _NewsFeedItemsToRenderRightWay = 0;
        private static int _MyMealsItemsToRenderRightWay = 1;
        public static string ApiUri
        {
            get
            {
                return _ApiUri;
            }
            set
            {
                _ApiUri = value;
            }
        }

        public static string ImageUri
        {
            get
            {
                return _ImageUri;
            }
            set
            {
                _ImageUri = value;
            }
        }

        public static string UploadImageKey
        {
            get
            {
                return _UploadImageKey;
            }
            set
            {
                _UploadImageKey = value;
            }
        }

        public static int NewsFeedItemsToRenderRightWay
        {
            get
            {
                return _NewsFeedItemsToRenderRightWay;
            }
            set
            {
                _NewsFeedItemsToRenderRightWay = value;
            }
        }

        public static int MyMealsItemsToRenderRightWay
        {
            get
            {
                return _MyMealsItemsToRenderRightWay;
            }
            set
            {
                _MyMealsItemsToRenderRightWay = value;
            }
        }
    }
}
