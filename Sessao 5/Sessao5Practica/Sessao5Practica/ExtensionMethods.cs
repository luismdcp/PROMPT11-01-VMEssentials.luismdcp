using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sessao5Practica
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> RemoveRepeated<T>(this IEnumerable<T> seq)
        {
            List<T> buffer = new List<T>();

            foreach (var element in seq)
            {
                if (!buffer.Contains(element))
                {
                    buffer.Add(element);
                    yield return element;
                }
            }
        }

        public static IEnumerable<T> OrderBy<T, U>(this IEnumerable<T> seq, Func<T, U> criterium)
                   where U : IComparable<U>
        {
            OrderedSequence<T> sequence = new OrderedSequence<T>((t1, t2) => criterium(t1).CompareTo(criterium(t2)), seq);
            return sequence;

            //var a = seq.ToArray();
            //Array.Sort(a, (t1, t2) => criterium(t1).CompareTo(criterium(t2)));
            //return a;
        }

        public static IEnumerable<T> ThenBy<T, U>(this OrderedSequence<T> seq, Func<T, U> criterium)
            where U : IComparable<U>
        {
            var a = seq.Elements.ToArray();
            var c = seq.Comparer;

            Array.Sort(a, (t1, t2) =>  c(t1,t2) + criterium(t1).CompareTo(criterium(t2)));
            
            return a;
        }
    }
}