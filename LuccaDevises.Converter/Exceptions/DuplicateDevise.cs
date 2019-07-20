using System;
using System.Runtime.Serialization;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class DuplicateDevise : Exception
    {
        public DuplicateDevise(string message) :
            base ("La devise est la même en entrée et sortie : " + message)
        {
        }
    }
}