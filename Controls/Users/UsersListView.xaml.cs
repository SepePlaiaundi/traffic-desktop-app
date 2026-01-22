using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrafficDesktopApp.Models;

namespace TrafficDesktopApp.Controls.Users
{
    /// <summary>
    /// Lógica de interacción para UsersListView.xaml
    /// </summary>
    public partial class UsersListView : UserControl
    {
        public ObservableCollection<User> VisibleUsers { get; set; } = new ObservableCollection<User>();

        public UsersListView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetUsers(List<User> users)
        {
            VisibleUsers.Clear();
            if (users != null)
            {
                foreach (var user in users)
                {
                    VisibleUsers.Add(user);
                }
            }
        }
    }
}