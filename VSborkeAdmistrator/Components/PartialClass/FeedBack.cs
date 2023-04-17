using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VSborkeAdmistrator.Components
{
    public partial class FeedBack
    {
        public string CountOfStars
        {
            get
            {
                //Символы из шрифта FontAwesome. Не отображаются системой, но работают.
                switch (GeneralStars)
                {
                    case 1:
                        return @"    "; //1 полная звезда
                    case 2:
                        return @"    "; //2 полные звезды
                    case 3:
                        return @"    "; //3 полные звезды
                    case 4:
                        return @"    "; //4 полные звезды
                    case 5:
                        return @"    "; //5 полных звезд
                }
                return "6+";
            }
        }

        public Visibility VisibilityAddition
        {
            get
            {
                if(Addition != null)
                {
                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }

        
    }
}
