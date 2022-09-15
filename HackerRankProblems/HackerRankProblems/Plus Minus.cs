using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'plusMinus' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static void plusMinus(List<int> arr)
    {
        int Positives = 0;
        int Negatives = 0;
        int Zeroes = 0;

        for (
            int i = 0; i < arr.Count; i++)
        {
            if (arr[i] < 0)
            {
                Negatives++;
            }
            else if (arr[i] > 0)
            {
                Positives++;
            }
            else if (arr[i] == 0)
            {
                Zeroes++;
            }
        }
        List<decimal> ratios = new List<decimal>();
        decimal length = Convert.ToDecimal(arr.Count);

        decimal PosRatio = Positives / length;
        ratios.Add(PosRatio);
        decimal NegRatio = Negatives / length;
        ratios.Add(NegRatio);
        decimal ZerRatio = Zeroes / length;
        ratios.Add(ZerRatio);

        for (int i = 0; i < ratios.Count; i++)
        {
            Console.WriteLine(Math.Round(ratios[i], 6));
        }
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.plusMinus(arr);
    }
}
