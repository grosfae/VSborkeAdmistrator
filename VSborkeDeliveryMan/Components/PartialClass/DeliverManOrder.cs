using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VSborkeDeliveryMan.Components
{
    public partial class DeliverManOrder
    {
        public string StatusColor
        {
            get
            {
                if (DeliveryStatusId == 1)
                    return "#FFF9C201";
                if (DeliveryStatusId == 2)
                    return "#FF09AB04";
                if (DeliveryStatusId == 3)
                    return "#FF9304AB";
                if (DeliveryStatusId == 4)
                    return "#FFCC0606";
                else
                    return "";

            }
        }
        public Visibility VisibilityShipmentBtn
        {
            get
            {
                if (DeliveryStatusId == 1)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility VisibilityFinishBtn
        {
            get
            {
                if (DeliveryStatusId == 2)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }
    }
}
