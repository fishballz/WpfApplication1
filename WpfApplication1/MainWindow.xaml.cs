using System.Windows;
using System.Windows.Input;
namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pnlMainGrid.MouseRightButtonUp += PnlMainGrid_MouseRightButtonUp;
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
            label.Content = "MouseUp"+ e.GetPosition(this).ToString();
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
}
