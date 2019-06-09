using LuccaDevises.Entities;
using LuccaDevises.Module;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LuccaDevisesTest
{
    [TestFixture]
    public class FileManagerTest
    {
        private readonly IFileManager fileManager;

        public FileManagerTest()
        {
            fileManager = new FileManager();
        }

        [Test]
        public void ReadExternalCurrencyExchageFile_OK()
        {
            //Arrange
            var expectedResult = new object[7]
            {
                new CurrencyToConvert { InputCurrencyFrom = "EUR", InputAmountToExchage = 550, InputCurrencyTo = "JPY" },
                new CurrencyRate { CurrencyFrom = "AUD", CurrencyTo = "CHF", Rate = 0.9661M },
                new CurrencyRate { CurrencyFrom = "JPY", CurrencyTo = "KRW", Rate = 13.1151M },
                new CurrencyRate { CurrencyFrom = "EUR", CurrencyTo = "CHF", Rate = 1.2053M },
                new CurrencyRate { CurrencyFrom = "AUD", CurrencyTo = "JPY", Rate = 86.0305M },
                new CurrencyRate { CurrencyFrom = "EUR", CurrencyTo = "USD", Rate = 1.2989M },
                new CurrencyRate { CurrencyFrom = "JPY", CurrencyTo = "INR", Rate = 0.6571M }
            };
            var filePath = "..\\..\\..\\DummyFiles\\DummyDefault_CurrencyToConvert.csv";

            //Act
            var result = fileManager.ReadExternalCurrencyExchageFile(filePath);

            //Assert
            for (int i = 0; i < expectedResult.Length; i++)
            {
                Assert.IsTrue(expectedResult[i].Equals(result[i]));
            }
        }

        [Test]
        public void ReadExternalCurrencyExchageFile_KO_FileNotFound()
        {
            //Arrange
            var dummyFilePathToTest = "pasteque";
            var pathInExceptionMessage = Path.Combine(Path.GetDirectoryName(typeof(FileManagerTest).Assembly.Location), dummyFilePathToTest);

            //Act
            var ex = Assert.Throws<FileNotFoundException>(() => fileManager.ReadExternalCurrencyExchageFile(dummyFilePathToTest));

            //Assert
            Assert.That(ex.Message == $"The file <{dummyFilePathToTest}> was not found: 'Could not find file '{pathInExceptionMessage}'.'");
        }

        [Test]
        public void ReadExternalCurrencyExchageFile_KO_EmptyFile()
        {
            //Arrange
            var filePath = "..\\..\\..\\DummyFiles\\DummyKO_NoData_CurrencyToConvert.csv";

            //Act
            var ex = Assert.Throws<Exception>(() => fileManager.ReadExternalCurrencyExchageFile(filePath));

            //Assert
            Assert.That(ex.Message == $"An error in method ReadExternalCurrencyExchageFile : 'The file <{filePath}> is empty'");
        }
    }
}
