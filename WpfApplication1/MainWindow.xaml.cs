using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.IO;
using System.Linq;
using System.Windows.Controls;
namespace WpfApplication1
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public MainWindow()
        {
            InitializeComponent();
            users.Add(new User() { Name = "John Doe" });
            users.Add(new User() { Name = "Jane Doe" });

            lbUsers.ItemsSource = users;
            List<User2> items = new List<User2>();
                        items.Add(new User2() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
                        items.Add(new User2() { Name = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
                        items.Add(new User2() { Name = "Sammy Doe", Age = 13, Mail = "sammy.doe@gmail.com" });
                        lvDataBinding.ItemsSource = items;
                        lvDataBinding2.ItemsSource = items;
            lvUsers.ItemsSource = items;

            List<Person> persons = new List<Person>();
            Person person1 = new Person() { Name = "John Doe", Age = 42 };

            Person person2 = new Person() { Name = "Jane Doe", Age = 39 };

            Person child1 = new Person() { Name = "Sammy Doe", Age = 13 };
            person1.Children.Add(child1);
            person2.Children.Add(child1);

            person2.Children.Add(new Person() { Name = "Jenny Moe", Age = 17 });

            Person person3 = new Person() { Name = "Becky Toe", Age = 25 };

            persons.Add(person1);
            persons.Add(person2);
            persons.Add(person3);

            person2.IsExpanded = true;
            person2.IsSelected = true;

            trvPersons.ItemsSource = persons;

            //drei
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfo in drives)
                trvStructure.Items.Add(CreateTreeItem(driveInfo));
            //vier
            List<User> users3 = new List<User>();
            users3.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
            users3.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
            users3.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });

            dgSimple.ItemsSource = users3;
        }

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
        
        private void btnSelectNext_Click(object sender, RoutedEventArgs e)
        {
            if (trvPersons.SelectedItem != null)
            {
                var list = (trvPersons.ItemsSource as List<Person>);
                int curIndex = list.IndexOf(trvPersons.SelectedItem as Person);
                if (curIndex >= 0)
                    curIndex++;
                if (curIndex >= list.Count)
                    curIndex = 0;
                if (curIndex >= 0)
                    list[curIndex].IsSelected = true;
            }
        }
        //drei
        private void btnToggleExpansion_Click(object sender, RoutedEventArgs e)
        {
            if (trvPersons.SelectedItem != null)
                (trvPersons.SelectedItem as Person).IsExpanded = !(trvPersons.SelectedItem as Person).IsExpanded;
        }
        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if ((item.Items.Count == 1) && (item.Items[0] is string))
            {
                item.Items.Clear();

                DirectoryInfo expandedDir = null;
                if (item.Tag is DriveInfo)
                    expandedDir = (item.Tag as DriveInfo).RootDirectory;
                if (item.Tag is DirectoryInfo)
                    expandedDir = (item.Tag as DirectoryInfo);
                try
                {
                    foreach (DirectoryInfo subDir in expandedDir.GetDirectories())
                        item.Items.Add(CreateTreeItem(subDir));
                }
                catch { }
            }
        }

        private TreeViewItem CreateTreeItem(object o)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = o.ToString();
            item.Tag = o;
            item.Items.Add("Loading...");
            return item;
        }
        //vier
        public class User
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public DateTime Birthday { get; set; }
        }
    }


    public class User2
    {
        public int Age { get; internal set; }
        public string Mail { get;  set; }
        public string Name { get;  set; }
        public override string ToString()
        {
            return this.Name + ", " + this.Age + " years old, email is " +  this.Mail;
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
                    NotifyPropertyChanged("Name");
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

    public class Person : TreeViewItemBase
    {
        public Person()
        {
            this.Children = new ObservableCollection<Person>();
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public ObservableCollection<Person> Children { get; set; }
    }

    public class TreeViewItemBase : INotifyPropertyChanged
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                    NotifyPropertyChanged("IsExpanded");
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
