using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Year2021
{
    internal class Day16App
    {
        private string Input = AppExtensions.GetInputString($"./Year2021/Day16/Input.txt");

        private Dictionary<char, string> Keys = new Dictionary<char, string>();

        private int VersionNumberTotal = 0;

        public void Execute()
        {
            this.Keys.Add('0', "0000");
            this.Keys.Add('1', "0001");
            this.Keys.Add('2', "0010");
            this.Keys.Add('3', "0011");
            this.Keys.Add('4', "0100");
            this.Keys.Add('5', "0101");
            this.Keys.Add('6', "0110");
            this.Keys.Add('7', "0111");
            this.Keys.Add('8', "1000");
            this.Keys.Add('9', "1001");
            this.Keys.Add('A', "1010");
            this.Keys.Add('B', "1011");
            this.Keys.Add('C', "1100");
            this.Keys.Add('D', "1101");
            this.Keys.Add('E', "1110");
            this.Keys.Add('F', "1111");

            var converted = new StringBuilder();

            foreach (var c in this.Input)
            {
                converted.Append(this.Keys[c]);
            }

            var info = converted.ToString();

            var result = CheckPacket(info);
        }

        private Day16Packet CheckPacket(string text)
        {
            var packetVersion = this.Keys.Where(m => m.Value == "0" + text.Substring(0, 3)).First().Key;
            var packetTypeID = this.Keys.Where(m => m.Value == "0" + text.Substring(3, 3)).First().Key;

            var packetInfo = new Day16Packet()
            {
                Version = Convert.ToInt32(packetVersion),
                TypeID = Convert.ToInt32(packetTypeID),
                FullText = text
            };

            if (packetInfo.TypeID == 4)
            {
                var literal = text.Substring(6);
                var result = ConvertToLiteral(literal);

            }
            else
            {
                packetInfo.LengthTypeID = packetInfo.FullText[6] == '0' ? 15 : 11;
                packetInfo.LengthBits = Convert.ToInt32(packetInfo.FullText.Substring(7, packetInfo.LengthTypeID));

                if (packetInfo.LengthTypeID == 15)
                {
                    // The lengthstringBits is how many bits we need to take
                    packetInfo.SubpacketText = packetInfo.FullText.Substring(7, packetInfo.LengthBits);
                }
                else
                {
                    // The lengthstring bits is the count of groups of 11bits
                    var bits = packetInfo.LengthBits * 11;
                    packetInfo.SubpacketText = packetInfo.FullText.Substring(7, bits);
                    for (var i = 0; i < packetInfo.LengthBits; i++)
                    {
                        //CheckPacket();
                        //packetInfo.Subpackets.Add(packetInfo.SubpacketText.Substring(i * 11, 11));
                    }
                }
            }

            return packetInfo;
        }

        private int ConvertToLiteral(string text)
        {
            var charsToGrab = Math.Floor((decimal)text.Length / 5);

            var runningText = new StringBuilder();
            for (var i = 0; i < charsToGrab; i++)
            {
                runningText.Append(text.Substring((i * 5) + 1, 4));
            }
            var result = AppExtensions.StringToBinaryInt(runningText.ToString());

            return result;
        }

        // D2FE28
        // 1101 0010 1111 1110 0010 1000
        // 110 100 101 111 111 000 101 000



    }

    internal class Day16Packet
    {
        public int Version { get; set; }
        public int TypeID { get; set; }
        public int LengthTypeID { get; set; } = 0;
        public int LengthBits { get; set; } = 0;
        public string FullText { get; set; } = "";
        public string SubpacketText { get; set; } = "";

    }
}
