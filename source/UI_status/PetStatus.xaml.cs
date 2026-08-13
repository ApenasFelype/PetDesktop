using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PetDesktop.source.UI_status
{
    /// <summary>
    /// Lógica interna para PetStatus.xaml
    /// </summary>
    public partial class PetStatus : Window
    {
        private PetNeeds Needs;
        private DispatcherTimer? timer;
        public PetStatus(PetNeeds needs)
        {
            InitializeComponent();

            this.Needs = needs;

            
        }

        public void UpdateControl()
        {
            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += UpdateBar;

            timer.Start();
        }

        public void UpdateBar(object? sender, EventArgs e)
        {
            HungerBar.Value = Needs.Hunger;
            ThirstBar.Value = Needs.Thirst;
            SleepBar.Value = Needs.Sleep;
        }

        private void FeedClick(object sender, RoutedEventArgs e)
        {
            Needs.Feed();
        }

        private void WaterClick(object sender, RoutedEventArgs e)
        {
            Needs.Drink();
            
        }

        private void SleepClick(Object sender, RoutedEventArgs e)
        {
            Needs.Rest();
            
        }
    }
}
