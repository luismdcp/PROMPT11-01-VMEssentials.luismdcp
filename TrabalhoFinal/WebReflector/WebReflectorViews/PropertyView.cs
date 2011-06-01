using System;
using System.Collections.Generic;
using System.Reflection;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class PropertyView : BaseResponseView
    {
        public PropertyInfo PropertyData { get; set; }
        public string Ctx { get; set; }
        public string Ns { get; set; }
        public string ShortName { get; set; }
        public string PropName { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public PropertyView(PropertyInfo propertyData, string ctx, string ns, string shortName, string propName, string contextRootPath, string domain, string port)
        {
            this.PropertyData = propertyData;
            this.Ctx = ctx;
            this.Ns = ns;
            this.ShortName = shortName;
            this.PropName = propName;
            this.ContextRootPath = contextRootPath;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            IHtmlEmiter title = HtmlGen.Title(2, HtmlGen.Text("Propriedade", 3));
            IHtmlEmiter head = HtmlGen.Head(1, title);
            IHtmlEmiter liName = HtmlGen.Li(null, 3,
                                                    HtmlGen.B(4,
                                                                HtmlGen.Text("Nome: ", 5)),
                                                                HtmlGen.Text(this.PropertyData.Name, 5)
                                           );

            string fieldTypeData = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}", this.Domain, this.Port, this.Ctx, this.PropertyData.PropertyType.Namespace, this.PropertyData.PropertyType.Name);

            IHtmlEmiter typeHyperlink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", fieldTypeData } }, 4, HtmlGen.Text(this.PropertyData.PropertyType.Name, 5));
            IHtmlEmiter liTypeHyperlink = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Tipo: ", 5)), typeHyperlink);
            IHtmlEmiter dataList = HtmlGen.Ul(null, 2, liName, liTypeHyperlink);

            var propertyParameters = this.PropertyData.GetIndexParameters();
            IHtmlEmiter parametersHtmlTitle = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Parâmetros: ", 5)));

            IHtmlEmiter body = propertyParameters.Length > 0 ? 
                HtmlGen.Body(null, 1, dataList, parametersHtmlTitle, this.BuildParametersView(propertyParameters, this.Domain, this.Port, this.Ctx, 5)) : 
                HtmlGen.Body(null, 1, dataList);

            return HtmlGen.Page(head, body, 0).EmitHtml();
        }
    }
}