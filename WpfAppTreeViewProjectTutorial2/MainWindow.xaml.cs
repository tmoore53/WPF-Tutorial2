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
    ///At the 42:00 minute mark in the video.
    

    
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

        #region On Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //Get every drive on the machine.
            foreach ( var drive in Directory.GetLogicalDrives())
            {
                //Create a new item for it.
                var item = new TreeViewItem();

                
                //Set the header and path.
                item.Header = drive;

                //Set the full path
                item.Tag = drive;

                //Add a empty item.
                item.Items.Add(null);

                //Look out for item being expanded.
                item.Expanded += Folder_Expanded;

                //Add it to the main tree-view.
                FolderView.Items.Add(item);
            }

        }

        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            var item = (TreeViewItem)sender;

            //If the item only contains the empty data
            if (item.Items.Count != 1 && item.Items[0] != null)
                return;

            //Clear empty item
            item.Items.Clear();

            //Get the path.
            var fullpath = (string)item.Tag;

            //Create a empty list.
            var directories = new List<string>();


             //Try and get directories from the folder
             //Ignoring any errors.
            try
            {
                var dirs = Directory.GetDirectories(fullpath);

                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);

                }
            }
            catch (Exception )
            { }

            //
            directories.ForEach(directoryPath =>
            {
                //Create a directory item. 
                var subItem = new TreeViewItem()
                {
                    //Set the header and tag.
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath

                };
                
                //Add a empty item to enable expanding folder.
                subItem.Items.Add(null);

                //Handle expanding recursion.
                subItem.Expanded += Folder_Expanded;


                //Add this subItem to the parent Item
                item.Items.Add(subItem);

            });


        }

        #endregion
        /// <summary>
        /// Find the folder name from path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            //C: \ Something Folder \ a folder
            if (string.IsNullOrEmpty(path))
            return string.Empty;

            //Replace all the forward slashes into back slashes
            // puting two backslashes in so that it doesn't think it is a special
            //character to tab out or net line (\n, \t)
            var normalizedPath = path.Replace('/', '\\');

            //Find the last 
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //If there is no backslash return the path itself
            if (lastIndex <=0)
            return path;
            
            //Return the name after the backslash
            return path.Substring(lastIndex + 1);

        }
    }



}
