using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Necesario para ObservableCollection
using System.Windows;
using System.Windows.Controls;
using TrafficDesktopApp.Models;
using TrafficDesktopApp.Services;
using TrafficDesktopApp.Windows; // Necesario para EditUserWindow

namespace TrafficDesktopApp.Controls.Users
{
    /// <summary>
    /// Lógica de interacción para UsersListView.xaml
    /// </summary>
    public partial class UsersListView : UserControl
    {
        // Esta es la colección que lee el XAML
        public ObservableCollection<User> VisibleUsers { get; set; } = new ObservableCollection<User>();

        public UsersListView()
        {
            InitializeComponent();
            // Importante: DataContext = this permite que el XAML vea "VisibleUsers"
            DataContext = this;
        }

        // --- MÉTODO QUE FALTABA (SetUsers) ---
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

        private async void SaveUser_Click(object sender, RoutedEventArgs e)
        {
            var user = (sender as Button)?.DataContext as User;
            if (user == null) return;

            await UsersService.UpdateUserAsync(user);
        }
    }
}