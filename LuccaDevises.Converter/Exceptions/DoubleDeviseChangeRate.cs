using System;
using System.Runtime.Serialization;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class DoubleDeviseChangeRate : Exception
    {
        public DoubleDeviseChangeRate(string deviseCible, string deviseSource) :
            base("Le tuple " + deviseCible + " " + deviseSource  + " est déjà existant")
        {
        }
    }
}