using System;
using System.Runtime.Serialization;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class IncorrectFile : Exception
    {
        public IncorrectFile(string message) :
            base("Le fichier était mal formé : " + message)
        {
        }
    }
}