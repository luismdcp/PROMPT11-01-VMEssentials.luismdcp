using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews.TypeViews
{
    public class TypeView : BaseResponseView
    {
        public Type TypeData { get; set; }
        public string Ctx { get; set; }
        public string Ns { get; set; }
        public string ShortName { get; set; }
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        public TypeView(Type typeData, string ctx, string ns, string shortName, string contextRootPath, string domain, string port)
        {
            this.TypeData = typeData;
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
            IHtmlEmiter title = HtmlGen.Title(2, HtmlGen.Text("Tipo", 3));
            IHtmlEmiter head = HtmlGen.Head(1, title);

            var nomeAssemblyElement = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Nome da Assembly: ", 5)), HtmlGen.Text(this.TypeData.Assembly.GetName().Name, 5));
            buffer.Add(nomeAssemblyElement);
            var nomeCompletoAssemblyElement = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Nome Completo da Assembly: ", 5)), HtmlGen.Text(this.TypeData.Assembly.GetName().FullName, 5));
            buffer.Add(nomeCompletoAssemblyElement);

            string assemblyLink = String.Format("http://{0}:{1}/{2}/as/{3}", this.Domain, this.Port, this.Ctx, this.TypeData.Assembly.GetName().Name);
            string namespaceLink = String.Format("http://{0}:{1}/{2}/ns/{3}", this.Domain, this.Port, this.Ctx, this.TypeData.Namespace);

            IHtmlEmiter assemblyHyperLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", assemblyLink } }, 4, HtmlGen.Text(this.TypeData.Assembly.GetName().Name, 5));
            IHtmlEmiter assemblyLinkElement = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Assembly link: ", 5)), assemblyHyperLink);
            buffer.Add(assemblyLinkElement);

            IHtmlEmiter namespaceHyperLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", namespaceLink } }, 4, HtmlGen.Text(this.TypeData.Namespace, 5));
            IHtmlEmiter namespaceLinkElement = HtmlGen.Li(null, 3, HtmlGen.B(4, HtmlGen.Text("Namespace link: ", 5)), namespaceHyperLink);
            buffer.Add(namespaceLinkElement);
            buffer.Add(HtmlGen.Br(3));
            buffer.Add(HtmlGen.Br(3));

            // Construção do bloco dos métodos
            List<IHtmlEmiter> linksBuffer = new List<IHtmlEmiter>();
            linksBuffer.Add(HtmlGen.Legend(null, 4, HtmlGen.Text("Métodos", 5)));

            string methodLinkString;
            IHtmlEmiter methodLink = null;

            foreach (var methodInfo in this.TypeData.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).OrderBy(m => m.Name))
            {
                methodLinkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}/m/{5}", this.Domain, this.Port, this.Ctx, this.TypeData.Namespace, this.TypeData.Name, methodInfo.Name);
                methodLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", methodLinkString } }, 4, HtmlGen.Text(methodInfo.Name, 5));
                linksBuffer.Add(methodLink);
                linksBuffer.Add(HtmlGen.Br(4));
            }

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            linksBuffer.Clear();

            // Construção do bloco dos construtores
            linksBuffer.Add(HtmlGen.Legend(null, 4, HtmlGen.Text("Construtores", 5)));

            string constructorLinkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}/c", this.Domain, this.Port, this.Ctx, this.TypeData.Namespace, this.TypeData.Name);
            IHtmlEmiter constructorLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", constructorLinkString } }, 4, HtmlGen.Text("Construtores", 5));
            linksBuffer.Add(constructorLink);
            linksBuffer.Add(HtmlGen.Br(4));

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            linksBuffer.Clear();

            // Construção do bloco dos campos
            linksBuffer.Add(HtmlGen.Legend(null, 4, HtmlGen.Text("Campos", 5)));

            string fieldLinkString;
            IHtmlEmiter fieldLink = null;

            foreach (var fieldInfo in this.TypeData.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).OrderBy(f => f.Name))
            {
                fieldLinkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}/f/{5}", this.Domain, this.Port, this.Ctx, this.TypeData.Namespace, this.TypeData.Name, fieldInfo.Name);
                fieldLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", fieldLinkString } }, 4, HtmlGen.Text(fieldInfo.Name, 5));
                linksBuffer.Add(fieldLink);
                linksBuffer.Add(HtmlGen.Br(4));
            }

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            linksBuffer.Clear();

            // Construção do bloco das propriedades
            linksBuffer.Add(HtmlGen.Legend(null, 4, HtmlGen.Text("Propriedades", 5)));

            string propLinkString;
            IHtmlEmiter propLink = null;

            foreach (var propInfo in this.TypeData.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).OrderBy(p => p.Name))
            {
                propLinkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}/p/{5}", this.Domain, this.Port, this.Ctx, this.TypeData.Namespace, this.TypeData.Name, propInfo.Name);
                propLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", propLinkString } }, 4, HtmlGen.Text(propInfo.Name, 5));
                linksBuffer.Add(propLink);
                linksBuffer.Add(HtmlGen.Br(4));
            }

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            linksBuffer.Clear();

            // Construção do bloco das propriedades
            linksBuffer.Add(HtmlGen.Legend(null, 4, HtmlGen.Text("Eventos", 5)));

            string eventLinkString;
            IHtmlEmiter eventLink = null;

            foreach (var eventInfo in this.TypeData.GetEvents(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).OrderBy(e => e.Name))
            {
                eventLinkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}/e/{5}", this.Domain, this.Port, this.Ctx, this.TypeData.Namespace, this.TypeData.Name, eventInfo.Name);
                eventLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", eventLinkString } }, 4, HtmlGen.Text(eventInfo.Name, 5));
                linksBuffer.Add(eventLink);
                linksBuffer.Add(HtmlGen.Br(4));
            }

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            linksBuffer.Clear();

            // Construção do bloco dos nested types
            linksBuffer.Add(HtmlGen.Legend(null, 4, HtmlGen.Text("Tipos Contidos", 5)));

            string typeLinkString;
            IHtmlEmiter typeLink = null;

            foreach (var type in this.TypeData.GetNestedTypes(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).OrderBy(t => t.Name))
            {
                typeLinkString = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}", this.Domain, this.Port, this.Ctx, this.TypeData.Namespace, type.Name);
                typeLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", typeLinkString } }, 4, HtmlGen.Text(type.Name, 5));
                linksBuffer.Add(typeLink);
                linksBuffer.Add(HtmlGen.Br(4));
            }

            buffer.Add(HtmlGen.Fieldset(null, 3, linksBuffer.ToArray()));
            linksBuffer.Clear();

            IHtmlEmiter body = HtmlGen.Body(null, 1, buffer.ToArray());
            IHtmlEmiter page = HtmlGen.Page(head, body, 0);

            return page.EmitHtml();
        }
    }
}