using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tacka_CSGO_Radar_Cheat.Server
{
    class HTTPServer
    {
        private static HttpListener listener;
        private static readonly string url = "http://localhost:";

        public static readonly string publicDir = "Public";
        public static readonly string viewsDir = "views";
        public static readonly string pgIndex = "index.html";

        public int Port { get; set; }

        public void RunLocalHTTPServer()
        {
            Console.WriteLine("Attemting to open server...");
            
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add(url + Port.ToString() + "/");
                listener.Start();
                Console.WriteLine($"[SUCCESS] Server listening locally on port: {Port.ToString()}");

                Task listenTask = ConnectionHandler();
            }
            catch
            {
                Console.WriteLine($"[ERROR] Something went wrong when opening server on port: {Port}");
            }
        }

        private static async Task ConnectionHandler()
        {
            for (; ; )
            {
                HttpListenerContext ctx = await listener.GetContextAsync();

                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/"))
                {
                    byte[] data = Encoding.UTF8.GetBytes(GetHTMLPage(pgIndex));
                    resp.ContentType = "text/html";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;

                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    resp.Close();
                }

                if ((req.HttpMethod == "GET") && (req.Url.AbsolutePath == "/Entity-Data"))
                {
                    byte[] data = Encoding.UTF8.GetBytes("TEST");
                    resp.ContentType = "text/plain";
                    resp.ContentEncoding = Encoding.UTF8;
                    resp.ContentLength64 = data.LongLength;

                    await resp.OutputStream.WriteAsync(data, 0, data.Length);
                    resp.Close();
                }
            }
        }

        private static string GetHTMLPage(string pgName)
        {
            string absolutePath = Path.Combine(Environment.CurrentDirectory, publicDir, pgName);

            if (Program.devMode == true || !File.Exists(absolutePath))
            {
                absolutePath = Path.Combine(Path.GetFullPath(@"../../"), publicDir, viewsDir, pgName);

            }

            string HTMLPageString = File.ReadAllText(absolutePath);
            return HTMLPageString;
        }
    }
}