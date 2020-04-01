using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfAppTreeViewProjectTutorial2
{
    

    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach ( var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();

                FolderView.Items.Add(item);
                item.Header = drive;
            }

        }
    }
    


}
