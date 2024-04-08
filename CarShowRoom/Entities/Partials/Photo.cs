using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShowRoom.Entities
{
    public partial class Photo
    {
        public string GetPhoto
        {
            get
            {
                if (Title is null)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return Directory.GetCurrentDirectory() + @"\Images\" + Title.Trim();
            }
        }
    }
}
