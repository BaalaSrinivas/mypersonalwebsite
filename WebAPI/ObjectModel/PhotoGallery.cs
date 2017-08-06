using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectModel
{
    public class PhotoGallery
    {
        public Guid id;
        public string name;
        public byte[] image;
        public string imageUrl;
        public bool enabled;
    }
}
