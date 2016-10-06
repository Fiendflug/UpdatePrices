using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChangeTariff
{
    class FormatNewBeeineFile
    {
        private string _path;
        private Dictionary<string, string> _formatedLines;

        public FormatNewBeeineFile(string path)
        {
            _path = path;
            _formatedLines = new Dictionary<string, string>();
        }

        public Dictionary<string, string> RunFormat()
        {
            foreach (string s in File.ReadAllLines(_path, Encoding.UTF8))
            {
                string[] allColumns = s.Split(';');
                string[] rollups = allColumns[2].Split(',');
                foreach (string rollup in rollups)
                {
                    if (rollup.Trim().Contains("-"))
                    {
                        string[] rollupsRange = rollup.Split('-');
                        if (rollupsRange[0].Trim().IndexOf('0') == 0 && rollupsRange[1].Trim().IndexOf('0') == 0)
                        {
                            int fCounter = 0;
                            int fLCounter = 0;
                            for (int i = 0; i < rollupsRange[0].Length; i++)
                            {
                                if (i > 0|| rollupsRange[0][i] == '0' || rollupsRange[1][i-1] == '0')
                                {
                                    fCounter++;
                                }
                            }
                            for (int i = 0; i < rollupsRange[1].Length; i++)
                            {
                                if (i > 0 || rollupsRange[0][i] == '0' || rollupsRange[1][i - 1] == '0')
                                {
                                    fLCounter++;
                                }
                            }
                            if (fCounter == fLCounter)
                            {
                                string zero = null;
                                for (int i = 0; i < fCounter; i++)
                                {
                                    zero += "0";
                                }
                                for (long i = Int64.Parse(rollupsRange[0].Trim()); i <= Int64.Parse(rollupsRange[1].Trim()); i++)
                                {
                                    _formatedLines.Add(allColumns[1].Trim() + zero + i.ToString(), allColumns[3]);
                                }
                            }
                            else
                            {
                                //Если разница в ренже состовляет более одного разряда
                            }
                        }
                        else
                        {
                            for (long i = Int64.Parse(rollupsRange[0].Trim()); i <= Int64.Parse(rollupsRange[1].Trim()); i++)
                            {
                                _formatedLines.Add(allColumns[1].Trim() + i.ToString(), allColumns[3]);
                            }
                        }
                    }
                    else
                    {
                        _formatedLines.Add(allColumns[1].Trim() + rollup.Trim(), allColumns[3]);
                    }
                }
            }
            return _formatedLines;
        }
    }
}
