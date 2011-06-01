using System.Collections.Generic;
using WebReflectorContracts;

namespace HtmlProvider
{
    public static class HtmlGen
    {
        public static IHtmlEmiter Page(IHtmlEmiter head, IHtmlEmiter body, int indentationLevel)
        {
            return new HtmlElement("html", null, indentationLevel, head, body);
        }

        public static IHtmlEmiter Head(int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("head", null, indentationLevel, content);
        }

        public static IHtmlEmiter Body(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("body", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Anchor(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("a", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Ul(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("ul", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Li(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("li", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Ol(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("ol", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Dl(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("dl", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Dt(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("dt", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Dd(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("dd", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter B(int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("b", null, indentationLevel, content);
        }

        public static IHtmlEmiter P(int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("p", null, indentationLevel, content);
        }

        public static IHtmlEmiter I(int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("i", null, indentationLevel, content);
        }

        public static IHtmlEmiter Table(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("table", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Th(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("th", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Tr(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("tr", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Td(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("td", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Fieldset(IDictionary<string, string> atributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            return new HtmlElement("fieldset", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Legend(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("legend", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter H1(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("h1", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter H2(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("h2", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter H3(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("h3", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter H4(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("h4", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter H5(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("h5", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter H6(IDictionary<string, string> atributes, int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("h6", atributes, indentationLevel, content);
        }

        public static IHtmlEmiter Text(string content, int indentationLevel)
        {
            return new TextHtmlElement(content, indentationLevel);
        }

        public static IHtmlEmiter Title(int indentationLevel, IHtmlEmiter content)
        {
            return new HtmlElement("title", null, indentationLevel, content);
        }

        public static IHtmlEmiter Br(int indentationLevel)
        {
            return new EmptyHtmlElement("br", indentationLevel);
        }

        public static IHtmlEmiter Hr(int indentationLevel)
        {
            return new EmptyHtmlElement("hr", indentationLevel);
        }
    }
}