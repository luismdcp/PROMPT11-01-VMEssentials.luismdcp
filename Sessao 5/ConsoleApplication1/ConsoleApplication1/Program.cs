using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ConsoleApplication1
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