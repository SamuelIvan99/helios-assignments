﻿using System.Collections.Generic;
using System.Globalization;
using static TradeProcessing.Processor.EpexParser;

namespace TradeProcessing.Processor.Interfaces
{
    public interface IEpexParser
    {
        IReadOnlyList<EpexRow> Parse(string csv, CultureInfo culture);
    }
}