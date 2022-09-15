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
     * Complete the 'miniMaxSum' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static void miniMaxSum(List<long> arr)
    {
        long maxsum=0;
        long minsum=0;
        long[] values = new long[arr.Count];
        for (int i = 0; i < arr.Count; i++)
        {
            long sum = 0;
            long valor = arr[i];
            arr[i] = 0;
            

            for (int b = 0; b < arr.Count; b++)
            {
                sum = sum + arr[b];
            }
            values[i] = sum;
            arr[i] = valor;
        }
        Array.Sort(values);
        minsum = values[0];
        maxsum = values[values.Length - 1];

        Console.WriteLine($"{minsum} {maxsum}");
    }

}

class Solution
{
    public static void Main(string[] args)
    {

        List<long> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt64(arrTemp)).ToList();

        Result.miniMaxSum(arr);
    }
}
