using System;
using System.Collections.Generic;
using System.Linq;
using Array_Csharp_1laba;
using System.Text;
using System.Threading.Tasks;

namespace ShannonFano 
{
    public class ShanFano
    {

        private List<Inf> InfList; // list of characters with a code and frequency of occurrence

        private Map<char, Inf> CodeTree;

        private Map<string, char> DecodeTree;

        private char[] UserString;

        private static int memorySize;

        private static int sourceMemory;

        private static float compressionRatio;

        public ShanFano()
        {
            InfList = new List<Inf>();

            CodeTree = new Map<char, Inf>();

            DecodeTree = new Map<string, char>();

        }

        public List<Inf> GetInfList()
        {
            return InfList;

        }

        public int GetMemorySize()
        {;

            foreach (var symbol in InfList)
            {
                memorySize += symbol.frequency * symbol.code.Length;
            }

            return memorySize;
        }

        public int GetSourceMemory(string symbols)
        {
            sourceMemory = symbols.Length * 8;

            return sourceMemory;

        }

        public float GetCompressionRatio()
        {
            compressionRatio = memorySize / (float)sourceMemory * 100;

            return compressionRatio;

        }

        private void StringEncoding(string symbols)
        {
            UserString = symbols.ToCharArray();

            if(UserString.Length == 0)
            {
                throw new Exception("Empty message");
            }

            GetSymbolsTree(UserString);

            GetInfList(CodeTree.GetKeys());

            Quicksort(InfList, 0, InfList.Count - 1);

            CreateCode(InfList, GetSumFrequency(InfList), "");

            for (int i = 0; i < InfList.Count; i++)
            {
                DecodeTree.Insert(InfList[i].code, InfList[i].symbol);

            }

            foreach (var cur in InfList)
            {
                CodeTree.Find(cur.symbol).Data.code = cur.code;
            }
            
        }


        public string GetDecodingString(string code)
        {
            var prefix = "";

            var text = "";

            for (var i = 0; i < code.Length; i++)
            {
                prefix += code[i];

                try
                {
                    var symbol = DecodeTree.Find(prefix).Data;

                    text += symbol;

                    prefix = "";
                }
                catch
                {

                }
            }

            return text;
        }


        public string GetEncodedString(string symbols)
        {
            StringEncoding(symbols);

            string EncodedString = "";

            for (int i = 0; i < UserString.Length; i++)
            {

                EncodedString += CodeTree.Find(UserString[i]).Data.code;
            }

            return EncodedString;

        }

        private void GetInfList(List<char> SymbolList)
        {
            Node<char, Inf> node;

            Inf inf;

            foreach (var cur in SymbolList)
            {
                node = new Node<char, Inf>();

                inf = new Inf();

                node = CodeTree.Find(cur);

                inf.symbol = node.Key;

                inf.frequency = node.Data.frequency;

                InfList.Add(inf);
            }


        }

        private void GetSymbolsTree(char[] UserString)
        {
            Inf curInf; 

            for (int i = 0; i < UserString.Length; i++)
            {
                curInf = new Inf();

                curInf.frequency = 1;

                try
                {
                    curInf.symbol = UserString[i];

                    CodeTree.Insert(UserString[i], curInf);

                }
                catch
                {
                    CodeTree.Find(UserString[i]).Data.frequency++;
                }

            }

        }

        private int GetSumFrequency(List<Inf> InfList)
        {
            int sum = 0;

            foreach (var cur in InfList)
            {

                sum += cur.frequency;
            }

            return sum;
        }

        private void CreateCode(List<Inf> symbols, int sum, string code)
        {
            if (symbols.Count == 1)
            {
                symbols[0].code = code;

                return;
            }

            var leftList = new List<Inf>();

            var rightList = new List<Inf>();

            var curSum = 0;

            for (int i = 0; i < symbols.Count; i++)
            {
                if (curSum + symbols[i].frequency <= sum / 2)
                {
                    leftList.Add(symbols[i]);

                    curSum += symbols[i].frequency;
                }
                else
                {
                    rightList.Add(symbols[i]);
                }
            }

            CreateCode(leftList, curSum, code + "0");

            CreateCode(rightList, sum - curSum, code + "1");
        }

        private int Partition(List<Inf> array, int StartPosition, int EndPosition)
        {
            int i = StartPosition;

            for (int j = StartPosition; j <= EndPosition; j++)
            {
                if (array[j].frequency >= array[EndPosition].frequency)
                {
                    Inf t = array[i];

                    array[i] = array[j];

                    array[j] = t;

                    i++;
                }
            }

            return i - 1;
        }

        private void Quicksort(List<Inf> array, int StartPosition, int EndPosition)
        {
            if (StartPosition >= EndPosition)
                return;

            int c = Partition(array, StartPosition, EndPosition);

            Quicksort(array, StartPosition, c - 1);

            Quicksort(array, c + 1, EndPosition);
        }

    }

    public class Inf
    {
        public char symbol;

        public int frequency;

        public string code;

    }
}
