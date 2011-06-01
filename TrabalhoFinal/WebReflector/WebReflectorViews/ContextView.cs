using System;
using System.Collections.Generic;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class ContextView : BaseResponseView
    {
        public string Ctx { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public ContextView(string ctx, string domain, string port)
        {
            this.Ctx = ctx;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();
            string link = String.Format("http://{0}:{1}/{2}/as", this.Domain, this.Port, this.Ctx);

            IHtmlEmiter hyperLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", link } }, 4, HtmlGen.Text("Assemblies", 5));
            IHtmlEmiter linkElement = HtmlGen.Li(null, 3, hyperLink);
            buffer.Add(linkElement);

            link = String.Format("http://{0}:{1}/{2}/ns", this.Domain, this.Port, this.Ctx);

            hyperLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", link } }, 4, HtmlGen.Text("Namespaces", 5));
            linkElement = HtmlGen.Li(null, 3, hyperLink);
            buffer.Add(linkElement);

            return this.BuildPage("Contexto", buffer.ToArray()).EmitHtml();
        }
    }
}