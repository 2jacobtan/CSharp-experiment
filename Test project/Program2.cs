/* using System;
using System.Collections.Generic;
using System.Numerics; //BigInt
using System.Linq;

namespace myNamespace1
{
  public static class FunctionChaining
    {
        static U _<T,U>(this Object input, Func<T,U> fun) => fun((T)input);
        static string aoeu(this IEnumerable<string> input, Func<IEnumerable<string>,string> fun) => fun(input);
        static string aoeu(this string input, Func<string,string> fun) => fun(input);
        // static string aoeu(this string input, string bar) => input + bar;
        public static string aoeu(this string input, string bar) {return input + bar;}
    }  
}

namespace myNamespace2
{
    using myNamespace1; //extension methods

    class Program1
    {
        static void print(string str = null) => Console.WriteLine(str);
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
            Console.WriteLine(i is IEnumerable<int> ? (IEnumerable<int>)i : (IEnumerable<int>)null);
            Console.WriteLine ("\n------");
            int? a = null, b = 3, c;
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
            string idHex = "0x" + string.Concat(Guid.Parse(idString).ToByteArray().Select(b=>b.ToString("x")));
            print(idHex);
            string idHex1 = idHex.aoeu(s => s.ToUpper());
            // string idHex2 = "0x" + Guid.Parse(idString).ToByteArray().Select(b=>b.ToString("x"));
            // print(idHex2);
            print(Guid.Parse(idString).ToString("X"));
        }
    }
}
 */