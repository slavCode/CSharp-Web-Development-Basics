using System;
using WebServer.Server.Contracts;

namespace WebServer.Server.Common
{
    public class InternalServerErrorView: IView
    {
        private readonly Exception exception;

        public InternalServerErrorView(Exception ex)
        {
            this.exception = ex;
        }

        public string View()
        {
            return $"<h1>{this.exception.Message}</h1><h2>{this.exception.StackTrace}</h2>";
        }
    }
}
