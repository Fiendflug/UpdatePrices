using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UpdatePrices
{
    class FormatFileFromUTM
    {
        private string _pathToFileFromUTMFile;
        private string _pathToOldBeelineFile;
        private List<string> _formatedOldTariffFile;
        private Dictionary<string, string> _temporaryDict;

        public FormatFileFromUTM(string pathToFileFromUTMFile, string pathToOldBeelineFile)
        {
            _pathToFileFromUTMFile = pathToFileFromUTMFile;
            _pathToOldBeelineFile = pathToOldBeelineFile;
            _formatedOldTariffFile = new List<string>();
            _temporaryDict = new Dictionary<string, string>();
        }

        public List<string> RunFormat()
        {
            foreach (string line in File.ReadAllLines(_pathToOldBeelineFile, Encoding.UTF8))
            {
                string[] splitedLine = line.Split(';');
                _temporaryDict.Add(splitedLine[1], splitedLine[2]);
            }
            foreach (string lineInUTMFile in File.ReadAllLines(_pathToFileFromUTMFile, Encoding.UTF8))
            {
                string[] splitedLineInUTMFile = lineInUTMFile.Split(';');
                if (_temporaryDict.ContainsKey(splitedLineInUTMFile[1]))
                {
                    _formatedOldTariffFile.Add(lineInUTMFile + ";" + _temporaryDict[splitedLineInUTMFile[1]]);
                }
            }
            return _formatedOldTariffFile;
        }
    }
}
