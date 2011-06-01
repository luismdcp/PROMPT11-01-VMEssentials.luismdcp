using System.IO;
using System.Net;

namespace WebReflector
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpListener listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost:8080/");
                listener.Start();

                while (true)
                {
                    HttpListenerContext ctx = listener.GetContext();
                    ctx.Response.StatusCode = 200;

                    StreamWriter writer = new StreamWriter(ctx.Response.OutputStream);
                    writer.WriteLine("<P>Hello, {0}</P>", ctx.Request.Url);
                    writer.Close();
                    ctx.Response.Close();
                }
            }
        }
    }
}