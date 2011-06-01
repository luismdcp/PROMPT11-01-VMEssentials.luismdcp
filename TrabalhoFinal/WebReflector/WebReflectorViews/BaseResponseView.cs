using System;
using System.Collections.Generic;
using System.Reflection;
using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public abstract class BaseResponseView : IResponseView
    {
        public abstract string Emit();

        public IHtmlEmiter BuildParametersView(ParameterInfo[] parameterInfos, string domain, string port, string ctx, int indentationLevel)
        {
            List<IHtmlEmiter> buffer = new List<IHtmlEmiter>();

            foreach (var parameterInfo in parameterInfos)
            {
                var nameElement = HtmlGen.Li(null, indentationLevel,
                                                    HtmlGen.B(indentationLevel + 1,
                                                                HtmlGen.Text("Nome: ", indentationLevel + 2)),
                                                                HtmlGen.Text(parameterInfo.Name, indentationLevel + 2)
                                           );

                buffer.Add(nameElement);

                string parameterTypeData = String.Format("http://{0}:{1}/{2}/ns/{3}/{4}", domain, port, ctx, parameterInfo.ParameterType.Namespace, parameterInfo.ParameterType.Name);
                var parameterTypeLink = HtmlGen.Anchor(new Dictionary<string, string> { { "href", parameterTypeData } }, indentationLevel + 1, HtmlGen.Text(parameterInfo.ParameterType.Name, indentationLevel + 2));
                IHtmlEmiter liTypeHyperlink = HtmlGen.Li(null, indentationLevel, HtmlGen.B(indentationLevel + 1, HtmlGen.Text("Tipo: ", indentationLevel + 2)), parameterTypeLink);

                buffer.Add(liTypeHyperlink);
            }

            return HtmlGen.Ul(null, indentationLevel, buffer.ToArray());
        }

        public IHtmlEmiter BuildPage(string title, params IHtmlEmiter[] content)
        {
            return HtmlGen.Page(HtmlGen.Head(1, HtmlGen.Title(2, HtmlGen.Text(title, 3))), HtmlGen.Body(null, 1, content), 0);
        }
    }
}