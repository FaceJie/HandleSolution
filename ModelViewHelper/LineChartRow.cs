using System;
using System.Collections.Generic;
using System.Text;

namespace ModelViewHelper
{
    public class LineChartRow
    {
        public DateTime Time { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? AverageBuy { get; private set; }
        public decimal? AverageSell { get; private set; }


        public LineChartRow() { }
        public LineChartRow(DateTime time, decimal? value, decimal? averageBuy, decimal? averageSell)
        {
            Time = time;
            Value = value;
            AverageBuy = averageBuy;
            AverageSell = averageSell;
        }
    }

    public class LineChartColumn
    {
        public string Label { get; private set; }
        public string Type { get; private set; }


        public LineChartColumn() { }
        public LineChartColumn(string type, string label)
        {
            Type = type;
            Label = label;
        }
    }
}
