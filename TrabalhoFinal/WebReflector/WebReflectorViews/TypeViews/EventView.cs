using System;
using System.Reflection;
using WebReflectorContracts;
using HtmlProvider;
using System.Collections.Generic;

namespace WebReflectorViews.TypeViews
{
    public class EventView : BaseResponseView
    {
        public EventInfo EventData { get; set; }
        public string Ctx { get; set; }
        public string Ns { get; set; }
        public string ShortName { get; set; }
        public string EventName { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public EventView(EventInfo eventData, string ctx, string ns, string shortName, string eventName, string contextRootPath, string domain, string port)
        {
            this.EventData = eventData;
            this.Ctx = ctx;
            this.Ns = ns;
            this.ShortName = shortName;
            this.EventName = eventName;
            this.ContextRootPath = contextRootPath;
            this.Domain = domain;
            this.Port = port;
        }

        public override string Emit()
        {
            IHtmlEmiter title = HtmlGen.Title(2, HtmlGen.Text("Evento", 3));
            IHtmlEmiter liName = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Nome: ", 5)), HtmlGen.Text(this.EventData.Name, 5));

            string fieldTypeData = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}", this.Domain, this.Port, this.Ctx, this.EventData.EventHandlerType.Namespace, this.EventData.EventHandlerType.Name);

            IHtmlEmiter typeHyperlink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", fieldTypeData } }, 4, HtmlGen.Text(this.EventData.EventHandlerType.Name, 5));
            IHtmlEmiter liTypeHyperlink = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Tipo: ", 5)), typeHyperlink);

            return this.BuildPage("Evento", HtmlGen.Ul(null, 2, liName, liTypeHyperlink)).EmitHtml();
        }
    }
}