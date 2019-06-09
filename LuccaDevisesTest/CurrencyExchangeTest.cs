using LuccaDevises.Entities;
using LuccaDevises.Module;
using Moq;
using NUnit.Framework;
using System;

namespace LuccaDevisesTest
{
    [TestFixture]
    public class CurrencyExchangeTest
    {
        private readonly ICurrencyExchange currencyExchange;

        public CurrencyExchangeTest()
        {
            var fileManager = new FileManager();
            currencyExchange = new CurrencyExchange(fileManager);
        }

        /// <summary>
        /// Test OK
        /// </summary>
        [Test]
        public void OK_ConvertCurrencyFromExternalFile()
        {
            //Arrange
            var filePath = "..\\..\\..\\DummyFiles\\DummyDefault_CurrencyToConvert.csv";

            //Act
            var result = currencyExchange.ConvertCurrencyFromExternalFile(filePath);

            //Assert
            Assert.AreEqual((decimal)59033, result);         
        }

        /// <summary>
        /// Test KO - No match because no information about input currency
        /// </summary>
        [Test]
        public void KO_ConvertCurrencyFromExternalFile_InputCurrencyWasNotInRateList()
        {
            //Arrange
            var filePath = "..\\..\\..\\DummyFiles\\DummyKO_NoInputCurrencyInRateList.csv";
            var dummyInputCurrencyFrom = "EUR";

            //Act
            var ex = Assert.Throws<Exception>(() => currencyExchange.ConvertCurrencyFromExternalFile(filePath));

            //Assert
            Assert.That(ex.Message == $"Error : Currency rate list doesn't contain the Input Currency : <{dummyInputCurrencyFrom}>");
        }

        /// <summary>
        /// Test KO - No match because information was incomplete
        /// </summary>
        [Test]
        public void KO_ConvertCurrencyFromExternalFile_NoMatchFound()
        {
            //Arrange
            var filePath = "..\\..\\..\\DummyFiles\\DummyKO_CurrencyToConvert.csv";

            //Act
            var ex = Assert.Throws<Exception>(() => currencyExchange.ConvertCurrencyFromExternalFile(filePath));

            //Assert
            Assert.That(ex.Message == "Error : List of currency rates incomplete - No match was found");
        }

        /// <summary>
        /// Test KO - Negative value
        /// </summary>
        [Test]
        public void KO_ConvertCurrencyFromExternalFile_NegativeValue()
        {
            //Arrange
            var filePath = "..\\..\\..\\DummyFiles\\DummyKO_NegativeDeviseToConvert.csv";

            //Act
            var ex = Assert.Throws<Exception>(() => currencyExchange.ConvertCurrencyFromExternalFile(filePath));

            //Assert
            Assert.That(ex.Message == "Error : Currency rate cannot be negative !");
        }
    }
}
