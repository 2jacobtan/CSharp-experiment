#nullable enable

using System;
using System.Linq;
using System.Numerics;
using System.Globalization;
using static MyNamespace.ConsoleOutputHelpers;

namespace MyNamespace
{
    static class FunctionChaining
    {
        //pipe-forward extension methods
        internal static U _<T,U>(this T input, Func<T,U> fun) => fun(input);
        internal static void _<T>(this T input, Action<T> fun) => fun(input);
    }
    static class ConsoleOutputHelpers
    {
        static internal void print0(params Object[] args) =>
            args
            .Select(x=>x?.ToString()) //unnecessary
            ._(_=>string.Join(' ',_))
            ._(Console.WriteLine);
        
        //no need to use ToString() before string.Join()
        static internal void print(params Object[] args) =>
            args
            ._(_=>string.Join(' ',_))
            ._(Console.WriteLine);
    }
    class Program
    {
        static int product(int a, int b) => a + b;

        static void Main(string[] args)
        {
            print("Hello World!");
            print();

            //Forward-pipe
            int num = 2;
            num._(_=>product(_,3))
            ._(_=>print("Product =", _));

            print("\n--------\n");

            string idString = "8a847645-8cac-422c-962a-fdf3aa220065";
            // print( Guid.Parse(idString).ToString("x") );
            print("Guid ", Guid.Parse(idString).ToString("d") );
            print();

        #region Guid string to hex string
            print("_Guid -> Hex_") ;

            //wrapped in string.Concat
            string idHex = "0x" + string.Concat(
                Guid.Parse(idString)
                .ToByteArray()
                .Select(b=>b.ToString("x2"))
            );
            print("Hex  ", idHex);

            //string.Concat chained at the end
            string idHex2 = "0x" +
                idString._(Guid.Parse)
                .ToByteArray()
                .Select(b=>b.ToString("x2"))
                ._(string.Concat);
            print("Hex2 ", idHex2);
            print();
        #endregion


        #region Hex string to Guid string
            print("_Hex -> Guid_");

            //new Guid() in front
            Guid guid = new Guid(
                "04576848aac8c2c42962afdf3aa220065"
                ._(_=>BigInteger.Parse(_,NumberStyles.HexNumber))
                .ToByteArray(isBigEndian:true)
            );
            //new Guid() behind
            Guid guid2 = 
                "04576848aac8c2c42962afdf3aa220065"
                ._(_=>BigInteger.Parse(_,NumberStyles.HexNumber))
                .ToByteArray(isBigEndian:true)
                ._(_=>new Guid(_));
            print("Guid  ", guid);
            print("Guid2 ", guid2);


            //testing BigInteger.ToByteArray()
            print();
            print(BigInteger
                .Parse("04576848aac8c2c42962afdf3aa220065",NumberStyles.HexNumber)
                .ToString("X")
            );
            print(BigInteger
                .Parse("04576848aac8c2c42962afdf3aa220065",NumberStyles.HexNumber)
                .ToByteArray(isBigEndian:true) //isBigEndian:True is needed
                .Select(b=>b.ToString("x2"))._(string.Concat)
            );
            print(BigInteger
                .Parse("04576848aac8c2c42962afdf3aa220065",NumberStyles.HexNumber)
                .ToByteArray() //isBigEndian:True is needed
                .Reverse() //otherwise use Array.Reverse()
                .Select(b=>b.ToString("x2"))._(string.Concat)
            );
        #endregion

        }
    }
}

// Output:
/* 
Hello World!

Product = 5

--------

Guid  8a847645-8cac-422c-962a-fdf3aa220065

_Guid to Hex_
Hex   0x4576848aac8c2c42962afdf3aa220065
Hex2  0x4576848aac8c2c42962afdf3aa220065

_Hex to Guid_
Guid   8a847645-8cac-422c-962a-fdf3aa220065
Guid2  8a847645-8cac-422c-962a-fdf3aa220065

4576848AAC8C2C42962AFDF3AA220065
4576848aac8c2c42962afdf3aa220065
4576848aac8c2c42962afdf3aa220065
 */