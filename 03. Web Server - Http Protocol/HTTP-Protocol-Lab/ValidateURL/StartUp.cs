namespace ValidateURL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System;

    class StartUp
    {
        public static void Main()
        {
            var url = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(url);
            try
            {
                var parsedUrl = new Uri(decodedUrl);

                var protocol = parsedUrl.Scheme;
                var host = parsedUrl.Host;
                var port = parsedUrl.Port;
                var path = parsedUrl.AbsolutePath;
                var query = parsedUrl.Query;
                var fragment = parsedUrl.Fragment;

                var requiredParts = new List<string> { protocol, host };
                if (requiredParts.Any(string.IsNullOrEmpty))
                {
                    Console.WriteLine("Invalid URL");
                }

                else if (protocol == "http" && port != 80)
                {
                    Console.WriteLine("Invalid URL");
                }

                else if (protocol == "https" && port != 443)
                {
                    Console.WriteLine("Invalid URL");
                }

                else
                {
                    Console.WriteLine($"Protocol: {protocol}");
                    Console.WriteLine($"Host: {host}");
                    Console.WriteLine($"Port: {port}");
                    Console.WriteLine($"Path: {path}");
                    Console.WriteLine($"Query: {query}");
                    Console.WriteLine($"Fragment: {fragment}");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Invalid URL");
            }
        }
    }
}
