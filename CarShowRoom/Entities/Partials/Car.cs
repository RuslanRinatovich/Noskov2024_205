using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarShowRoom.Entities
{
    public partial class Car
    {
        List<Photo> photos = null;
        int index = 0;
        public string GetCurrentPhoto
        {
            get
            {
                if (photos == null)
                    photos = Photos.ToList();
                if (photos.Count == 0)
                    return Directory.GetCurrentDirectory() + @"\Images\picture.png";
                return photos[index].GetPhoto;
            }
        }

        public int ReloadPhotos
        {
            set
            {
                if (photos != null)
                {
                    photos = null;
                    photos = Photos.ToList();
                }
            }
        }

        public int SetPrevPhoto
        {
            set
            {
                if (index == 0)
                    return;
                index--;
            }
        }

        public int SetNextPhoto
        {
            set
            {
                if (index == photos.Count - 1)
                    return;
                index++;
            }
        }

        public string GetInfo
        {
            get
            {

                return $"{EngineCapacity:N1} {HorsePower} л.с. {Transmission.Title}";
            }
        }
        public Visibility GetVisibility
        {
            get
            {
                if (Manager.CurrentUser == null)
                    return Visibility.Collapsed;
                if (Manager.CurrentUser.RoleId < 2)
                    return Visibility.Collapsed;
                return Visibility.Visible;
            }
        }

    }
}
