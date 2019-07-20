using System;
using System.Runtime.Serialization;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class InvalidDeviseChangeRateFormat : Exception
    {
        public InvalidDeviseChangeRateFormat(string message)
            :base("Le format du taux de change est incorrect : " + message)
        {
        }
    }
}