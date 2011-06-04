using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews.TypeViews
{
    public class MethodView : BaseResponseView
    {
        public IEnumerable<MethodInfo> MethodsData { get; set; }
        public string Ctx { get; set; }
        public string Ns { get; set; }
        public string ShortName { get; set; }
        public string MethodName { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public MethodView(IEnumerable<MethodInfo> methodsData, string ctx, string ns, string shortName, string methodName, string contextRootPath, string domain, string port)
        {
            this.MethodsData = methodsData.OrderBy(m => m.Name);
            this.Ctx = ctx;
            this.Ns = ns;
            this.ShortName = shortName;
            this.MethodName = methodName;
            this.ContextRootPath = contextRootPath;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();
            IHtmlEmiter title = HtmlGen.Title(2, HtmlGen.Text("Método", 3));
            IHtmlEmiter head = HtmlGen.Head(1, title);

            foreach (var methodInfo in this.MethodsData)
            {
                if (methodInfo.GetParameters().Length > 0)
                {
                    buffer.Add(HtmlGen.B(2, HtmlGen.Text(methodInfo.Name, 3)));
                    buffer.Add(this.BuildParametersView(methodInfo.GetParameters(), this.Domain, this.Port, this.Ctx, 2));
                    buffer.Add(HtmlGen.Hr(2));
                }
            }

            return HtmlGen.Page(head, HtmlGen.Body(null, 1, buffer.ToArray()), 0).EmitHtml();
        }
    }
}