using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sessao5Practica;

namespace Sessao5PracticaTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void test1()
        {
            List<int> buffer = new List<int>{1, 2, 1, 3, 5, 2};
            Assert.IsTrue(buffer.RemoveRepeated().Count() == 4);
        }
    }
}