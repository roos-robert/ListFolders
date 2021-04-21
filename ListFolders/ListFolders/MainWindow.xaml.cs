using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ListFolders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool pathIsSet { get; set; }
        public string pathToList { get; set; }
        public List<string> dirs = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            if(!pathIsSet)
            {
                listButton.Visibility = Visibility.Hidden;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var d in Directory.GetDirectories(pathToList))
            {
                var dir = new DirectoryInfo(d);
                var dirName = dir.Name;

                dirs.Add(dir.Name);
            }

            await File.WriteAllLinesAsync("ListadeMappar.txt", dirs);

            statusTextBlock.Text = "Listan är nu skapad!";

            Process.Start("notepad.exe", "ListadeMappar.txt");

            var a = 1 + 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                pathTextBlock.Text = String.Format("Du har valt: {0}", dialog.FileName);
                pathIsSet = true;
                pathToList = dialog.FileName;
                listButton.Visibility = Visibility.Visible;
            }
            else
            {
                pathTextBlock.Text = "Något gick fel, försök igen. Kom ihåg att välja en mapp och inte en fil i en mapp";
                pathIsSet = false;
            }
        }
    }
}
