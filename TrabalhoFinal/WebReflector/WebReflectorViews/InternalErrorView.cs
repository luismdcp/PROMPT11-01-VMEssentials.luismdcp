using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class InternalErrorView : BaseResponseView
    {
        public string Content { get; set; }

        public InternalErrorView(string content)
        {
            this.Content = content;
        }

        public override string Emit()
        {
            IHtmlEmiter head = HtmlGen.Head(1, HtmlGen.Title(2, HtmlGen.Text("Erro", 3)));
            IHtmlEmiter body = HtmlGen.Body(null, 1, HtmlGen.P(2, HtmlGen.Text(this.Content, 3)));
            return HtmlGen.Page(head, body, 0).EmitHtml();
        }
    }
}