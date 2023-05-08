using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VSborkeAdmistrator.Components
{
    public partial class Order
    {
        public string StatusColor
        {
            get
            {
                if (Status.Id == 1)
                    return "#FFF9C201";
                if (Status.Id == 2)
                    return "#FF09AB04";
                if (Status.Id == 3)
                    return "#FF00A7FF";
                if (Status.Id == 4)
                    return "#FF0B847C";
                if (Status.Id == 5)
                    return "#FF009BC3";
                if (Status.Id == 6)
                    return "#FF9304AB";
                if (Status.Id == 7)
                    return "#FFCC0606";
                else
                    return "";

            }
        }

        public int PriceDiscountOfCount
        {
            get
            {
                if (Discount == 0)
                {
                    return PricePerUnit;
                }
                else
                {
                    return PricePerUnit * (100 - Discount) / 100;
                }
            }
        }

        public Visibility VisibilityDiscount
        {
            get
            {
                if (Discount == 0)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility VisibilityAcceptBtn
        {
            get
            {
                if (Status.Id == 1)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility VisibilityCancelBtn
        {
            get
            {
                if (Status.Id == 7)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        public Visibility VisibilityComment
        {
            get
            {
                if (CommentOrder != null)
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
