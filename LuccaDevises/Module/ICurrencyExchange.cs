﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises.Module
{
    public interface ICurrencyExchange
    {
        decimal ConvertCurrencyFromExternalFile(string FullFilePath);
    }
}
