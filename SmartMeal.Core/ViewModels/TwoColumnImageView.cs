using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.ViewModels
{
    public class TwoColumnImageView
    {

        public string ImageOneUrl { get; set; }

        public object RenderedImageOne { get; set; }

        public string ImageTwoUrl { get; set; }

        public object RenderedImageTwo { get; set; }
    }
}
