using System;
using System.Threading.Tasks;

namespace RazorLitePoc
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await new Services.Test().GetTemplateByResource();
        }
    }
}