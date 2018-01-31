namespace RequestParser
{
    using System.Collections.Generic;
    using System;

    public class StartUp
    {
        public static void Main()
        {
            var validUrls = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var inputLine = Console.ReadLine();

                if (inputLine == "END") break;


                var parts = inputLine.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);


                var path = $"/{parts[0]}";
                var method = parts[1];

                if (!validUrls.ContainsKey(path))
                {
                    validUrls[path] = new HashSet<string>();
                }

                validUrls[path].Add(method);
            }

            var request = Console.ReadLine();
            var requestParts = request.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            var requestMethod = requestParts[0];
            var requestPath = requestParts[1];
            var responseProtocol = requestParts[2];

            var responseStatus = 404;
            var responseStatusText = "Not Found";

            if (validUrls.ContainsKey(requestPath)
             && validUrls[requestPath].Contains(requestMethod.ToLower()))
            {
                responseStatus = 200;
                responseStatusText = "OK";
            }

            Console.WriteLine($"{responseProtocol} {responseStatus} {responseStatusText}");
            Console.WriteLine($"Content-Length: {responseStatusText.Length}");
            Console.WriteLine($"Content-Type: text/plain{Environment.NewLine}");
            Console.WriteLine($"{responseStatusText}");
        }
    }
}
