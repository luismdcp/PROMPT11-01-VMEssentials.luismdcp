using System;
using System.Collections.Generic;
using System.Linq;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class NamespaceView : BaseResponseView
    {
        public IEnumerable<string> SubNamespaces { get; set; }
        public IEnumerable<string> ShortNames { get; set; }
        public string Ctx { get; set; }
        public string NamespacePrefix { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public NamespaceView(IEnumerable<string> subNamespaces, IEnumerable<string> shortNames, string ctx, string namespacePrefix, string contextRootPath, string domain, string port)
        {
            this.SubNamespaces = subNamespaces.OrderBy(sb => sb.ToLower());
            this.ShortNames = shortNames.OrderBy(sn => sn.ToLower());
            this.Ctx = ctx;
            this.NamespacePrefix = namespacePrefix;
            this.ContextRootPath = contextRootPath;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            string linkString;
            IHtmlEmiter link;
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();
            IHtmlEmiter title = HtmlGen.Title(2, HtmlGen.Text("Namespace", 3));
            IHtmlEmiter head = HtmlGen.Head(1, title);

            // Construção do bloco dos sub-namespaces
            List<IHtmlEmiter> linksBuffer = new List<IHtmlEmiter> { HtmlGen.Legend(null, 3, HtmlGen.Text("Sub-Namespaces", 4)) };

            foreach (var subNamespace in this.SubNamespaces)
            {
                linkString = String.Format("http://{0}:{1}/{2}/ns/{3}", this.Domain, this.Port, this.Ctx, subNamespace);
                link = HtmlGen.Anchor(new Dictionary<string, string> { { "href", linkString } }, 3, HtmlGen.Text(subNamespace, 4));
                linksBuffer.Add(link);
                linksBuffer.Add(HtmlGen.Br(4));
            }

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            linksBuffer.Clear();

            // Construção do bloco dos tipos definidos no namespace
            linksBuffer.Add(HtmlGen.Legend(null, 3, HtmlGen.Text("Tipos", 4)));

            foreach (var shortName in this.ShortNames)
            {
                linkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}", this.Domain, this.Port, this.Ctx, this.NamespacePrefix, shortName);
                link = HtmlGen.Anchor(new Dictionary<string, string> { { "href", linkString } }, 3, HtmlGen.Text(shortName, 4));
                linksBuffer.Add(link);
                linksBuffer.Add(HtmlGen.Br(4));
            }

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            return HtmlGen.Page(head, HtmlGen.Body(null, 1, buffer.ToArray()), 0).EmitHtml();
        }
    }
}