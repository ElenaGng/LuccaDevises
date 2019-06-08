using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text;
using FileHelpers;
using FileHelpers.Events;
using LuccaDevises.Entities;

namespace LuccaDevises.Module
{
    public class FileManager : IFileManager
    {
        /// <summary>
        /// Read an external currency exchange file using MultiRecordEngine
        /// </summary>
        /// <param name="FullFilePath"></param>
        /// <returns>An single dimentional array with all the rows in the file</returns>
        public object[] ReadExternalCurrencyExchageFile(string FullFilePath)
        {
            FileInfo finfo = new FileInfo(FullFilePath);

            if (!finfo.Exists || finfo.Length == 0)
            {
                Console.Write($"This file does not exist or is empty : {FullFilePath}");
                return null;
            }

            using (FileStream fs = new FileStream(FullFilePath, FileMode.Open, FileAccess.Read))
            {
                var engine = new MultiRecordEngine(typeof(CurrencyToConvert), typeof(CurrencyRate));

                engine.RecordSelector = new RecordTypeSelector(CustomSelector);

                return engine.ReadFile(FullFilePath);
            }
        }

        /// <summary>
        /// Get the type of the row in the file
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="recordLine"></param>
        /// <returns></returns>
        private Type CustomSelector(MultiRecordEngine engine, string recordLine)
        {
            //Return null if the line is empty or is the second one -> I don't need it ;)
            if (recordLine.Length == 0 || engine.LineNumber == 2)
                return null;

            if (Char.IsNumber(recordLine[5]))
                return typeof(CurrencyToConvert);

            else
                return typeof(CurrencyRate);
        }
    }
}
