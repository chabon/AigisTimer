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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AigisTimer.Views;


namespace AigisTimer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // field
        


        // property


        // constructor
        public MainWindow()
        {
            InitializeComponent();

            // test
            this.MainPanel.Children.Add( new TestView() );
            this.MainPanel.Children.Add( new TestView() );
            this.MainPanel.Children.Add( new KitchenTimerView() );
        }



    }
}
