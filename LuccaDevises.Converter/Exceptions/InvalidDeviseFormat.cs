using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class InvalidDeviseFormat : Exception
    {
        public InvalidDeviseFormat( string message)
            :base("Le format de la devise est incorrect : " + message)
        {

        }
    }
}
