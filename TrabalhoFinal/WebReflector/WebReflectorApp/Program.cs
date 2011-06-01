using System;
using System.Configuration;
using System.Net;
using System.Text;
using Handlers;
using Router;
using WebReflectorContracts;

namespace WebReflectorApp
{
    class Program
    {
        static void Main()
        {
            string domain = ConfigurationManager.AppSettings["Domain"].ToString();
            string port = ConfigurationManager.AppSettings["Port"].ToString();
            string contextRootPath = ConfigurationManager.AppSettings["ContextRootPath"].ToString();

            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add(String.Format("http://{0}:{1}/", domain, port)); 
                listener.Start();

                var provider = new HandlerProvider {ContextRootPath = contextRootPath, Domain = domain, Port = port};
                RoutesManager router = new RoutesManager(provider);
                IResponseView errorView = null;
                bool registrySuccess = router.RegisterHandlerProvider(out errorView);

                if (!registrySuccess)
                {
                    HttpListenerContext ctx = listener.GetContext();
                    byte[] errorViewEncodedBytes = Encoding.Default.GetBytes(errorView.Emit());

                    ctx.Response.StatusCode = 501;
                    ctx.Response.ContentType = "text/html";
                    ctx.Response.ContentEncoding = Encoding.Default;
                    ctx.Response.ContentLength64 = errorViewEncodedBytes.Length;
                    ctx.Response.OutputStream.Write(errorViewEncodedBytes, 0, errorViewEncodedBytes.Length);
                    ctx.Response.Close();
                }
                else
                {
                    while (true)
                    {
                        HttpListenerContext ctx = listener.GetContext();
                        IResponseView resultView = router.Execute(ctx.Request.Url.AbsolutePath);
                        byte[] viewEncodedBytes = Encoding.Default.GetBytes(resultView.Emit());

                        ctx.Response.StatusCode = 200;
                        ctx.Response.ContentType = "text/html";
                        ctx.Response.ContentEncoding = Encoding.Default;
                        ctx.Response.ContentLength64 = viewEncodedBytes.Length;
                        ctx.Response.OutputStream.Write(viewEncodedBytes, 0, viewEncodedBytes.Length);
                        ctx.Response.Close();
                    }
                }
            }
        }
    }
}