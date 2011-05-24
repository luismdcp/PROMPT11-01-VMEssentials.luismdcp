using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sessao5Practica
{
    public class OrderedSequence<T> : IEnumerable<T>
    {
        private Comparison<T> comparerDelegate;
        private IEnumerable<T> elements;

        public Comparison<T> Comparer { get { return this.comparerDelegate; } }
        public IEnumerable<T> Elements { get { return this.elements; } }

        public OrderedSequence(Comparison<T> comparerDelegate, IEnumerable<T> elements)
        {
            this.comparerDelegate = comparerDelegate;
            this.elements = elements;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}