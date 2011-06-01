using System;
using System.Collections.Generic;
using System.Reflection;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class FieldView : BaseResponseView
    {
        public FieldInfo FieldData { get; set; }
        public string Ctx { get; set; }
        public string Ns { get; set; }
        public string ShortName { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public FieldView(FieldInfo fieldData, string ctx, string ns, string shortName, string contextRootPath, string domain, string port)
        {
            this.FieldData = fieldData;
            this.Ctx = ctx;
            this.Ns = ns;
            this.ShortName = shortName;
            this.ContextRootPath = contextRootPath;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            IHtmlEmiter liName = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Nome: ", 5)), HtmlGen.Text(this.FieldData.Name, 5));

            string fieldTypeData = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}", this.Domain, this.Port, this.Ctx, this.FieldData.FieldType.Namespace, this.FieldData.FieldType.Name);

            IHtmlEmiter typeHyperlink = HtmlGen.Anchor(new Dictionary<string, string> {{"href", fieldTypeData}}, 4, HtmlGen.Text(this.FieldData.FieldType.Name, 5));
            IHtmlEmiter liTypeHyperlink = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Tipo: ", 5)), typeHyperlink);

            return this.BuildPage("Campo", HtmlGen.Ul(null, 2, liName, liTypeHyperlink)).EmitHtml();
        }
    }
}