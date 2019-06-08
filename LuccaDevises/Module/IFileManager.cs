using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises.Module
{
    public interface IFileManager
    {
        object[] ReadExternalCurrencyExchageFile(string FullFilePath);
    }
}
