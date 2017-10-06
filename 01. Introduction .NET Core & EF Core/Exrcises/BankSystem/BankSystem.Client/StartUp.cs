using System.ComponentModel.DataAnnotations;
using BankSystem.Client.Core;

namespace Client
{
    class StartUp
    {
        static void Main()
        {
            var engine = new Engine();
            engine.Run();
        }
    }
}
