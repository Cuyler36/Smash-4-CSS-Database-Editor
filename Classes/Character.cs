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

        public static Dictionary<uint, string> Cosmetic_Names = new Dictionary<uint, string>
        {
            {0x01, "Mii Brawler"},
            {0x02, "Mii Swordsman"},
            {0x03, "Mii Gunner"},
            {0x04, "Mario"},
            {0x05, "Donkey Kong"},
            {0x06, "Link"},
            {0x07, "Samus"},
            {0x08, "Yoshi"},
            {0x09, "Kirby"},
            {0x0A, "Fox"},
            {0x0B, "Pikachu"},
            {0x0C, "Luigi"},
            {0x0D, "Falcon"},
            {0x0E, "Ness"},
            {0x0F, "Peach"},
            {0x10, "Bowser"},
            {0x11, "Zelda"},
            {0x12, "Sheik"},
            {0x13, "Marth"},
            {0x14, "Game & Watch"},
            {0x15, "Ganon"},
            {0x16, "Falco"},
            {0x17, "Wario"},
            {0x18, "Meta Knight"},
            {0x19, "Pit"},
            {0x1A, "Zero Suit Samus"},
            {0x1B, "Olimar/Alph"},
            {0x1C, "Diddy Kong"},
            {0x1D, "King Dedede"},
            {0x1E, "Ike"},
            {0x1F, "Lucario"},
            {0x20, "R.O.B."},
            {0x21, "Toon Link"},
            {0x22, "Charizard"},
            {0x23, "Sonic"},
            {0x24, "Dr. Mario"},
            {0x25, "Rosalina"},
            {0x26, "Wii Fit"},
            {0x27, "Little Mac"},
            {0x28, "Villager"},
            {0x29, "Palutena"},
            {0x2A, "Robin"},
            {0x2B, "Duck Hunt"},
            {0x2C, "Bowser Jr."},
            {0x2D, "Shulk"},
            {0x2E, "Jigglypuff"},
            {0x2F, "Lucina"},
            {0x30, "Dark Pit"},
            {0x31, "Greninja"},
            {0x32, "Pac-Man"},
            {0x33, "Megaman"},
            {0x34, "Mewtwo"},
            {0x35, "Ryu"},
            {0x36, "Lucas"},
            {0x37, "Roy"},
            {0x38, "Cloud"},
            {0x39, "Bayonetta"},
            {0x3A, "Corrin"},
            {0x3B, "Giga Bowser"},
            {0x3C, "Wario Man"},
            {0x3D, "Giga Mac"},
            {0x3E, "Mega Lucario"},
            {0x48, "Char_71"},
            {0x4D, "Char_76"},
            {0x4E, "Char_77"},
            {0xCB, "Question"},
            {0xD2, "Master Hand"},
            {0xD3, "Master Core"},
            {0xD5, "Crazy Hand"},
            {0xD6, "Bomb"},
            {0xD7, "Sandbag"},
            {0xD8, "Ridley"},
            {0xD9, "Metal Face"},
            {0xDA, "Yellow Devil"},
            {0xDB, "Mii Enemy (Brawler)"},
            {0xDC, "Mii Enemy (Swordsman)"},
            {0xDD, "Mii Enemy (Gunner)"},
            {0xDE, "Mii Enemy (All)"},
            {0xDF, "Virus"},
            {0xE0, "Narration"},
            {0xE1, "Assist Trophy"},
            {0xE2, "Pokemon"},
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
            for (int i = 0; i < 0x10; i++)
            {
                DataManager.Write(ref Buffer, Offset + 0x3F + i * 2, Slot_Icon_IDs[i]);
                DataManager.Write(ref Buffer, Offset + 0x5F + i * 2, Slot_Name_IDs[i]);
            }
        }
    }
}