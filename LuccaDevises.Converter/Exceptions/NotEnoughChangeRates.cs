using System;
using System.Runtime.Serialization;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class NotEnoughChangeRates : Exception
    {
        public NotEnoughChangeRates(string message) :
            base("Nombre de taux attendus : " + message)
        {
        }
    }
}