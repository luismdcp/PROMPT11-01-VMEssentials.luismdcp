using System;
using System.Collections.Generic;
using System.Linq;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class ContextNamespaces : BaseResponseView
    {
        public IEnumerable<string> Namespaces { get; set; }
        public string Ctx { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public ContextNamespaces(IEnumerable<string> namespaces, string ctx, string domain, string port)
        {
            this.Namespaces = namespaces.OrderBy(n => n.ToLower());
            this.Ctx = ctx;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();

            foreach (var ns in this.Namespaces)
            {
                string link = String.Format("http://{0}:{1}/{2}/ns/{3}", this.Domain, this.Port, this.Ctx, ns);

                IHtmlEmiter hyperLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", link } }, 4, HtmlGen.Text(ns, 5));
                IHtmlEmiter linkElement = HtmlGen.Li(null, 3, hyperLink);
                buffer.Add(linkElement);
            }

            return this.BuildPage("Namespaces", buffer.ToArray()).EmitHtml();
        }
    }
}