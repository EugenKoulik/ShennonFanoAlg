using System;
using Array_Csharp_1laba;
using System.Collections.Generic;

namespace ShannonFano
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введите строку \n");

            ShanFano shanFano = new ShanFano();

            List<Inf> inf = shanFano.GetInfList(); 

            string message = Console.ReadLine();

            string codeMessage = shanFano.GetEncodedString(message);

            string decodeMessage = shanFano.GetDecodingString(codeMessage);

            Console.WriteLine($"Закодированное сообщение - {codeMessage}");

            foreach(var cur in inf)
            {
                Console.WriteLine($"{cur.symbol} - {cur.frequency} - {cur.code}");

            }

            Console.WriteLine($"Декодированное сообщение - {decodeMessage}");

            Console.WriteLine($"Память исходной - {shanFano.GetSourceMemory(message)} бит, " +
                              $"память сжатой - {shanFano.GetMemorySize()} бит, " +
                              $"коэффициент сжатия - {shanFano.GetCompressionRatio()}");

        }
    }

}
