using System;

namespace Sessao1
{
    public class Program
    {
        public static void Main(string[] args)
        {
          Ponto p1 = new Ponto(1,1);
		  Ponto p2 = new Ponto(2,2);
		  
		  int distancia = p2.Distance(p1);
		  
		  Console.WriteLine(p1.ToString());
		  Console.WriteLine(p2.ToString());
		  Console.WriteLine("Distancia: {0}", distancia);
		  
		  Console.WriteLine("CompareTo: {0}", p1.CompareTo(p2));
        }
    }
}