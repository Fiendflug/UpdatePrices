using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UpdatePrices
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
            try
            {
                FormatFileFromUTM fromUtmFile = new FormatFileFromUTM(pathToUtmFile, pathToOldFile);
                List<string> oldComplexTariff = fromUtmFile.RunFormat();

                FormatNewBeeineFile fromNewBeelineFile = new FormatNewBeeineFile(pathToNewFile);
                Dictionary<string, string> complexNewTariff = fromNewBeelineFile.RunFormat();

                CompareFiles compareObject = new CompareFiles(pathToTargetFile, oldComplexTariff, complexNewTariff);
                compareObject.CreateDifferentTarifFile();

                Console.WriteLine("Для выхода из приложения нажмите любую клавишу");
                Console.ReadKey();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Console.WriteLine("Для выхода из приложения нажмите любую клавишу");
                Console.ReadKey();
            }
            
        }
    }
}
