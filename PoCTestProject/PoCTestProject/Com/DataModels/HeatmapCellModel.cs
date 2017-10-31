using System;
using System.Collections.Generic;

namespace PoCTestProject.Com.DataModels
{
    class HeatmapCellModel
    {

        public Dictionary<string, string> colors = new Dictionary<string, string>();
        //there is always at least one - grey (color)
        private int numberOfColors = 0;

        public int CalculateVisibleColors()
        {
            numberOfColors = 0;
            foreach (KeyValuePair < string, string> entry in colors)
            {
                Console.WriteLine("{0} : {1}", entry.Key, entry.Value);
                if(Convert.ToDouble(entry.Value) != 0.00)
                {
                    numberOfColors++;
                }
            }
            Console.WriteLine("Color Count: {0}", numberOfColors);
            return numberOfColors;
        }

    }
}
