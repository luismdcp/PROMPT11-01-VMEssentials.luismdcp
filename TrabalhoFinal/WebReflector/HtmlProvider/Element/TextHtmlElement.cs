using System.Text;

namespace HtmlProvider.Element
{
    public class TextHtmlElement : HtmlElementBase
    {
        public string Content { get; set; }
        public int IndentationLevel { get; set; }

        public TextHtmlElement(string content, int indentationLevel)
        {
            this.Content = content;
            IndentationLevel = indentationLevel;
        }

        public override string EmitHtml()
        {
            StringBuilder buffer = new StringBuilder();
            string indentation = BuildIndentation(IndentationLevel);

            buffer.Append(indentation);
            buffer.AppendLine(Content);

            return buffer.ToString();
        }
    }
}