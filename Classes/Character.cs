using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;

namespace Smash_Character_Database_Editor
{
    public static class CharacterInfo
    {
        public static string[] Character_Names = new string[65]
        {
            "Mii Brawler",
            "Mii Swordsman",
            "Mii Gunner",
            "Mario",
            "Donkey Kong",
            "Link",
            "Samus",
            "Yoshi",
            "Kirby",
            "Fox",
            "Pikachu",
            "Luigi",
            "Captain Falcon",
            "Ness",
            "Peach",
            "Bowser",
            "Zelda",
            "Sheik",
            "Marth",
            "Game & Watch",
            "Ganon",
            "Falco",
            "Wario",
            "Meta Knight",
            "Pit",
            "Zero Suit Samus",
            "Olimar",
            "Diddy Kong",
            "King Dedede",
            "Ike",
            "Lucario",
            "R.O.B.",
            "Toon Link",
            "Charizard",
            "Sonic",
            "Jigglypuff",
            "Dr. Mario",
            "Lucina",
            "Dark Pit",
            "Rosalina",
            "Wii Fit",
            "Little Mac",
            "Villager",
            "Palutena",
            "Robin",
            "Duck Hunt",
            "Bowser Jr.",
            "Shulk",
            "Greninja",
            "Pac-Man",
            "Megaman",
            "Mewtwo",
            "Ryu",
            "Lucas",
            "Roy",
            "Cloud",
            "Bayonetta",
            "Corrin",
            "Giga Bowser",
            "Wario Man",
            "Giga Mac",
            "Mega Lucario",
            "Mii Enemy (Brawler)",
            "Mii Enemy (Swordsman)",
            "Mii Enemy (Gunner)"
        };
    }

    class Character
    {
        public uint Cosmetic_ID;
        public uint Series_Icon;
        public uint ID;
        public int Character_Slots;
        public bool Show_on_CSS;
        public bool Is_DLC;
        public byte CSS_Position;
        public byte[] Slot_Icon_IDs = new byte[0x10];
        public byte[] Slot_Name_IDs = new byte[0x10];

        public string Name;
        public BitmapImage Character_Image;

        public Character(byte[] Data, int Index = 0)
        {
            Cosmetic_ID = DataManager.Read(Data, 0x0);
            Series_Icon = DataManager.Read(Data, 0xB);
            ID = DataManager.Read(Data, 0x15);
            Character_Slots = DataManager.Read(Data, 0x1A);
            Show_on_CSS = DataManager.Read(Data, 0x24) == 0;
            Is_DLC = DataManager.Read(Data, 0x26) == 1;
            CSS_Position = DataManager.Read(Data, 0x2C);
            for (int i = 0; i < 0x10; i++)
            {
                Slot_Icon_IDs[i] = DataManager.Read(Data, 0x3F + i * 2);
            }
            for (int i = 0; i < 0x10; i++)
            {
                Slot_Name_IDs[i] = DataManager.Read(Data, 0x5F + i * 2);
            }

            Name = ID >= CharacterInfo.Character_Names.Length ? "char " + ID.ToString() : CharacterInfo.Character_Names[ID];
            string Image_Path = MainWindow.Executable_Path + "\\Icons\\" + Name + ".png";
            if (File.Exists(Image_Path))
            {
                Character_Image = new BitmapImage(new Uri(Image_Path));
            }
            else
            {
                Character_Image = new BitmapImage(new Uri(MainWindow.Executable_Path + "\\Icons\\Random.png"));
            }
        }

        public void Write(ref byte[] Buffer, int Offset)
        {
            DataManager.Write(ref Buffer, Offset, Cosmetic_ID);
            DataManager.Write(ref Buffer, Offset + 0xB, Series_Icon);
            DataManager.Write(ref Buffer, Offset + 0x15, ID);
            DataManager.Write(ref Buffer, Offset + 0x1A, Character_Slots);
            DataManager.Write(ref Buffer, Offset + 0x24, Show_on_CSS ? (byte)0 : (byte)1);
            DataManager.Write(ref Buffer, Offset + 0x26, Is_DLC ? (byte)1 : (byte)0);
            DataManager.Write(ref Buffer, Offset + 0x2C, CSS_Position);
        }
    }
}