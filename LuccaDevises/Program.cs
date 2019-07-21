using LuccaDevises.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Start 19:43
// Break 3:30
namespace LuccaDevises
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 1 || !File.Exists(args[0]))
                throw new Exception("argument incorrect");

            var parser = new FileParser(args[0]);
            var file = parser.ParseFile();

            var converter = new ConverterTool(file);
            int conversion = converter.ProcessChangeRate();
            Console.WriteLine(conversion);
            Console.ReadLine();
        }
    }
}
