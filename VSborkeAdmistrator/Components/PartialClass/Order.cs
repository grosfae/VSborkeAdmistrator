using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
