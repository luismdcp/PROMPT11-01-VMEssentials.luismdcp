using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sessao5Practica
{
    class Program
    {
        public struct Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public DateTime PublishDate { get; set; }
        }

        static int RationalNumberComparer(RationalNumber rn1, RationalNumber rn2)
        {
            return (Convert.ToInt32(rn1.Numerator) / Convert.ToInt32(rn1.Denominator)) - (Convert.ToInt32(rn1.Numerator) / Convert.ToInt32(rn1.Denominator));
        }

        static void Main(string[] args)
        {
            Comparison<RationalNumber> comparer;

            List<RationalNumber> rnumbers = new List<RationalNumber>
                                                {
                                                    new RationalNumber(3, 2),
                                                    new RationalNumber(1, 1),
                                                    new RationalNumber(2, 1),
                                                };

            var n = rnumbers.Where(r => r.Equals(new RationalNumber(1, 1)));

            foreach (var r in n)
                Console.WriteLine(r);

            rnumbers.Sort(new Comparison<RationalNumber>(RationalNumberComparer));

            foreach (var r in rnumbers)
                Console.WriteLine(r);

            List<Book> books = new List<Book>
                    {
                        new Book {Title = "Ensaio sobre a cegueira", Author = "Saramago",
                                PublishDate = new DateTime(2005, 12, 3) },
                        new Book {Title = "Memorial do Convento", Author = "Saramago",
                                PublishDate = new DateTime(1984, 12, 3) },
                        new Book {Title = "Lusiadas", Author = "Camoes",
                                PublishDate = new DateTime(1600, 12, 3) }
                    };

            var ordered = (OrderedSequence<Book>) books.OrderBy(b => b.Author);
            var thenBy = ordered.ThenBy(b => b.Title);
        }
    }
}