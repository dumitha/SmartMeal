using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Models
{
    public class EventMessage
    {

        //For now lets only support one image per posting (but we need a gallery eventually).
        private List<string> _ImagesList = new List<string>();
        private List<object> _RenderedImagesList = new List<object>();
        public EventMessage()
        {

        }

        public int Id { get; set; }

        public string Type { get; set; }

        public int TypeId { get; set; }

        public int ObjectId { get; set; }
        public DateTime PostedOn { get; set; }

        public string PostedBy { get; set; }

        public string PostedById { get; set; }

        public string UserId { get; set; }

        public string Description { get; set; }

        public List<string> ImageList
        {
            get
            {
                return _ImagesList;
            }
            set
            {
                _ImagesList = value;
            }
        }

        public List<object> RenderedImagesList
        {
            get
            {
                return _RenderedImagesList;
            }
            set
            {
                _RenderedImagesList = value;
            }
        }

    }
}
