using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UpdatePrices
{
    class CompareFiles
    {
        private string _pathToNewFile;
        private List<string> _newTariffList;
        private Dictionary<string, string> _formatedBeelineFile;
        private List<string> _allLinesForNewFile;

        public CompareFiles(string pathToNewFile, List<string> newTariffList, Dictionary<string, string> formatedBeelineFile)
        {
            _pathToNewFile = pathToNewFile;
            _newTariffList = newTariffList;
            _formatedBeelineFile = formatedBeelineFile;
            _allLinesForNewFile = new List<string>();
        }

        public void CreateDifferentTarifFile()
        {
            if (_formatedBeelineFile != null)
            {
                foreach (string line in _newTariffList)
                {
                    string[] splitedLine = line.Split(';');
                    if (_formatedBeelineFile.ContainsKey(splitedLine[1]))
                    {
                        double newPrice = Double.Parse(_formatedBeelineFile[splitedLine[1]]);
                        double oldPrice = Double.Parse(splitedLine[5]);
                        if (newPrice != oldPrice)
                        {
                            double c = newPrice - oldPrice;
                            double fullAltesPrice = c + Double.Parse(splitedLine[2]);
                            double discountOneAltesPrice = c + Double.Parse(splitedLine[3]);
                            double discountTwoAltesPrice = c + Double.Parse(splitedLine[4]);
                            string newAltesPrices = fullAltesPrice.ToString("F") + ";" + discountOneAltesPrice.ToString("F") + ";" + discountTwoAltesPrice.ToString("F");
                            _allLinesForNewFile.Add(line + ";" + _formatedBeelineFile[splitedLine[1]] + ";" + newAltesPrices);
                        }
                        else
                        {
                            _allLinesForNewFile.Add(line + ";" + _formatedBeelineFile[splitedLine[1]] + ";null;null;null");
                        }
                        //_allLinesForNewFile.Add(line + ";" + _formatedBeelineFile[splitedLine[1]]);
                    }
                    else
                    {
                        _allLinesForNewFile.Add(line + ";null;null;null;null");
                    }
                }
                File.AppendAllLines(_pathToNewFile, _allLinesForNewFile, Encoding.UTF8);
            }
            else
            {
                Console.WriteLine("Отфарматированный словарь пуст.");
            }
        }
    }
}
