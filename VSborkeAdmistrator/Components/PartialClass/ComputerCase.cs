using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VSborkeAdmistrator.Components
{
    public partial class ComputerCase
    {
        public string FullName
        {
            get
            {
                if (Name != null)
                    return $"Корпус {Name} {PrimaryColor.Name} [{eATX}{flexATX}{microATX}{miniDTX}{miniITX}{ssiCEB}{ssiEEB}{standartATX}{thinminiITX}{xlATX}{Weight} кг]";
                else
                    return "Данные не заполнены";

            }
        }
        public string eATX
        {
            get
            {
                if (EAtx == true)
                    return "E-ATX, ";
                else
                    return "";

            }
        }
        public string flexATX
        {
            get
            {
                if (FlexAtx == true)
                    return "Flex-ATX, ";
                else
                    return "";

            }
        }
        public string microATX
        {
            get
            {
                if (MicroAtx == true)
                    return "Micro-ATX, ";
                else
                    return "";

            }
        }
        public string miniDTX
        {
            get
            {
                if (MiniDtx == true)
                    return "Mini-DTX, ";
                else
                    return "";

            }
        }
        public string miniITX
        {
            get
            {
                if (MiniItx == true)
                    return "Mini-ITX, ";
                else
                    return "";

            }
        }
        public string ssiCEB
        {
            get
            {
                if (SsiCeb == true)
                    return "SSI-CEB, ";
                else
                    return "";

            }
        }
        public string ssiEEB
        {
            get
            {
                if (SsiEeb == true)
                    return "SSI-EEB, ";
                else
                    return "";

            }
        }
        public string standartATX
        {
            get
            {
                if (StandartAtx == true)
                    return "Standart-ATX, ";
                else
                    return "";

            }
        }
        public string thinminiITX
        {
            get
            {
                if (ThinMiniItx == true)
                    return "Thin Mini-ITX, ";
                else
                    return "";

            }
        }
        public string xlATX
        {
            get
            {
                if (XlAtx == true)
                    return "XL-ATX, ";
                else
                    return "";

            }
        }

        public string Accesable
        {
            get
            {
                if (IsAccessable == false)
                    return "Корпус недоступен";
                else
                    return "";
            }
        }
        public string ForegroundColor
        {
            get
            {
                if (Discount == 0)
                    return "Black";
                else
                    return "#FF3881EF";
            }
        }
        public int PriceDiscount
        {
            get
            {
                if (Discount == 0)
                    return Price;
                else
                    return Price * (100-Discount)/100;

            }
        }
        public Visibility VisibilityDiscount
        {
            get
            {
                if (Discount == 0)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        public Visibility VisibilityBtnOrder
        {
            get
            {
                if (IsAccessable == false)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        public Visibility VisibilityBtnNortific
        {
            get
            {
                if (IsAccessable == true)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }
        
        
        public Visibility VisibilityAdd
        {
            get
            {
                var favourite = App.DB.Favourite.FirstOrDefault(x => x.UserId == App.LoggedUser.Id & x.ComputerCaseId == Id);
                if (favourite != null)
                {
                    if (favourite.UserId == App.LoggedUser.Id & favourite.ComputerCaseId == Id)
                        return Visibility.Collapsed;
                    else
                        return Visibility.Visible;
                }
                else
                    return Visibility.Visible;
               
                
            }
        }

        public Visibility VisibilityRemove
        {
            get
            {
                var favourite = App.DB.Favourite.FirstOrDefault(x => x.UserId == App.LoggedUser.Id & x.ComputerCaseId == Id);
                if (favourite != null)
                {
                    if (favourite.UserId == App.LoggedUser.Id & favourite.ComputerCaseId == Id)
                        return Visibility.Visible;
                    else
                        return Visibility.Collapsed;
                }
                else
                    return Visibility.Collapsed;
            }
        }

    }
}
