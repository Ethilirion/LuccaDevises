using System;
using System.Runtime.Serialization;

namespace LuccaDevises.Converter
{
    [Serializable]
    public class MisformatLine : Exception
    {
        private string _line;
        private string _regex;
        
        public MisformatLine(string line, string regex) :  base($"Les lignes suivantes sont incorrectes (ligne - regex) : {line} - {regex}")
        {
            this._line = line;
            this._regex = regex;
        }
    }
}