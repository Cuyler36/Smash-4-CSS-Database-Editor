using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smash_Character_Database_Editor
{
    public static class DataManager
    {
        public static dynamic Read(byte[] Buffer, int Offset)
        {
            switch (Buffer[Offset])
            {
                case 0x1:
                case 0x2:
                    return Buffer[Offset + 1]; // 0x2 is actually sbyte
                case 0x3:
                    return BitConverter.ToUInt16(Buffer.Skip(Offset + 1).Take(2).Reverse().ToArray(), 0);
                case 0x4:
                    return BitConverter.ToInt16(Buffer.Skip(Offset + 1).Take(2).Reverse().ToArray(), 0);
                case 0x5:
                    return BitConverter.ToUInt32(Buffer.Skip(Offset + 1).Take(4).Reverse().ToArray(), 0);
                case 0x6:
                    return BitConverter.ToInt32(Buffer.Skip(Offset + 1).Take(4).Reverse().ToArray(), 0);
                case 0x7:
                    return BitConverter.ToSingle(Buffer.Skip(Offset + 1).Take(4).Reverse().ToArray(), 0);
                case 0x8:
                    return Encoding.ASCII.GetString(Buffer.Skip(Offset + 5).Take(BitConverter.ToInt32(Buffer.Skip(Offset + 1).Take(4).Reverse().ToArray(), 0)).ToArray());
                default:
                    throw new Exception(string.Format("Invalid data type: 0x{0} at offset: 0x{1}", Buffer[Offset].ToString("X"), Offset));
            }
        }

        public static void Write(ref byte[] Buffer, int Offset, dynamic Data)
        {
            Type Data_Type = Data.GetType();
            if (Data_Type == typeof(byte))
            {
                Buffer[Offset + 1] = Data;
            }
            else if (Data_Type == typeof(byte[]))
            {
                Data.CopyTo(Buffer, Offset + 1);
            }
            else
            {
                ((byte[])BitConverter.GetBytes(Data)).Reverse().ToArray().CopyTo(Buffer, Offset + 1);
            }
        }
    }
}
