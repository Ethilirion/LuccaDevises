using System;
using System.Runtime.Serialization;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class MissingDevise : Exception
    {
        public MissingDevise(string message) : base("La devise suivante était absente des conversions : " + message)
        {
        }
    }
}