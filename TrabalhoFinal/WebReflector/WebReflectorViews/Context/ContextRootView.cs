using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews.Context
{
    public class ContextRootView : BaseResponseView
    {
        public IEnumerable<string> ContextNames { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public ContextRootView(IEnumerable<string> contextNames, string domain, string port)
        {
            this.ContextNames = contextNames.OrderBy(c => c.ToLower());
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();

            foreach (var contextName in this.ContextNames)
            {
                string contextLink = String.Format("http://{0}:{1}/{2}", this.Domain, this.Port, Path.GetFileName(contextName));

                IHtmlEmiter contextHyperLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", contextLink } }, 4, HtmlGen.Text(Path.GetFileName(contextName), 5));
                IHtmlEmiter contextLinkElement = HtmlGen.Li(null, 3, contextHyperLink);
                buffer.Add(contextLinkElement);
            }

            return this.BuildPage("Raíz", buffer.ToArray()).EmitHtml();
        }
    }
}