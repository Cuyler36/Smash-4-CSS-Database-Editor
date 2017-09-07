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
        public static string[] Character_Names = new string[67]
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
            "All Miis",
            "Giga Bowser",
            "Wario Man",
            "Giga Mac",
            "Mega Lucario",
<<<<<<< HEAD
            "Mii Brawler (Enemy)",
            "Mii Swordsman (Enemy)",
            "Mii Gunner (Enemy)"
=======
            "Mii Enemy (Brawler)",
            "Mii Enemy (Swordsman)",
            "Mii Enemy (Gunner)",
            "Random"
>>>>>>> 63ca892648e9fa391d728a909d29e3ec9cf1b8e1
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
            {0xC8, "All Miis"},
            {0xC9, "Giga Bowser"},
            {0xCA, "Giga Mac"},
            {0xCB, "Question"},
            {0xD2, "Master Hand"},
            {0xD3, "Master Core"},
            {0xD4, "Crazy Hand"},
            {0xD5, "Master Hand & Crazy Hand"},
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

        public static Dictionary<uint, string> Series_Names = new Dictionary<uint, string>
        {
            {0x00, "Smash"},
            {0x01, "Donkey Kong"},
            {0x02, "Starfox"},
            {0x03, "Kirby"},
            {0x04, "F-Zero"},
            {0x05, "Metroid"},
            {0x06, "Earthbound"},
            {0x07, "Pokemon"},
            {0x08, "Legend of Zelda"},
            {0x09, "Mario"},
            {0x0A, "Yoshi"},
            {0x0B, "Fire Emblem"},
            {0x0C, "Game & Watch"},
            {0x0D, "Pikmin"},
            {0x0E, "Wario"},
            {0x0F, "Kid Icarus"},
            {0x10, "Smash" },
            {0x11, "Gyromite"},
            {0x12, "Sonic"},
            {0x13, "Electroplankton"}, //No file
            {0x14, "Pictochat"},
            {0x15, "Animal Crossing"},
            {0x16, "Wii Fit"},
            {0x17, "Xenoblade"},
            {0x18, "Punch-Out"},
            {0x19, "Duck Hunt"},
            {0x1A, "Rythm Heaven"}, //No file
            {0x1B, "Megaman"},
            {0x1C, "Nintendogs"},
            {0x1D, "Mii Plaza"},
            {0x1E, "Pac-Man"},
            {0x1F, "Tomodachi"},
            {0x20, "Wrecking Crew"},
            {0x21, "Wuhu Island"},
            {0x22, "Miiverse"},
            {0x23, "Pilotwings"},
            {0x24, "Brain Age"}, //Blank File
            {0x25, "Ballon Fight"},
            {0x26, "Swapnote"}, //No file
            {0x27, "Street Fighter"},
            {0x28, "Final Fantasy"},
            {0x29, "Bayonetta"},
            //0x30 - 0x62?
            {0x63, "Etc"},
            //0x64 - 0x18F?
            {0x190, "All"},
            {0x191, "Equipment"},
            {0x192, "Special"},
            {0x193, "Mii Hats"},
            {0x194, "Mii Costume"},
            {0x195, "Figure"},
            {0x196, "Money"},
            {0x197, "Stage"},
            {0x198, "S-Ticket"},
            {0x199, "CD"},
            {0x19A, "Controller"},
            {0x19B, "Masterball"},
            {0x1F4, "fsi_moveup"},
            {0x1F5, "fsi_jumpup"},
            {0x1F6, "fsi_atkup"},
            {0x1F7, "fsi_specialup"},
            {0x1F8, "Smash"},
            {0x1F9, "fsi_itemup"},
            {0x1FA, "fsi_defup"},
            {0x1FB, "fsi_allup"},
            {0x1FC, "fg_fighter"},
            {0x1FD, "fg_companion"},
            {0x1FE, "fg_finalarts"},
            {0x1FF, "fg_item"},
            {0x200, "fg_assistfigure"},
            {0x201, "fg_pokeball"},
            {0x202, "fg_ws"},
            {0x203, "fg_fs"},
            {0x204, "fg_enemy"},
            {0x205, "fg_stage"},
            {0x207, "fg_series"},
            {0x208, "fg_etc"},
            {0x2BD, "fs_item"},
            {0x2BE, "m_retnormal"},
            {0x2BF, "m_rethold"},
            {0x2C0, "m_x0001"},
            {0x2C1, "m_reset"},
            {0x2C2, "m_stage"},
            {0x2C3, "m_fighter"},
            {0x2C4, "m_friend"},
            {0x2C5, "m_download"},
            {0x2C6, "m_loading"},
            {0x2C7, "m_nowloading"},
            {0x2C8, "m_team"},
            {0x2C9, "m_exitmulti"},
            {0x2CA, "m_rulenormal"},
            {0x2CB, "m_ruletimelimit"},
            {0x2CC, "m_customize"},
            {0x2CD, "m_rulehandicap"},
            {0x2CE, "m_ruleblowratio"},
            {0x2CF, "m_rulestaeselect"},
            {0x2D0, "m_rulestock"},
            {0x2D1, "m_ruleadditional"},
            {0x2D2, "m_ra_stocklimit"},
            {0x2D3, "m_ra_teamattack"},
            {0x2D4, "m_ra_pausefunction"},
            {0x2D5, "m_ra_scoredisplay"},
            {0x2D6, "m_ra_damagedisplay"},
            {0x2D7, "m_ra_randomstage"},
            {0x2D8, "m_miifighter"},
            {0x2D9, "m_miishooter"},
            {0x2DA, "m_miisordsman"},
            {0x2DB, "m_figlist_rotate"},
            {0x2DC, "m_figlist_move"},
            {0x2DD, "m_tr_lock"},
            {0x2DE, "m_tr_camera"},
            {0x2DF, "m_tr_fixedcamera"},
            {0x2E0, "m_tr_zoomin"},
            {0x2E1, "m_time"},
            {0x2E2, "m_ra_selfmiss"},
            {0x2E3, "m_popup"},
            {0x2E4, "m_title"},
            {0x2E5, "m_main"},
            {0x2E6, "m_clear"},
            {0x2E7, "m_ar"},
            {0x2E8, "m_info"},
            {0x2E9, "m_wiiu"},
            {0x2EA, "m_pass"},
            {0x2EB, "m_arcade"},
            {0x2EC, "m_pad1"},
            {0x2ED, "m_tds"},
            {0x2EE, "m_melee"},
            {0x2EF, "m_rule"},
            {0x2F0, "m_rule_item"},
            {0x2F1, "m_rule_add"},
            {0x2F2, "m_rule_stage"},
            {0x2F3, "m_chara"},
            {0x2F4, "m_result"},
            {0x2F5, "m_chara_8"},
            {0x2F6, "m_result_8"},
            {0x2F7, "m_melee_multi"},
            {0x2F8, "m_melee_newg"},
            {0x2F9, "m_wifi_friend"},
            {0x2FA, "m_friend_list"},
            {0x2FB, "m_wifi_other"},
            {0x2FC, "m_wifi_enjoy"},
            {0x2FD, "m_wifi_serious"},
            {0x2FE, "m_wifi_record"},
            {0x2FF, "m_wifi_watch"},
            {0x300, "m_watch_list"},
            {0x301, "m_wifi_share"},
            {0x302, "m_share_replay"},
            {0x303, "m_share_list"},
            {0x304, "m_share_photo"},
            {0x305, "m_share_mii"},
            {0x306, "m_share_stage"},
            {0x307, "m_wifi_event"},
            {0x308, "m_event_compe"},
            {0x309, "m_event_conq"},
            {0x30A, "m_wifi_tour"},
            {0x30B, "m_field"},
            {0x30C, "m_result_field1"},
            {0x30D, "m_other"},
            {0x30E, "m_other_one"},
            {0x30F, "m_diffi"},
            {0x310, "m_order"},
            {0x311, "m_order_stage_m"},
            {0x312, "m_order_stock"},
            {0x313, "m_order_stage_c"},
            {0x314, "m_order_how"},
            {0x315, "m_world"},
            {0x316, "m_event"},
            {0x317, "m_other_all"},
            {0x318, "m_stadium"},
            {0x319, "m_stadium_spar"},
            {0x31A, "m_other_chrmake"},
            {0x31B, "m_chrmake_cs"},
            {0x31C, "m_chrmake_mii"},
            {0x31D, "m_other_stgmake"},
            {0x31E, "m_other_col"},
            {0x31F, "m_col_fig"},
            {0x320, "m_flg_list"},
            {0x321, "m_fig_disp"},
            {0x322, "m_fig_coin"},
            {0x323, "m_fig_shop"},
            {0x324, "m_fig_box"},
            {0x325, "m_col_album"},
            {0x326, "m_col_replay"},
            {0x327, "m_col_sound"},
            {0x328, "m_col_record"},
            {0x329, "m_col_record_vs"},
            {0x32A, "m_record_count"},
            {0x32B, "m_record_info"},
            {0x32C, "m_record_bonus"},
            {0x32D, "m_other_option"},
            {0x32E, "m_option_vibrate"},
            {0x32F, "m_option_button"},
            {0x330, "m_option_sound"},
            {0x331, "m_option_border"},
            {0x332, "m_option_pos"},
            {0x333, "m_option_wifi"},
            {0x334, "m_option_profile"},
            {0x335, "m_option_camera"},
            {0x336, "m_option_mymusic"},
            {0x337, "m_from_official"},
            {0x338, "m_from_friend"},
            {0x339, "m_from_delivery"},
        };

        public static string[] Character_File_Names = new string[65]
        {
            "miifighter",
            "miiswordsman",
            "miigunner",
            "mario",
            "donkey",
            "link",
            "samus",
            "yoshi",
            "kirby",
            "fox",
            "pikachu",
            "luigi",
            "captain",
            "ness",
            "peach",
            "koopa",
            "zelda",
            "sheik",
            "marth",
            "gamewatch",
            "ganon",
            "falco",
            "wario",
            "metaknight",
            "pit",
            "szerosuit",
            "pikmin",
            "diddy",
            "dedede",
            "ike",
            "lucario",
            "robot",
            "toonlink",
            "lizardon",
            "sonic",
            "purin",
            "mariod",
            "lucina",
            "pitb",
            "rosetta",
            "wiifit",
            "littlemac",
            "murabito",
            "palutena",
            "reflet",
            "duckhunt",
            "koopajr",
            "shulk",
            "gekkouga",
            "pacman",
            "rockman",
            "mewtwo",
            "ryu",
            "lucas",
            "roy",
            "cloud",
            "bayonetta",
            "kamui",
            "koopag",
            "warioman",
            "littlemacg",
            "lucariom",
            "miienemyf",
            "miienemys",
            "miienemyg"
        };
    }

    public class Character
    {
        public uint Cosmetic_ID; // Offset: 0x00 (Entry 0)
        public byte Entry_1; // Offset: 0x05 (Always 1 in valid Characters)
        public byte Playable; // Offset: 0x07 (Entry 2)
        public byte Entry_3; // Offset: 0x09 (Always 1 in valid Characters)
        public uint Series_Icon; // Offset: 0x0B (Entry 4)
        public int Has_Victory_Data; // Offset: 0x10 (Entry 5)
        public uint ID; // Offset: 0x15 (Entry 6)
        public int Character_Slots; // Offset: 0x1A (Entry 7)
        public int Entry_8; // Offset: 0x1F (Unknown, look into)
        public bool Show_on_CSS; // Offset: 0x24 (Entry 9)
        public bool Is_DLC; // Offset: 0x26 (Entry 10)
        public byte Entry_11; // Offset: 0x28
        public byte Entry_12; // Offset: 0x2A
        public byte CSS_Position; // Offset: 0x2C (Entry 13)
        public byte Entry_14; // Offset: 0x2E
        public byte Entry_15; // Offset: 0x30
        public uint Entry_16; // Offset: 0x32 (Always seems to be 0xFFFFFFFF)
        public byte Entry_17; // Offset: 0x37
        public byte Entry_18; // Offset: 0x39
        public byte Entry_19; // Offset: 0x3B
        public byte Entry_20; // Offset: 0x3D
        public byte[] Slot_Icon_IDs = new byte[0x10]; // Offset: 0x3F - 0x5E (Entries 21 - 36)
        public byte[] Slot_Name_IDs = new byte[0x10]; // Offset: 0x5F - 0x7E (Entries 37 - 52)

        public string Name;
        public BitmapImage Character_Image;

        public Character() { }

        public Character(byte[] Data, int Index = 0)
        {
            Cosmetic_ID = DataManager.Read(Data, 0x0);
            //
            Entry_1 = DataManager.Read(Data, 0x5);
            //
            Playable = DataManager.Read(Data, 0x7);
            //
            Entry_3 = DataManager.Read(Data, 0x9);
            //
            Series_Icon = DataManager.Read(Data, 0xB);
            //
            Has_Victory_Data = DataManager.Read(Data, 0x10);
            //
            ID = DataManager.Read(Data, 0x15);
            Character_Slots = DataManager.Read(Data, 0x1A);
            //
            Entry_8 = DataManager.Read(Data, 0x1F);
            //
            Show_on_CSS = DataManager.Read(Data, 0x24) == 0;
            Is_DLC = DataManager.Read(Data, 0x26) == 1;
            //
            Entry_11 = DataManager.Read(Data, 0x28);
            Entry_12 = DataManager.Read(Data, 0x2A);
            //
            CSS_Position = DataManager.Read(Data, 0x2C);
            //
            Entry_14 = DataManager.Read(Data, 0x2E);
            Entry_15 = DataManager.Read(Data, 0x30);
            Entry_16 = DataManager.Read(Data, 0x32);
            Entry_17 = DataManager.Read(Data, 0x37);
            Entry_18 = DataManager.Read(Data, 0x39);
            Entry_19 = DataManager.Read(Data, 0x3B);
            Entry_20 = DataManager.Read(Data, 0x3D);
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
            // Important Data
            DataManager.Write(ref Buffer, Offset, Cosmetic_ID);
            DataManager.Write(ref Buffer, Offset + 0x7, Playable);
            DataManager.Write(ref Buffer, Offset + 0xB, Series_Icon);
            DataManager.Write(ref Buffer, Offset + 0x10, Has_Victory_Data);
            DataManager.Write(ref Buffer, Offset + 0x15, ID);
            DataManager.Write(ref Buffer, Offset + 0x1A, Character_Slots);
            DataManager.Write(ref Buffer, Offset + 0x24, Show_on_CSS ? (byte)0 : (byte)1);
            DataManager.Write(ref Buffer, Offset + 0x26, Is_DLC ? (byte)1 : (byte)0);
            DataManager.Write(ref Buffer, Offset + 0x2C, CSS_Position);

            // Unknown Data
            DataManager.Write(ref Buffer, Offset + 0x5, Entry_1);
            DataManager.Write(ref Buffer, Offset + 0x9, Entry_3);
            DataManager.Write(ref Buffer, Offset + 0x1F, Entry_8);
            DataManager.Write(ref Buffer, Offset + 0x28, Entry_11);
            DataManager.Write(ref Buffer, Offset + 0x2A, Entry_12);
            DataManager.Write(ref Buffer, Offset + 0x2E, Entry_14);
            DataManager.Write(ref Buffer, Offset + 0x30, Entry_15);
            DataManager.Write(ref Buffer, Offset + 0x32, Entry_16);
            DataManager.Write(ref Buffer, Offset + 0x37, Entry_17);
            DataManager.Write(ref Buffer, Offset + 0x39, Entry_18);
            DataManager.Write(ref Buffer, Offset + 0x3B, Entry_19);
            DataManager.Write(ref Buffer, Offset + 0x3D, Entry_20);

            // Per-Costume Slot Data
            for (int i = 0; i < 0x10; i++)
            {
                DataManager.Write(ref Buffer, Offset + 0x3F + i * 2, Slot_Icon_IDs[i]);
                DataManager.Write(ref Buffer, Offset + 0x5F + i * 2, Slot_Name_IDs[i]);
            }
        }

        public Character Copy(Character Copy_To = null)
        {
            Character Clone = Copy_To ?? new Character();

            Clone.Cosmetic_ID = Cosmetic_ID;
            Clone.Playable = Playable;
            Clone.Series_Icon = Series_Icon;
            Clone.Has_Victory_Data = Has_Victory_Data;
            Clone.ID = ID;
            Clone.Character_Slots = Character_Slots;
            Clone.Show_on_CSS = Show_on_CSS;
            Clone.Is_DLC = Is_DLC;
            Clone.CSS_Position = CSS_Position;
            Clone.Name = Name;
            Clone.Character_Image = Character_Image;

            Clone.Entry_1 = Entry_1;
            Clone.Entry_3 = Entry_3;
            Clone.Entry_8 = Entry_8;
            Clone.Entry_11 = Entry_11;
            Clone.Entry_12 = Entry_12;
            Clone.Entry_14 = Entry_14;
            Clone.Entry_15 = Entry_15;
            Clone.Entry_16 = Entry_16;
            Clone.Entry_17 = Entry_17;
            Clone.Entry_18 = Entry_18;
            Clone.Entry_19 = Entry_19;
            Clone.Entry_20 = Entry_20;

            Clone.Slot_Icon_IDs = Slot_Icon_IDs;
            Clone.Slot_Name_IDs = Slot_Name_IDs;

            return Clone;
        }
    }
}