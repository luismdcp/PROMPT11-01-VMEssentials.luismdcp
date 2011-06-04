using System;
using System.Collections.Generic;
using System.Linq;
using HtmlProvider;
using WebReflectorContracts;
using System.Web;

namespace WebReflectorViews.Assembly
{
    public class AssemblyView : BaseResponseView
    {
        public string FriendlyName { get; set; }
        public string PublicKey { get; set; }
        public IEnumerable<IGrouping<string, Type>> TypesGroupedByNamespace { get; set; }
        public string Ctx { get; set; }
        public string AssemblyName { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public AssemblyView(string friendlyName, string publicKey, IEnumerable<IGrouping<string, Type>> typesGroupedByNamespace, string ctx, string assemblyName, string contextRootPath, string domain, string port)
        {
            this.FriendlyName = friendlyName;
            this.PublicKey = publicKey;
            this.TypesGroupedByNamespace = typesGroupedByNamespace.OrderBy(g => g.Key);
            this.Ctx = ctx;
            this.AssemblyName = assemblyName;
            this.ContextRootPath = contextRootPath;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();
            List<IHtmlEmiter> linksBuffer = new List<IHtmlEmiter>();

            var nomeAssemblyElement = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Nome da Assembly: ", 5)), HtmlGen.Text(this.FriendlyName, 5));
            buffer.Add(nomeAssemblyElement);

            var publicKeyAssemblyElement = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Public key da Assembly: ", 5)), HtmlGen.Text(this.PublicKey, 5));
            buffer.Add(publicKeyAssemblyElement);
            buffer.Add(HtmlGen.Br(3));
            buffer.Add(HtmlGen.Br(3));

            foreach (var group in this.TypesGroupedByNamespace)
            {
                linksBuffer.Add(HtmlGen.Legend(null, 4, HtmlGen.Text(group.Key, 5)));

                string typeLinkString;
                IHtmlEmiter typeLink;

                foreach (var type in group)
                {
                    typeLinkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}", this.Domain, this.Port, this.Ctx, type.Namespace, type.Name);
                    typeLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", typeLinkString } }, 4, HtmlGen.Text(type.Name, 5));
                    linksBuffer.Add(typeLink);
                    linksBuffer.Add(HtmlGen.Br(4));
                }

                buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
                linksBuffer.Clear();
                buffer.Add(HtmlGen.Br(3));
                buffer.Add(HtmlGen.Br(3));
            }

            return this.BuildPage("Assembly", buffer.ToArray()).EmitHtml();
        }
    }
}