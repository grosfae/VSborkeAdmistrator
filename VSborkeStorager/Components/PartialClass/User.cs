using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSborkeStorager.Components
{
    public partial class User
    {
        public string ReviewName
        {
            get
            {
                return $"{Name} {Surname}";

            }
        }

        public string Color
        {
            get
            {
                if (IsBanned == true)
                    return "Black";
                else
                    return "White";

            }
        }
        public string BannedTextVisibility
        {
            get
            {
                if (IsBanned == true)
                    return "Visible";
                else
                    return "Collapsed";

            }
        }

        public string ColorForeground
        {
            get
            {
                if (IsBanned == true)
                    return "White";
                else
                    return "Black";

            }
        }
        public string BtnRedVisibility
        {
            get
            {
                if (IsBanned == true)
                    return "Collapsed";
                else
                    return "Visible";

            }
        }

        public string BtnGreenVisibility
        {
            get
            {
                if (IsBanned == true)
                    return "Visible";
                else
                    return "Collapsed";

            }
        }

        public string PbVisibility
        {
            get
            {
                if (Id == App.LoggedUser.Id)
                    return "Visible";
                else
                    return "Collapsed";

            }
        }
    }
}
