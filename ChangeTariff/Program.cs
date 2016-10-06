﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChangeTariff
{
    class Program
    {                
        static void Main(string[] args)
        {
            
            Console.WriteLine("Введите путь к файлу со старыми тарифами из Beeline");
            string pathToOldFile = Console.ReadLine();

            Console.WriteLine("Введите путь к файлу со старыми тарифами из UTM");
            string pathToUtmFile = Console.ReadLine();

            Console.WriteLine("Введите путь к файлу с новыми тарифами из Beeline");
            string pathToNewFile = Console.ReadLine();

            Console.WriteLine("Введите путь к результирующему файлу");
            string pathToTargetFile = Console.ReadLine();

            FormatFileFromUTM fromUtmFile = new FormatFileFromUTM(pathToUtmFile, pathToOldFile);
            List<string> oldComplexTariff = fromUtmFile.RunFormat();

            FormatNewBeeineFile fromNewBeelineFile = new FormatNewBeeineFile(pathToNewFile);
            Dictionary<string, string> complexNewTariff = fromNewBeelineFile.RunFormat();

            foreach (KeyValuePair<string, string> kvp in complexNewTariff)
            {
                Console.WriteLine(kvp.Key + "   " + kvp.Value);
                //File.AppendAllText(@"ttt.csv", Environment.NewLine + kvp.Key + ";" + kvp.Value, Encoding.UTF8);                
            }
            CompareFiles compareObject = new CompareFiles(pathToTargetFile, oldComplexTariff, complexNewTariff);
            compareObject.CreateDifferentTarifFile();
            Console.WriteLine("Для выхода из приложения нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}