using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VSborkeStorager.Components;

namespace VSborkeStorager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static VSborkeBaseEntities DB = new VSborkeBaseEntities();
        public static User LoggedUser;
        public static bool RejectOrder;
    }
}
