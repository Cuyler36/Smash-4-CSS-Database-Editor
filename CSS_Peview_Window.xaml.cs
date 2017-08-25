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

namespace Smash_Character_Database_Editor
{
    /// <summary>
    /// Interaction logic for CSS_Peview_Window.xaml
    /// </summary>
    public partial class CSS_Peview_Window : Window
    {
        private MainWindow Window;

        public CSS_Peview_Window(MainWindow Handle, Character[] Characters)
        {
            Window = Handle;
            InitializeComponent();
            RootGrid.HorizontalAlignment = HorizontalAlignment.Left;
            GenerateCSS_Preview(Characters);
        }

        private void GenerateCSS_Preview(Character[] Characters)
        {
            Character[] CSS_Members = Characters.Where(o => (o.ID < 60 && o.Show_on_CSS) || o.Name.Equals("Random")).ToArray();
            int Total_CSS_Members = CSS_Members.Length;
            int Rows = Total_CSS_Members / 14 + 1;
            int i = 0;

            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < (y + 1 == Rows ? Total_CSS_Members % 14 : 14); x++)
                {
                    Border Character_Border = new Border
                    {
                        Width = 64,
                        Height = 32,
                        Margin = new Thickness(10 + x * 63, 10 + y * 31, -620, -620),
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1, 1, 1, 1),
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top
                    };

                    Image CSP = new Image
                    {
                        Width = 32,
                        Height = 32,
                        Source = CSS_Members[i].Character_Image
                    };

                    Character_Border.Child = CSP;
                    RootGrid.Children.Add(Character_Border);
                    i++;
                }
            }

            RootGrid.UpdateLayout();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.Show();   
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Window.Show();
        }
    }
}
