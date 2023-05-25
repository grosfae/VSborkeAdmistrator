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
                if (StatusId == 1)
                    return "#FFF9C201";
                if (StatusId == 2)
                    return "#FF09AB04";
                if (StatusId == 3)
                    return "#FF00A7FF";
                if (StatusId == 4)
                    return "#FF0B847C";
                if (StatusId == 5)
                    return "#FF009BC3";
                if (StatusId == 6)
                    return "#FF9304AB";
                if (StatusId == 7)
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
                if (StatusId == 1)
                {
                    if (IsAcceptedOperator == true)
                    {
                        return Visibility.Collapsed;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
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
                if (StatusId == 7)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        public Visibility VisibilityReasonCancelBtn
        {
            get
            {
                if (StatusId == 7 & ReasonReject != null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
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
