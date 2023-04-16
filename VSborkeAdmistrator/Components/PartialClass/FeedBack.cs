using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSborkeAdmistrator.Components
{
    public partial class FeedBack
    {
        public string CountOfStars
        {
            get
            {
                switch (GeneralStars)
                {
                    case 1:
                        return "&#xf005; &#xf006; &#xf006; &#xf006; &#xf006;";
                    case 2:
                        return "&#xf005; &#xf005; &#xf006; &#xf006; &#xf006;";
                    case 3:
                        return "&#xf005; &#xf005; &#xf005; &#xf006; &#xf006;";
                    case 4:
                        return "&#xf005; &#xf005; &#xf005; &#xf005; &#xf006;";
                    case 5:
                        return "&#xf005; &#xf005; &#xf005; &#xf005; &#xf005;";
                }
                return "";
            }
        }
    }
}
