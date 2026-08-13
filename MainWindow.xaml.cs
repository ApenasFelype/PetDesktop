using PetDesktop.source.UI_status;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PetDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PetController controller;
        public PetStatus StatusWindow;

        public MainWindow()
        {
            InitializeComponent();

            Left = (SystemParameters.WorkArea.Width - Width) / 2;
            Top = (SystemParameters.WorkArea.Height - Height) / 2;

            controller = new PetController(this);
            controller.ControllerMain();
            
        }

        private void CatMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                controller.StartDragging();
                
                DragMove();
                

                controller.StopDragging();
            }
        }

        private void CatRightClick(Object sender, MouseButtonEventArgs e)
        {
            StatusWindow = new PetStatus(controller.Needs);
            StatusWindow.UpdateControl();
            StatusWindow.Show();

            e.Handled = true;
        }
         
    }
}