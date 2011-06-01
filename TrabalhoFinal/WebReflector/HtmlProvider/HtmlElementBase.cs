using System;
using System.Text;
using WebReflectorContracts;

namespace HtmlProvider
{
    public abstract class HtmlElementBase : IHtmlEmiter
    {
        public abstract string EmitHtml();

        protected static string BuildIndentation(int indentationLevel)
        {
            if (indentationLevel == 0)
            {
                return String.Empty;
            }

            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < indentationLevel; i++)
            {
                buffer.Append("  ");
            }

            return buffer.ToString();
        }
    }
}