using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarShowRoom.Entities
{
    public partial class Order
    {

        public double GetTotalPrice
        {
            get
            {
                double total = 0;
                foreach (OrderContent orderContent in OrderContents)
                {
                    total += orderContent.Option.Price;
                }
                return Car.Price + total;
            }
        }

        public Visibility GetVisibility
        {
            get
            {
                if (StatusId != 0)
                    return Visibility.Collapsed;

                return Visibility.Visible;
            }
        }
    }
}
