using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();

     

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New user" });
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                (lbUsers.SelectedItem as User).Name = "Random Name";
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                users.Remove(lbUsers.SelectedItem as User);
        }
        public MainWindow()
        {
            InitializeComponent();
            pnlMainGrid.MouseRightButtonUp += PnlMainGrid_MouseRightButtonUp;

            this.DataContext = this;

            users.Add(new User() { Name = "John Doe" });
            users.Add(new User() { Name = "Jane Doe" });

            lbUsers.ItemsSource = users;
        }

        private void PnlMainGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            label.Content = "MouseRightButtonUp" + e.GetPosition(this).ToString();
        }

        private void pnlMainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            label.Content = "MouseDown" + e.ToString();
        }

        private void pnlMainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            label.Content = "MouseUp" + e.GetPosition(this).ToString();
        }

        private void button_MouseUp(object sender, MouseButtonEventArgs e)
        {





        }

        private void pnlMainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            label.Content = e.GetPosition(this).ToString();
        }

        private void label_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

    }
    
    public class User : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
