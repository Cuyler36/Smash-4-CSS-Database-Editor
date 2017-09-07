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
using System.Text.RegularExpressions;

namespace Smash_Character_Database_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Executable_Path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        public static string Character_DB_Path = Executable_Path + "\\ui_character_db.bin";
        private static byte[] Character_DB_Buffer;
        private static byte[] Global_Param_Buffer;
        private static Character[] Unsorted_Characters;
        private static Character[] Characters;
        private static int Entries;
        private static Character Selected_Character;
        private static StackPanel Selected_Character_Panel;
        private static TextBox[] Icon_Boxes = new TextBox[16];
        private static TextBox[] Name_Boxes = new TextBox[16];
        private static bool SwitchingCharacters = false;
        private static bool AllowAutoSort = true;
        private static System.Windows.Forms.FolderBrowserDialog SmashExplorer_Browser = new System.Windows.Forms.FolderBrowserDialog();
        private static System.Windows.Forms.SaveFileDialog SaveAsDialog = new System.Windows.Forms.SaveFileDialog();

        public MainWindow()
        {
            if (File.Exists(Character_DB_Path))
            {
                Character_DB_Buffer = File.ReadAllBytes(Executable_Path + "\\ui_character_db.bin");
                if (Character_DB_Buffer[0x8] == 0x20)
                {
                    if(MessageBox.Show("Would you like to make a backup of your ui_character_db.bin file? This backup will overwrite any previous backup!", "File Backup", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            FileStream Backup = File.Create(Executable_Path + "\\ui_character_db_backup.bin");
                            Backup.Write(Character_DB_Buffer, 0, Character_DB_Buffer.Length);
                            Backup.Flush();
                            Backup.Close();
                        } catch { MessageBox.Show("Unable to make backup!"); }
                    }
                    Entries = BitConverter.ToInt32(Character_DB_Buffer.Skip(0x9).Take(4).Reverse().ToArray(), 0);
                    Characters = new Character[Entries];
                    Unsorted_Characters = new Character[Entries];
                    for (int i = 0; i < Entries; i++)
                    {
                        Characters[i] = new Character(Character_DB_Buffer.Skip(0xD + 0x7F * i).Take(0x7F).ToArray());
                        Unsorted_Characters[i] = Characters[i];
                    }
<<<<<<< HEAD
                    // Name Random & All Miis
                    Characters[0].Name = "Random";
                    Characters[59].Name = "All Miis";
                    try
                    {
                        Characters[59].Character_Image = new BitmapImage(new Uri(Executable_Path + "\\Icons\\All Miis.png"));
                    }
                    catch { }

                    // Sort Characters by CSS order
                    SortCharacters();

                    // Setup FolderBrowserDialog
                    SmashExplorer_Browser.SelectedPath = Executable_Path;
                    SmashExplorer_Browser.Description = "Select Sm4shExplorer Folder";

                    // Check for fighters with the same CSS position
                    Check_Same_Positions(false);
=======
                    Characters[0].Name = "Random";
                    // Sort Characters by CSS order
                    SortCharacters();
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
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
            GenerateCharacterPanels();

            DataObject.AddPastingHandler(CharacterSlotsTextBox, OnPaste);
<<<<<<< HEAD
=======

            //new CSS_Peview_Window(Characters).Show();
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
        }

        private void GenerateCharacterPanels()
        {
            CharacterStackPanel.Children.Clear();
<<<<<<< HEAD
            for (int i = 0; i < Characters.Length; i++)
            {
                if ((Characters[i].ID != 0xFFFFFFFF && Characters[i].Playable == 1) || Characters[i].Name.Equals("Random") || Characters[i].Name.Equals("All Miis")) // 0 is random
=======
            for (int i = 3; i < Characters.Length; i++)
            {
                if (Characters[i].ID != 0xFFFFFFFF || Characters[i].Name == "Random") // 0 is random
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
                {
                    Character Current_Character = Characters[i];
                    StackPanel Character_Panel = new StackPanel
                    {
                        Tag = Characters[i].Name,
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

                    // Set Default Selection
<<<<<<< HEAD
                    if ((Selected_Character != null && Selected_Character.Name.Equals(Characters[i].Name)))
                        SetSelectedFighter(Current_Character, Character_Panel);
                    else if (Selected_Character == null && Characters[i].ID == 3)
=======
                    if ((Selected_Character != null && Selected_Character.Name.Equals(Characters[i].Name)) || Characters[i].ID == 3)
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
                        SetSelectedFighter(Current_Character, Character_Panel);

                    Character_Panel.Children.Add(Character_Image);
                    Character_Panel.Children.Add(Character_Name);
                    Character_Panel.UpdateLayout();
                    Character_Panel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler((object sender, MouseButtonEventArgs e) => CharacterPanel_MouseDown(Current_Character, Character_Panel));
                    Character_Panel.MouseDown += new MouseButtonEventHandler((object s, MouseButtonEventArgs e) => CharacterEntry_MouseDown(s, e, Selected_Character));
                    Character_Panel.MouseUp += CharacterEntry_MouseUp;
                    CharacterStackPanel.Children.Add(Character_Panel);
                }
            }
        }

        private void SortCharacters()
        {
            Array.Sort(Characters, CompareCharacters);
<<<<<<< HEAD

            // Put Random at the end (since changing it's CSS Position won't change how smash positions it)
            Character Random = null;
            int Random_Location = 0;
=======
            // Put Random at the end (since changing it's CSS Position won't change how smash positions it)
            Character End_Character = Characters[Characters.Length - 1];
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
            for (int i = 0; i < Characters.Length; i++)
            {
                if (Characters[i].Name.Equals("Random"))
                {
<<<<<<< HEAD
                    Random = Characters[i];
                    Random_Location = i;
                    break;
                }
            }

            if (Random != null)
            {
                for (int i = Random_Location + 1; i < Characters.Length; i++)
                {
                    Characters[i - 1] = Characters[i];
                }
                Characters[Characters.Length - 1] = Random;
            }

            // Put All Miis in front of random
            Random = null;
            for (int i = 0; i < Characters.Length; i++)
            {
                if (Characters[i].Name.Equals("All Miis"))
                {
                    Random = Characters[i];
                    Random_Location = i;
                    break;
                }
            }
            if (Random != null)
            {
                for (int i = Random_Location + 1; i < Characters.Length; i++)
                {
                    Characters[i - 1] = Characters[i];
                }
                Characters[Characters.Length - 2] = Random;
            }
=======
                    Characters[Characters.Length - 1] = Characters[i];
                    Characters[i] = End_Character;
                    break;
                }
            }
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
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

        private static bool Can_Move(Character Fighter)
        {
            return !Fighter.Name.Equals("All Miis") && !Fighter.Name.Equals("Random");
        }

        private void CharacterPanel_MouseDown(Character Fighter, StackPanel Panel)
        {
            if (!Dragging)
            {
                SetSelectedFighter(Fighter, Panel);
            }
        }

        private void SetSelectedFighter(Character Fighter, StackPanel Character_Panel = null)
        {
            SwitchingCharacters = true;
            if (Selected_Character_Panel != null)
            {
                Selected_Character_Panel.Background = Brushes.Transparent;
                Selected_Character_Panel.UpdateLayout();
            }
            if (Character_Panel != null)
            {
                Character_Panel.Background = Brushes.LightGray;
                Character_Panel.UpdateLayout();
            }
            Selected_Character = Fighter;
            Selected_Character_Panel = Character_Panel;
            SelectedFighterImage.Source = Fighter.Character_Image;
            SelectedFighterLabel.Content = string.Format("{0} [ID {1}]", Fighter.Name, Fighter.ID);
<<<<<<< HEAD
=======
            // TODO: Finish Reloading Fighter Data
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
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
                try
                {
                    SeriesIconImage.Source = new BitmapImage(new Uri(Executable_Path + "\\Series Icons\\Unused.png"));
                }
                catch { MessageBox.Show("Couldn't locate a valid Series Icon image. Make sure you have the Series Icons folder in the same place as the program!"); };
            }

            for (int i = 0; i < 16; i++)
            {
                try
                {
                    Icon_Boxes[i].Text = Fighter.Slot_Icon_IDs[i].ToString();
                    Name_Boxes[i].Text = Fighter.Slot_Name_IDs[i].ToString();
                }
                catch
                {
                    //MessageBox.Show(e.StackTrace + " | " + e.Message);
                }
            }
            SwitchingCharacters = false;
        }

        private Character Add_Character()
        {
            Array.Resize(ref Character_DB_Buffer, Character_DB_Buffer.Length + 0x7F);
            Array.ConstrainedCopy(Character_DB_Buffer, 0xD, Character_DB_Buffer, 0xD + 0x7F * Entries, 0x7F);
            Entries++;
            return new Character(Character_DB_Buffer.Skip(0xD + 0x7F * (Entries - 1)).Take(0x7F).ToArray());
        }

        private void Unlock_Final_Smash_Characters_New()
        {
            // Giga Bowser
            Unsorted_Characters[60] = Unsorted_Characters[4].Copy(Unsorted_Characters[60]);
            Unsorted_Characters[60].Cosmetic_ID = 0xC9;
            Unsorted_Characters[60].Series_Icon = 0x09;
            Unsorted_Characters[60].Has_Victory_Data = 0;
            Unsorted_Characters[60].ID = 58;
            Unsorted_Characters[60].CSS_Position = 61;
            Unsorted_Characters[60].Name = CharacterInfo.Character_Names[58];
            try
            {
                Unsorted_Characters[60].Character_Image = new BitmapImage(new Uri(Executable_Path + "\\Icons\\" + Unsorted_Characters[60].Name + ".png"));
            }
            catch { }

            // Giga Mac
            Unsorted_Characters[61] = Unsorted_Characters[4].Copy(Unsorted_Characters[61]);
            Unsorted_Characters[61].Cosmetic_ID = 0xCA;
            Unsorted_Characters[61].Series_Icon = 0x18;
            Unsorted_Characters[61].Has_Victory_Data = 0;
            Unsorted_Characters[61].ID = 60;
            Unsorted_Characters[61].CSS_Position = 62;
            Unsorted_Characters[61].Name = CharacterInfo.Character_Names[60];
            try
            {
                Unsorted_Characters[61].Character_Image = new BitmapImage(new Uri(Executable_Path + "\\Icons\\" + Unsorted_Characters[61].Name + ".png"));
            }
            catch { }

            if (MessageBox.Show("Do you want to add Wario Man and Mega Lucario as well? Adding these two characters overwrites other unused characters in the file, and will cause your CSS to have minor graphical glitches. It will still function normally.", "Add Characters Not In File", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // Wario Man (82)?
                Unsorted_Characters[63] = Unsorted_Characters[4].Copy(Unsorted_Characters[63]);
                Unsorted_Characters[63].Cosmetic_ID = 0x3C; // Not right
                Unsorted_Characters[63].Series_Icon = 0x0E;
                Unsorted_Characters[63].Has_Victory_Data = 0;
                Unsorted_Characters[63].ID = 59;
                Unsorted_Characters[63].CSS_Position = 63;
                Unsorted_Characters[63].Name = CharacterInfo.Character_Names[59];
                try
                {
                    Unsorted_Characters[63].Character_Image = new BitmapImage(new Uri(Executable_Path + "\\Icons\\" + Unsorted_Characters[63].Name + ".png"));
                }
                catch { }
                //Characters[82] = Unsorted_Characters[82];

                // Mega Lucario (83)?
                Unsorted_Characters[81] = Unsorted_Characters[4].Copy(Unsorted_Characters[81]);
                Unsorted_Characters[81].Cosmetic_ID = 0x3E; // Not right
                Unsorted_Characters[81].Series_Icon = 0x07;
                Unsorted_Characters[81].Has_Victory_Data = 0;
                Unsorted_Characters[81].ID = 61;
                Unsorted_Characters[81].CSS_Position = 64;
                Unsorted_Characters[81].Name = CharacterInfo.Character_Names[61];
                try
                {
                    Unsorted_Characters[81].Character_Image = new BitmapImage(new Uri(Executable_Path + "\\Icons\\" + Unsorted_Characters[81].Name + ".png"));
                }
                catch { }
                //Characters[83] = Unsorted_Characters[83];
            }

            // Resort Characters and Generate Panels
            SortCharacters();
            GenerateCharacterPanels();
        }

        private void Auto_Update_Costume_Count(string Smash_Explorer_Directory)
        {
            if (Directory.Exists(Smash_Explorer_Directory))
            {
                string Fighter_Directory = Smash_Explorer_Directory + "\\workspace\\content\\patch\\data\\fighter";
                if (Directory.Exists(Fighter_Directory))
                {
                    for (int i = 0; i < CharacterInfo.Character_File_Names.Length; i++)
                    {
                        Character Modifying_Character = null;
                        for (int x = 0; x < Unsorted_Characters.Length; x++)
                        {
                            if (Unsorted_Characters[x].Name == CharacterInfo.Character_Names[i])
                            {
                                Modifying_Character = Unsorted_Characters[x];
                                break;
                            }
                        }

                        int Costume_Count = 8;
                        if (i < 3 || i > 57)
                        {
                            Costume_Count = 1; // Non-standard fighters (Master Core has 6 slots)
                        }
                        else if (i == 41)
                        {
                            Costume_Count = 16; // Little Mac
                        }

                        string Character_Directory = Fighter_Directory + "\\" + CharacterInfo.Character_File_Names[i] + "\\model\\body\\";
                        if (Modifying_Character != null && Directory.Exists(Character_Directory))
                        {
                            string[] Costume_Folders = Directory.GetDirectories(Character_Directory);
                            foreach (string Costume_Folder in Costume_Folders)
                            {
                                string Folder_Name = new DirectoryInfo(Costume_Folder).Name;
                                if (Folder_Name.Substring(0, 1) == "c" && byte.TryParse(Folder_Name.Substring(1, 2), out byte CostumeId))
                                {
                                    CostumeId++; // Add one, since we start at c00
                                    if (CostumeId > Costume_Count)
                                    {
                                        Costume_Count = CostumeId;
                                    }
                                }
                            }
                            Modifying_Character.Character_Slots = Costume_Count;
                        }
                        else if (Modifying_Character != null)
                        {
                            Modifying_Character.Character_Slots = Costume_Count;
                        }
                    }
                    GenerateCharacterPanels();
                    MessageBox.Show("Successfully updated costume counts!");
                }
                else
                {
                    MessageBox.Show("Unable to locate fighters folder inside of Sm4shExplorer workspace! Make sure you pointed to the base Sm4shExplorer folder!");
                }
            }
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

        private void Auto_Sort(uint Original_Position, Character Original_Fighter = null)
        {
            Original_Fighter = Original_Fighter ?? Selected_Character;
            foreach (Character Fighter in Characters)
            {
                if ((Fighter != Original_Fighter && Fighter.ID != 0xFFFFFFFF) || Fighter.Name.Equals("All Miis") || Fighter.Name.Equals("Random"))
                {
                    if (Original_Fighter.CSS_Position < Original_Position && Fighter.CSS_Position >= Original_Fighter.CSS_Position && Fighter.CSS_Position < Original_Position)
                    {
                        Fighter.CSS_Position += 1;
                    }
                    else if (Original_Fighter.CSS_Position > Original_Position && Fighter.CSS_Position <= Original_Fighter.CSS_Position && Fighter.CSS_Position > Original_Position)
                    {
                        Fighter.CSS_Position -= 1;
                    }
                }
            }
        }

        private void CSSPosition_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!SwitchingCharacters && Selected_Character != null && int.TryParse(CSSPositionTextBox.Text, out int Slots))
            {
                uint Original_Position = Selected_Character.CSS_Position;
                if (Slots > 255)
                {
                    CSSPositionTextBox.Text = "255";
                    Selected_Character.CSS_Position = 255;
                }
                else
                {
                    Selected_Character.CSS_Position = (byte)Slots;
                }
                // Update other characters CSS Positions
<<<<<<< HEAD
                if (AllowAutoSort)
                {
                    Auto_Sort(Original_Position);
=======
                foreach (Character Fighter in Characters)
                {
                    if ((Fighter != Selected_Character && Fighter.ID != 0xFFFFFFFF) || Fighter.Name.Equals("Random"))
                    {
                        if (Selected_Character.CSS_Position < Original_Position && Fighter.CSS_Position >= Selected_Character.CSS_Position && Fighter.CSS_Position < Original_Position)
                        {
                            Fighter.CSS_Position += 1;
                        }
                        else if (Selected_Character.CSS_Position > Original_Position && Fighter.CSS_Position <= Selected_Character.CSS_Position && Fighter.CSS_Position > Original_Position)
                        {
                            Fighter.CSS_Position -= 1;
                        }
                    }
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
                }
                SortCharacters();
                GenerateCharacterPanels();
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
                        try
                        {
                            SeriesIconImage.Source = new BitmapImage(new Uri(Executable_Path + "\\Series Icons\\Unused.png"));
                        }
                        catch { MessageBox.Show("Couldn't locate a valid Series Icon image. Make sure you have the Series Icons folder in the same place as the program!"); };
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Save Entries Count
            BitConverter.GetBytes(Entries).Reverse().ToArray().CopyTo(Character_DB_Buffer, 0x9);
            for (int i = 0; i < Characters.Length; i++)
            {
                Unsorted_Characters[i].Write(ref Character_DB_Buffer, 0xD + 0x7F * i);
            }
            using (FileStream Stream = new FileStream(Executable_Path + "\\ui_character_db.bin", FileMode.OpenOrCreate))
            {
                using (BinaryWriter Writer = new BinaryWriter(Stream))
                {
                    Writer.Write(Character_DB_Buffer);
                }
            }
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            SaveAsDialog.FileName = "ui_character_db.bin";
            SaveAsDialog.RestoreDirectory = true;
            SaveAsDialog.InitialDirectory = Executable_Path;
            SaveAsDialog.Filter = "Binary File|*.bin|All Files|*.*";
            if (SaveAsDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Save Entries Count
                BitConverter.GetBytes(Entries).Reverse().ToArray().CopyTo(Character_DB_Buffer, 0x9);
                for (int i = 0; i < Characters.Length; i++)
                {
                    Unsorted_Characters[i].Write(ref Character_DB_Buffer, 0xD + 0x7F * i);
                }
                using (FileStream Stream = new FileStream(SaveAsDialog.FileName, FileMode.OpenOrCreate))
                {
                    using (BinaryWriter Writer = new BinaryWriter(Stream))
                    {
                        Writer.Write(Character_DB_Buffer);
                    }
                }
            }
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new CSS_Peview_Window(this, Characters).Show();
        }
<<<<<<< HEAD

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Unlock_Final_Smash_Characters_New();
        }

        private void MenuItem_Click1(object sender, RoutedEventArgs e)
        {
            Character Donating_Character = Unsorted_Characters[4]; // Mario is donating his info
            for (int i = 73; i < 76; i++)
            {
                Character Current_Character = Unsorted_Characters[i];
                if (Current_Character.Playable == 0)
                {
                    Current_Character.Entry_1 = Donating_Character.Entry_1;
                    Current_Character.Playable = Donating_Character.Playable;
                    Current_Character.Entry_3 = Donating_Character.Entry_3;
                    Current_Character.Has_Victory_Data = Donating_Character.Has_Victory_Data;
                    Current_Character.Entry_8 = Donating_Character.Entry_8;
                    Current_Character.Entry_11 = Donating_Character.Entry_11;
                    Current_Character.Entry_12 = Donating_Character.Entry_12;
                    Current_Character.Entry_14 = Donating_Character.Entry_14;
                    Current_Character.Entry_15 = Donating_Character.Entry_15;
                    Current_Character.Entry_16 = Donating_Character.Entry_16;
                    Current_Character.Entry_17 = Donating_Character.Entry_17;
                    Current_Character.Entry_18 = Donating_Character.Entry_18;
                    Current_Character.Entry_19 = Donating_Character.Entry_19;
                    Current_Character.Entry_20 = Donating_Character.Entry_20;
                    Current_Character.CSS_Position = (byte)i;
                }
            }
            SortCharacters();
            GenerateCharacterPanels();
        }

        private void MenuItem_Click2(object sender, RoutedEventArgs e)
        {
            if (SmashExplorer_Browser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Auto_Update_Costume_Count(SmashExplorer_Browser.SelectedPath);
            }
        }

        #region Same Position Check

        private void Check_Same_Positions(bool Fix = false)
        {
            for (int i = 0; i < Unsorted_Characters.Length; i++)
            {
                Character Current = Unsorted_Characters[i];
                for (int x = 0; x < Unsorted_Characters.Length; x++)
                {
                    if (Current != Unsorted_Characters[x] && Current.Playable == 1 && Unsorted_Characters[x].Playable == 1 && Current.CSS_Position == Unsorted_Characters[x].CSS_Position)
                    {
                        uint Original_Position = Current.CSS_Position;
                        if (!Fix)
                        {
                            if (MessageBox.Show("Multiple Characters have the same CSS Position! If they are not all unique, this will cause problems when dragging. Would you like them to be automatically fixed? This could change some character's positions.", "CSS Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                Fix = true;
                                if (i > x)
                                {
                                    Current.CSS_Position += 1;
                                    Auto_Sort(Original_Position, Current);
                                }
                                else
                                {
                                    Unsorted_Characters[x].CSS_Position += 1;
                                    Auto_Sort(Original_Position, Unsorted_Characters[x]);
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            if (i > x)
                            {
                                Current.CSS_Position += 1;
                                Auto_Sort(Original_Position, Current);
                            }
                            else
                            {
                                Unsorted_Characters[x].CSS_Position += 1;
                                Auto_Sort(Original_Position, Unsorted_Characters[x]);
                            }
                        }
                    }
                }
            }
            SortCharacters();
        }

        #endregion

        #region Drag & Drop

        private static int Click_Y = 0;
        private static bool Dragging = false;
        private static StackPanel Dragging_Control;
        private static Character Dragging_Character;
        
        private int Location_to_CSS_Position(int Y, int Control_Thickness)
        {
            int Midpoint = Control_Thickness / 2;
            return ((Y / Control_Thickness)).Clamp(0, Entries);
        }

        private void CharacterEntry_MouseDown(object sender, MouseEventArgs e, Character Fighter)
        {
            if (Can_Move(Fighter))
            {
                Dragging_Character = Fighter;
                Dragging_Control = sender as StackPanel;
                Dragging = true;
                Click_Y = (int)e.GetPosition(CharacterStackPanel).Y;
            }
        }

        private void CharacterEntry_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender as StackPanel == Dragging_Control || sender == CharacterStackPanel)
            {
                Dragging_Character = null;
                Dragging_Control = null;
                Dragging = false;
                Click_Y = 0;
            }
        }

        // CharacterStackPanel MouseMove
        private void CharacterStackPanel_MouseMoved(object sender, MouseEventArgs e)
        {
            if (Dragging && Dragging_Control != null && Dragging_Character == Selected_Character && Can_Move(Dragging_Character))
            {
                int Y = (int)e.GetPosition(CharacterStackPanel).Y;
                int CSS_Position = Location_to_CSS_Position(Y, 30);

                if (CSS_Position < CharacterStackPanel.Children.Count)
                {
                    string Tag = (string)((CharacterStackPanel.Children[CSS_Position] as StackPanel).Tag);
                    Character Hovered_Character = Characters.FirstOrDefault(o => o.Name.Equals(Tag));
                    if (Hovered_Character != null && Hovered_Character != Dragging_Character && !Hovered_Character.Name.Equals("All Miis") && !Hovered_Character.Name.Equals("Random"))
                    {
                        CSS_Position = Hovered_Character.CSS_Position;
                        if (CSS_Position != Dragging_Character.CSS_Position)
                        {
                            uint Original_Position = Dragging_Character.CSS_Position;
                            Dragging_Character.CSS_Position = (byte)CSS_Position;
                            Auto_Sort(Original_Position);
                            SortCharacters();

                            // Sort Panels
                            Dragging_Control = null;
                            GenerateCharacterPanels();
                            for (int i = 0; i < CharacterStackPanel.Children.Count; i++)
                            {
                                Tag = (string)(((StackPanel)CharacterStackPanel.Children[i]).Tag);
                                if (Tag.Equals(Dragging_Character.Name))
                                {
                                    Dragging_Control = CharacterStackPanel.Children[i] as StackPanel;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
=======
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
    }
}
