using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Converter
{
    public class FileParser
    {
        private string LuccaFilePath;

        public LuccaDevisesFile ParseFile()
        {
            var lines = new List<string>();
            string line = "";

            // Read the file and display it line by line.  
            using (System.IO.StreamReader file = new System.IO.StreamReader(LuccaFilePath))
            {
                try
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        if (file != null)
                            file.Close();
                    }
                    catch 
                    {
                        throw;
                    }
                }
            }

            return new LuccaDevisesFile(lines);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">File path</param>
        /// <exception cref="FileNotFoundException">File does not exist or you do not have the rights</exception>
        public FileParser(string path)
        {
            if (!File.Exists(path) || File.OpenRead(path) == null)
                throw new FileNotFoundException("Le fichier doit être présent et être au format csv");
            LuccaFilePath = path;
        }
    }
}
