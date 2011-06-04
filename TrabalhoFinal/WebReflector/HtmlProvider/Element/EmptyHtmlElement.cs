using System;
using System.Text;

namespace HtmlProvider.Element
{
    public class EmptyHtmlElement : HtmlElementBase
    {
        public string Tag { get; set; }
        public int IndentationLevel { get; set; }

        public EmptyHtmlElement(string tag, int indentationLevel)
        {
            this.Tag = tag;
            this.IndentationLevel = indentationLevel;
        }

        public override string EmitHtml()
        {
            StringBuilder buffer = new StringBuilder();
            string indentation = BuildIndentation(IndentationLevel);

            buffer.Append(indentation);
            buffer.AppendLine(String.Format("<{0}/>", this.Tag));

            return buffer.ToString();
        }
    }
}