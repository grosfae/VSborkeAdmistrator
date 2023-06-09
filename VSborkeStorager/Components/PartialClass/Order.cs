using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VSborkeStorager.Components
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

        public Visibility AcceptedByOperator
        {
            get
            {
                if (IsAcceptedOperator == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility NotReadyStatus
        {
            get
            {
                if (IsReadyMaster == null || IsReadyMaster == false)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility RejectByMaster
        {
            get
            {
                if (IsRejectedMaster == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility VisibilityAcceptBtn
        {
            get
            {
                if (StatusId == 1)
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
                if (string.IsNullOrWhiteSpace(CommentOrder))
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility VisibilityStorageBtn
        {
            get
            {
                if(IsPocketed == true & IsStoraged != true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility VisibilityPocketIndicator
        {
            get
            {
                if (IsPocketed == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility VisibilityNotPocketBtn
        {
            get
            {
                if (IsForStorager == true & IsPocketed != true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }


        public Visibility CountForStatusStorage
        {
            get
            {
                if (StatusId == 4)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public Visibility GeneralCountStorage
        {
            get
            {
                if (StatusId == 4)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility NeedTakeToDelivery
        {
            get
            {
                if (IsForDeliveler != true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
        }

        public Visibility TookToDelivery
        {
            get
            {
                if (IsForDeliveler == true)
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
