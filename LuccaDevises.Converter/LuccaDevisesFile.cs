using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace LuccaDevises.Converter
{
    public class LuccaDevisesFile
    {
        private int _numberOfDevises;
        public int NumberOfDevises
        {
            get
            {
                return _numberOfDevises;
            }
        }

        private List<DeviseChangeRateTuple> _devisesChangeRateTuples = new List<DeviseChangeRateTuple>();
        public List<DeviseChangeRateTuple> DevisesChangeRateTuples
        {
            get
            {
                return _devisesChangeRateTuples;
            }
        }

        private DeviseToConvert _deviseToConvert;
        public DeviseToConvert DeviseToConvert
        {
            get
            {
                return _deviseToConvert;
            }
        }

        public static string convertToDeviseRegex = @"([A-Za-z]{3});(\d+);([A-Za-z]{3})";
        public static string numberOfDevisesChangeRates = @"\d+";
        public static string devisesChangeRatesRegex = @"([A-Za-z]{3});([A-Za-z]{3});(\d+.\d{4})";

        public List<string> linesFormats = new List<string>
        {
            convertToDeviseRegex,
            numberOfDevisesChangeRates,
            devisesChangeRatesRegex
        };

#region Format data from input
        private DeviseChangeRateTuple GetDevisesChangeRates(string line)
        {
            string deviseCible = "";
            string deviseSource = "";
            string taux = "";

            Regex deviseToConvert = new Regex(devisesChangeRatesRegex);
            var match = deviseToConvert.Match(line);
            deviseSource = match.Groups[1].Value;
            deviseCible = match.Groups[2].Value;
            taux = match.Groups[3].Value;
            return new DeviseChangeRateTuple(deviseSource, deviseCible, taux);
        }

        private DeviseToConvert GetDeviseToConvert(string line)
        {
            string deviseCible = "";
            string deviseSource = "";
            string taux = "";

            Regex deviseToConvert = new Regex(convertToDeviseRegex);
            var match = deviseToConvert.Match(line);
            deviseSource = match.Groups[1].Value;
            taux = match.Groups[2].Value;
            deviseCible = match.Groups[3].Value;
            return new DeviseToConvert(deviseSource, taux, deviseCible);
        }
#endregion

        private bool CheckRule(string line, string regex)
        {
            Regex r = new Regex(regex);
            return r.IsMatch(line);
        }

        public LuccaDevisesFile(List<string> lines)
        {
            if (lines == null)
                throw new IncorrectFile("Les lignes sont nulles");

            if (lines.Count < 3)
                throw new IncorrectFile(string.Join("\r\n", lines));

            for (int i = 0; i < lines.Count; i++)
            {
                int lineFormatIndex = i < 2 ? i : 2; // which regex to check : 0 - 1 : equiv, > 1 : 2

                if (!CheckRule(lines[i], linesFormats[lineFormatIndex]))
                    throw new MisformatLine(lines[i], linesFormats[lineFormatIndex]);
            }
            _deviseToConvert = GetDeviseToConvert(lines[0]);
            _numberOfDevises = int.Parse(lines[1]);

            if (lines.Count - 2 < _numberOfDevises)
                throw new NotEnoughChangeRates(_numberOfDevises.ToString());

            foreach (var line in lines.GetRange(2, lines.Count - 2))
            {
                var newDeviseChangeRate = GetDevisesChangeRates(line);
                if (_devisesChangeRateTuples.Any(dcrt => dcrt.DeviseCible == newDeviseChangeRate.DeviseCible && dcrt.DeviseSource == newDeviseChangeRate.DeviseSource) ||
                    _devisesChangeRateTuples.Any(dcrt => dcrt.DeviseCible == newDeviseChangeRate.DeviseSource && dcrt.DeviseSource == newDeviseChangeRate.DeviseCible))
                    throw new DoubleDeviseChangeRate(newDeviseChangeRate.DeviseCible, newDeviseChangeRate.DeviseSource);
                _devisesChangeRateTuples.Add(newDeviseChangeRate);
            }

            if (!_devisesChangeRateTuples.Any(dcrt => dcrt.DeviseCible == _deviseToConvert.DeviseCible || dcrt.DeviseSource == _deviseToConvert.DeviseCible))
                throw new MissingDevise(_deviseToConvert.DeviseCible);

            if (!_devisesChangeRateTuples.Any(dcrt => dcrt.DeviseCible == _deviseToConvert.DeviseSource || dcrt.DeviseSource == _deviseToConvert.DeviseSource))
                throw new MissingDevise(_deviseToConvert.DeviseSource);
        }
    }
}