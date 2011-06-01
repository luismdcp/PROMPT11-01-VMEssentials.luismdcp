using System;
using System.Collections.Generic;
using System.Linq;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class ContextAssembliesView : BaseResponseView
    {
        public IEnumerable<string> AssemblyNames { get; set; }
        public string Ctx { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public ContextAssembliesView(IEnumerable<string> assemblyNames, string ctx, string domain, string port)
        {
            this.AssemblyNames = assemblyNames.OrderBy(a => a.ToLower());
            this.Ctx = ctx;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();

            foreach (var assembly in this.AssemblyNames)
            {
                string link = String.Format("http://{0}:{1}/{2}/as/{3}", this.Domain, this.Port, this.Ctx, assembly);

                IHtmlEmiter hyperLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", link } }, 4, HtmlGen.Text(assembly, 5));
                IHtmlEmiter linkElement = HtmlGen.Li(null, 3, hyperLink);
                buffer.Add(linkElement);
            }

            return this.BuildPage("Assemblies", buffer.ToArray()).EmitHtml();
        }
    }
}