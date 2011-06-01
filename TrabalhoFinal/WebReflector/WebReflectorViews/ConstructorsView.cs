using System.Collections.Generic;
using System.Reflection;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class ConstructorsView : BaseResponseView
    {
        public IEnumerable<ConstructorInfo> ConstructorsData { get; set; }
        public string Ctx { get; set; }
        public string Ns { get; set; }
        public string ShortName { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public ConstructorsView(IEnumerable<ConstructorInfo> constructorsData, string ctx, string ns, string shortName, string contextRootPath, string domain, string port)
        {
            this.ConstructorsData = constructorsData;
            this.Ctx = ctx;
            this.Ns = ns;
            this.ShortName = shortName;
            this.ContextRootPath = contextRootPath;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();

            foreach (var constructorInfo in this.ConstructorsData)
            {
                if (constructorInfo.GetParameters().Length > 0)
                {
                    var parametersInfo = this.BuildParametersView(constructorInfo.GetParameters(), this.Domain, this.Port, this.Ctx, 2);
                    buffer.Add(parametersInfo);
                    buffer.Add(HtmlGen.Hr(2));   
                }
            }

            return this.BuildPage("Construtores", buffer.ToArray()).EmitHtml();
        }
    }
}