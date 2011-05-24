using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Drawing;


namespace Session4.Binding.Tests
{
    [TestFixture]
    public class BinderTests
    {
        class A
        {
            [Bindable(Name = "UmInteiro", Required = true)]
            public int AnInteger { get; set; }
            [Bindable(Name = "UmaString", Required = true)]
            public string AString;
        }

        class B
        {
            [Bindable(Name = "UmInteiro", Required = true)]
            public int AnInteger { get; set; }
            [Bindable(Name = "UmaString", Required = true)]
            public string AString;
            public Point APoint;

        }

        class C
        {
            public int AnInteger { get; set; }
            [Bindable(Name = "UmaString", Required = false)]
            public string AString;
        }

        [Test]
        public void cand_bind_to_int_and_string_prop_and_field()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
                            {
                                new KeyValuePair<string, string>("AnInteger", "2"),
                                new KeyValuePair<string, string>("AString", "abc")
                            };

            var a = binder.BindTo<A>(pairs);

            Assert.AreEqual(2, a.AnInteger);
            Assert.AreEqual("abc", a.AString);
        }

        [Test]
        public void test()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
                            {
                                new KeyValuePair<string, string>("AnInteger", "2"),
                                new KeyValuePair<string, string>("AString", "abc"),
                                new KeyValuePair<string, string>("APoint", "1,2"), 
                            };

            var exc = Assert.Throws<InvalidMemberTypeException>(() => binder.BindTo<B>(pairs));
            Assert.AreEqual(typeof(B).GetField("APoint"), exc.MemberInfo);
        }

        [Test]
        public void cand_bind_only_to_bindable_fields_or_properties_that_are_required()
        {
            var binder = new Binder();
            var pairs = new KeyValuePair<string, string>[]
                            {
                                new KeyValuePair<string, string>("AnInteger", "2"),
                                new KeyValuePair<string, string>("AString", "abc")
                            };

            var a = binder.BindTo<C>(pairs);

            Assert.AreEqual(default(Int32), a.AnInteger);
            Assert.AreEqual(default(String), a.AString);
        }
    }
}