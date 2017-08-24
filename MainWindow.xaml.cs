using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace Smash_Character_Database_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Executable_Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static string Character_DB_Path = Executable_Path + "\\ui_character_db.bin";
        private static byte[] Character_DB_Buffer;
        private static byte[] Global_Param_Buffer;
        private static Character[] Unsorted_Characters;
        private static Character[] Characters;
        private static Character Selected_Character;
        private static StackPanel Selected_Character_Panel;
        private static TextBox[] Icon_Boxes = new TextBox[16];
        private static TextBox[] Name_Boxes = new TextBox[16];
        private static bool SwitchingCharacters = false;

        public MainWindow()
        {
            if (File.Exists(Character_DB_Path))
            {
                Character_DB_Buffer = File.ReadAllBytes(Executable_Path + "\\ui_character_db.bin");
                if (Character_DB_Buffer[0x8] == 0x20)
                {
                    int Entries = BitConverter.ToInt32(Character_DB_Buffer.Skip(0x9).Take(4).Reverse().ToArray(), 0);
                    Characters = new Character[Entries];
                    Unsorted_Characters = new Character[Entries];
                    for (int i = 0; i < Entries; i++)
                    {
                        Characters[i] = new Character(Character_DB_Buffer.Skip(0xD + 0x7F * i).Take(0x7F).ToArray());
                        Unsorted_Characters[i] = Characters[i];
                    }
                    // Sort Characters by CSS order
                    Array.Sort(Characters, CompareCharacters);
                }
                else
                {
                    MessageBox.Show("ui_character_db.bin seems to be corrupt! Try re-extracting it from your game data.", "Error");
                    throw new Exception("ui_character_db.bin seems to be corrupt! Try re-extracting it from your game data.");
                }
            }
            else
            {
                MessageBox.Show("Unable to find file ui_character_db.bin! Make sure it is in the same directory as the program.", "Error");
                throw new Exception("Unable to find file ui_character_db.bin! Make sure it is in the same directory as the program.");
            }
            
            InitializeComponent();

            // Add ItemSource for ComboBoxes
            CosmeticId.ItemsSource = CharacterInfo.Cosmetic_Names.Values;
            SeriesIcon.ItemsSource = CharacterInfo.Series_Names.Values;

            // Generate Character Icon and Name ID Panels
            for (int i = 1; i < 0x11; i++)
            {
                // Icon Portion
                StackPanel Icon_Panel = new StackPanel
                {
                    Height = 24,
                    Width = 150,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Orientation = Orientation.Horizontal
                };
                Label Icon_Index = new Label
                {
                    Height = 24,
                    Width = 30,
                    Content = i.ToString(),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                TextBox Icon_Value = new TextBox
                {
                    Height = 24,
                    Width = 30,
                    Text = "0",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Tag = i - 1
                };

                Icon_Value.PreviewTextInput += HandlePreviewTextInput;
                Icon_Value.TextChanged += new TextChangedEventHandler((object sender, TextChangedEventArgs e) => IconValue_TextChanged(sender as TextBox, (int)Icon_Value.Tag));

                Icon_Boxes[i - 1] = Icon_Value;
                Icon_Panel.Children.Add(Icon_Index);
                Icon_Panel.Children.Add(Icon_Value);
                Icon_Panel.UpdateLayout();
                IconIDPanel.Children.Add(Icon_Panel);

                // Name Portion
                StackPanel Name_Panel = new StackPanel
                {
                    Height = 24,
                    Width = 150,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Orientation = Orientation.Horizontal
                };
                Label Name_Index = new Label
                {
                    Height = 24,
                    Width = 30,
                    Content = i.ToString(),
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                TextBox Name_Value = new TextBox
                {
                    Height = 24,
                    Width = 30,
                    Text = "0",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Tag = i - 1
                };

                Name_Value.PreviewTextInput += HandlePreviewTextInput;
                Name_Value.TextChanged += new TextChangedEventHandler((object sender, TextChangedEventArgs e) => NameValue_TextChanged(sender as TextBox, (int)Name_Value.Tag));

                Name_Boxes[i - 1] = Name_Value;
                Name_Panel.Children.Add(Name_Index);
                Name_Panel.Children.Add(Name_Value);
                Name_Panel.UpdateLayout();
                NameIDPanel.Children.Add(Name_Panel);
            }

            // Generate Fighter Selection Panels
            for (int i = 3; i < Characters.Length; i++)
            {
                if (Characters[i].ID != 0xFFFFFFFF)
                {
                    Character Current_Character = Characters[i];
                    StackPanel Character_Panel = new StackPanel
                    {
                        Height = 30,
                        Width = 200,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Orientation = Orientation.Horizontal
                    };
                    Image Character_Image = new Image
                    {
                        Height = 30,
                        Width = 30,
                        Source = Current_Character.Character_Image
                    };
                    Label Character_Name = new Label
                    {
                        Height = 30,
                        Width = 170 - SystemParameters.ThinVerticalBorderWidth,
                        Content = Current_Character.Name,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center
                    };

                    // Set Default Selection of Mario
                    if (i == 3)
                        SetSelectedFighter(Current_Character, Character_Panel);

                    Character_Panel.Children.Add(Character_Image);
                    Character_Panel.Children.Add(Character_Name);
                    Character_Panel.UpdateLayout();
                    Character_Panel.PreviewMouseLeftButtonUp += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) => SetSelectedFighter(Current_Character, Character_Panel));
                    CharacterStackPanel.Children.Add(Character_Panel);
                }
            }

            DataObject.AddPastingHandler(CharacterSlotsTextBox, OnPaste);
        }

        private static int CompareCharacters(Character A, Character B)
        {
            if (A == null)
            {
                if (B == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (B == null)
                {
                    return 1;
                }
                else
                {
                    return A.CSS_Position.CompareTo(B.CSS_Position);
                }
            }
        }

        private void SetSelectedFighter(Character Fighter, StackPanel Character_Panel = null)
        {
            SwitchingCharacters = true;
            if (Selected_Character_Panel != null)
            {
                Selected_Character_Panel.Background = Brushes.Transparent;
            }
            if (Character_Panel != null)
            {
                Character_Panel.Background = Brushes.LightGray;
            }
            Selected_Character = Fighter;
            Selected_Character_Panel = Character_Panel;
            SelectedFighterImage.Source = Fighter.Character_Image;
            SelectedFighterLabel.Content = string.Format("{0} [Entry {1}]", Fighter.Name, Fighter.ID);
            // TODO: Finish Reloading Fighter Data
            CharacterSlotsTextBox.Text = Fighter.Character_Slots.ToString();
            ShowOnCSSCheckBox.IsChecked = Fighter.Show_on_CSS;
            IsDLCCheckBox.IsChecked = Fighter.Is_DLC;
            CSSPositionTextBox.Text = Fighter.CSS_Position.ToString();

            if (CharacterInfo.Cosmetic_Names.ContainsKey(Fighter.Cosmetic_ID))
            {
                CosmeticId.SelectedIndex = Array.IndexOf(CharacterInfo.Cosmetic_Names.Keys.ToArray(), Fighter.Cosmetic_ID);
            }

            if (CharacterInfo.Series_Names.ContainsKey(Fighter.Series_Icon))
            {
                SeriesIcon.SelectedIndex = Array.IndexOf(CharacterInfo.Series_Names.Keys.ToArray(), Fighter.Series_Icon);
            }

            string Image_Path = Executable_Path + "\\Series Icons\\" + CharacterInfo.Series_Names.Values.ToArray()[SeriesIcon.SelectedIndex] + ".png";
            if (File.Exists(Image_Path))
            {
                SeriesIconImage.Source = new BitmapImage(new Uri(Image_Path));
            }
            else
            {
                MessageBox.Show(Image_Path);
            }

            for (int i = 0; i < 16; i++)
            {
                try
                {
                    Icon_Boxes[i].Text = Fighter.Slot_Icon_IDs[i].ToString();
                    Name_Boxes[i].Text = Fighter.Slot_Name_IDs[i].ToString();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace + " | " + e.Message);
                }
            }
            SwitchingCharacters = false;
        }

        private static bool IsTextAllowed(string text)
        {
            return !new Regex("[^0-9.-]+").IsMatch(text);
        }

        private void HandlePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void CharacterSlots_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Selected_Character != null && int.TryParse(CharacterSlotsTextBox.Text, out int Slots))
            {
                if (Slots > 255)
                {
                    CharacterSlotsTextBox.Text = "255";
                    Selected_Character.Character_Slots = 255;
                }
                else
                {
                    Selected_Character.Character_Slots = Slots;
                }
            }
        }

        private void CSSPosition_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Selected_Character != null && int.TryParse(CSSPositionTextBox.Text, out int Slots))
            {
                if (Slots > 255)
                {
                    CSSPositionTextBox.Text = "255";
                    Selected_Character.CSS_Position = 255;
                }
                else
                {
                    Selected_Character.CSS_Position = (byte)Slots;
                }
            }
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void ShowOnCSS_Checked(object sender, EventArgs e)
        {
            Selected_Character.Show_on_CSS = true;
        }

        private void ShowOnCSS_Unchecked(object sender, EventArgs e)
        {
            Selected_Character.Show_on_CSS = false;
        }

        private void IsDLC_Checked(object sender, EventArgs e)
        {
            Selected_Character.Is_DLC = true;
        }

        private void IsDLC_Unchecked(object sender, EventArgs e)
        {
            if ((Selected_Character.ID > 50 && Selected_Character.ID < 58) && !(bool)IsDLCCheckBox.IsChecked)
            {
                
                MessageBoxResult Choice = MessageBox.Show("The Character you are editing is a paid DLC character. If you have not purchased this character, please consider doing so to support the creators! Do you wish to continue?",
                    "Anti-Piracy Warning", MessageBoxButton.YesNo);
                if (Choice == MessageBoxResult.No || Choice == MessageBoxResult.Cancel)
                {
                    IsDLCCheckBox.IsChecked = true;
                }
                else
                {
                    Selected_Character.Is_DLC = false;
                }
            }
            else
            {
                Selected_Character.Is_DLC = false;
            }
        }

        private void IconValue_TextChanged(TextBox sender, int Index)
        {
            if (Selected_Character != null && byte.TryParse(sender.Text, out byte ID))
            {
                Selected_Character.Slot_Icon_IDs[Index] = ID;
            }
        }

        private void NameValue_TextChanged(TextBox sender, int Index)
        {
            if (Selected_Character != null && byte.TryParse(sender.Text, out byte ID))
            {
                Selected_Character.Slot_Name_IDs[Index] = ID;
            }
        }

        private void CosmeticId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Selected_Character != null && !SwitchingCharacters)
            {
                if (CosmeticId.SelectedIndex > -1)
                {
                    Selected_Character.Cosmetic_ID = CharacterInfo.Cosmetic_Names.Keys.ToArray()[CosmeticId.SelectedIndex];
                }
            }
        }

        private void SeriesId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Selected_Character != null && !SwitchingCharacters)
            {
                if (SeriesIcon.SelectedIndex > -1)
                {
                    Selected_Character.Series_Icon = CharacterInfo.Series_Names.Keys.ToArray()[SeriesIcon.SelectedIndex];
                    string Image_Path = Executable_Path + "\\Series Icons\\" + CharacterInfo.Series_Names.Values.ToArray()[SeriesIcon.SelectedIndex] + ".png";
                    if (File.Exists(Image_Path))
                    {
                        SeriesIconImage.Source = new BitmapImage(new Uri(Image_Path));
                    }
                    else
                    {
                        MessageBox.Show(Image_Path);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Characters.Length; i++)
            {
                Unsorted_Characters[i].Write(ref Character_DB_Buffer, 0xD + 0x7F * i);
            }
            if (File.Exists(Executable_Path + "\\ui_character_db.bin"))
            {
                using (FileStream Stream = new FileStream(Executable_Path + "\\ui_character_db.bin", FileMode.Open))
                {
                    using (BinaryWriter Writer = new BinaryWriter(Stream))
                    {
                        Writer.Write(Character_DB_Buffer);
                    }
                }
            }
        }
    }
}
