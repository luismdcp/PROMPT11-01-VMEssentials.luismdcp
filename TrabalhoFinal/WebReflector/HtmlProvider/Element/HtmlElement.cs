using System;
using System.Collections.Generic;
using System.Text;
using WebReflectorContracts;

namespace HtmlProvider.Element
{
    public class HtmlElement : HtmlElementBase
    {
        public string Tag { get; set; }
        public IDictionary<string, string> Attributes { get; set; }
        public int IndentationLevel { get; set; }
        public IHtmlEmiter[] Content { get; set; }

        public HtmlElement(string tag, IDictionary<string, string> attributes, int indentationLevel, params IHtmlEmiter[] content)
        {
            this.Tag = tag;
            this.Attributes = attributes;
            IndentationLevel = indentationLevel;
            this.Content = content;
        }

        public override string EmitHtml()
        {
            StringBuilder buffer = new StringBuilder();
            string indentation = BuildIndentation(IndentationLevel);

            buffer.Append(indentation);

            if (Attributes != null)
            {
                if (Attributes.Count == 0)
                {
                    buffer.AppendLine(String.Format("<{0}>", this.Tag));
                }
                else
                {
                    buffer.Append(String.Format("<{0}", this.Tag));

                    foreach (var atributeKey in Attributes.Keys)
                    {
                        buffer.Append(String.Format(" {0}=\"{1}\"", atributeKey, Attributes[atributeKey]));
                    }

                    buffer.AppendLine(">");
                } 
            }
            else
            {
                buffer.AppendLine(String.Format("<{0}>", this.Tag));
            }

            foreach (var contentElement in this.Content)
            {
                buffer.Append(contentElement.EmitHtml());
            }

            buffer.Append(indentation);
            buffer.AppendLine(String.Format("</{0}>", this.Tag));

            return buffer.ToString();
        }
    }
}