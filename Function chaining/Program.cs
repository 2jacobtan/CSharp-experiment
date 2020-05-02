#nullable enable

using System;
using System.Linq;
using System.Numerics;
using System.Globalization;

namespace myNamespace
{
    static class FunctionChaining
    {
        internal static U _<T,U>(this T input, Func<T,U> fun) => fun(input);
        internal static void _<T>(this T input, Action<T> fun) => fun(input);
    }
    class Program
    {
        //console output helper
        static void print(params Object[] args) => args.Select(x=>x?.ToString())._(_=>string.Join(' ',_))._(Console.WriteLine);
        static void print2(params Object[] args) => args._(_=>string.Join(' ',_))._(Console.WriteLine);

        static int product(int a, int b) => a + b;

        static void Main(string[] args)
        {
            print("Hello World!");
            print("\n------\n");
            
            print("_Guid_") ;
            string idString = "8a847645-8cac-422c-962a-fdf3aa220065";

            // wrapped in string.Concat (sad)
            string idHex = "0x" + string.Concat(Guid.Parse(idString).ToByteArray().Select(b=>b.ToString("x2")));
            print("1) ", idHex);

            // string.Concat chained at the end (yay)
            string idHex2 = "0x" + idString._(Guid.Parse).ToByteArray().Select(b=>b.ToString("x2"))._(string.Concat);
            print("2) ", idHex2);

            print( Guid.Parse(idString).ToString("x") );

            print();
            Guid guid = new Guid("04576848aac8c2c42962afdf3aa220065"._(_=>BigInteger.Parse(_,NumberStyles.HexNumber)).ToByteArray(isBigEndian:true));
            Guid guid2 = "04576848aac8c2c42962afdf3aa220065"._(_=>BigInteger.Parse(_,NumberStyles.HexNumber)).ToByteArray(isBigEndian:true)._(_=>new Guid(_));
            print("Guid  ", guid);
            print("Guid2 ", guid2);
            print();
            print(BigInteger.Parse("04576848aac8c2c42962afdf3aa220065",NumberStyles.HexNumber).ToString("X"));
            print(BigInteger.Parse("04576848aac8c2c42962afdf3aa220065",NumberStyles.HexNumber).ToByteArray(isBigEndian:true).Select(b=>b.ToString("x2"))._(string.Concat));

            print("\n------\n");

            // product chained at the end
            int num = 2;
            print( num._(_=>product(_,3)) );
        }
    }
}

// Output:
/* 
Hello World!

------

_Guid_
1) 0x4576848aac8c2c42962afdf3aa22065
2) 0x4576848aac8c2c42962afdf3aa22065
{0x8a847645,0x8cac,0x422c,{0x96,0x2a,0xfd,0xf3,0xaa,0x22,0x00,0x65}}

------

5
 */