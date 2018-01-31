namespace HandmadeHTTPServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;
    using System.Collections.Generic;

    public class CalculatorController : Controller
    {
        private string result;

        public IHttpResponse Calculator()
        {
            return this.FileViewResponse("calculator", new Dictionary<string, string>
            {
                ["showResult"] = "none"
            });
        }

        public IHttpResponse Calculator(IHttpRequest request)
        {
            
            if (request.FormData.Count == 3)
            {
                var firstNumber = double.Parse(request.FormData["firstNumber"]);
                var secondNumber = double.Parse(request.FormData["secondNumber"]);
                var sign = request.FormData["sign"];

                this.result = Calculate(firstNumber, secondNumber, sign);
            }

            return this.FileViewResponse("calculator", new Dictionary<string, string>
            {
                ["showResult"] = "block",
                ["result"] = result
            });
        }

        private static string Calculate(double firstNumber, double secondNumber, string sign)
        {
            string result;

            switch (sign)
            {
                case "*":
                    result = (firstNumber * secondNumber).ToString();
                    break;
                case "/":
                    result = (firstNumber / secondNumber).ToString();
                    break;
                case "-":
                    result = (firstNumber - secondNumber).ToString();
                    break;
                case "+":
                    result = (firstNumber + secondNumber).ToString();
                    break;
                default:
                    result = "Invalid Sign";
                    break;
            }

            return result;
        }
    }
}
