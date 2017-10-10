namespace URLDecode
{
    using System;
    using System.Net;

    public class StartUp
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var webUtility = WebUtility.UrlDecode(input);

            Console.WriteLine(webUtility);
        }
    }
}
