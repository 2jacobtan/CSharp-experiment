#nullable enable

using System;
using System.Collections.Generic;
using System.Numerics; //BigInt
using System.Linq;

namespace myNamespace
{
    static class FunctionChaining
    {
        internal static U _<T,U>(this T input, Func<T,U> fun) => fun(input);
        static string aoeu(this IEnumerable<string> input, Func<IEnumerable<string>,string> fun) => fun(input);
        internal static string aoeu1(this string input, Func<string,string> fun) => fun(input);
        internal static string aoeu2(this string input, string bar) => input + bar;
    }
    class Program
    {
        static int prod(int a, int b) => a + b;
        static void print(Object? obj = null) => Console.WriteLine(obj is null ? null : obj.ToString());
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine ("\n------");
            int? j = null;
            Console.WriteLine (j ??= 42);            

            Console.WriteLine ("\n------");
            int? k = 3;
            Console.WriteLine (k ??= 42);

            Console.WriteLine ("\n------");
            List<int> i = new List<int>() {1,2};
            Console.WriteLine(i is IEnumerable<int>);
            Console.WriteLine(i as IEnumerable<int>);
            Console.WriteLine(i is IEnumerable<int> ? (IEnumerable<int>)i : (IEnumerable<int>?)null);

            Console.WriteLine ("\n------");
            int? a = null, b = 3;
            Console.WriteLine(a is int);
            Console.WriteLine(a as int?);
            Console.WriteLine(a is int? ? (int?)a : (int?)null);
            Console.WriteLine("\n------");
            Console.WriteLine(i is null);
            if (a*b is int d) Console.WriteLine(d) ;
            d=1;

            Console.WriteLine("\n------");
            Console.WriteLine("_Guid_") ;
            print();
            string idString = "8a847645-8cac-422c-962a-fdf3aa220065";
            string idHex = "0x" + string.Concat(Guid.Parse(idString).ToByteArray().Select(b=>b.ToString("x2")));
            print(idHex);
            string idHex1 = idHex.aoeu1(s => s.ToUpper());
            print("1 "+idHex1);
            string idHex2 = "0x" + idString._(Guid.Parse).ToByteArray().Select(b=>b.ToString("x2"))._(string.Concat);
            print("2 " + idHex2);
            print(Guid.Parse(idString).ToString("X"));

            Console.WriteLine("\n------");
            int num = 2;
            print( num._(_=>prod(_,3)) );
        }
    }
}
