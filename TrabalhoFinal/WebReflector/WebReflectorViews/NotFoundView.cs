using HtmlProvider;
using WebReflectorContracts;

namespace WebReflectorViews
{
    public class NotFoundView : BaseResponseView
    {
        public string Content { get; set; }

        public NotFoundView(string content)
        {
            this.Content = content;
        }

        public override string Emit()
        {
            IHtmlEmiter head = HtmlGen.Head(1, HtmlGen.Title(2, HtmlGen.Text("NotFound", 3)));
            IHtmlEmiter body = HtmlGen.Body(null, 1, HtmlGen.P(2, HtmlGen.Text(this.Content, 3)));
            return HtmlGen.Page(head, body, 0).EmitHtml();
        }
    }
}