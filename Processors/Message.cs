using System;
using System.Linq;
using System.Threading.Tasks;
using appt.Processors.Contracts;
using appt.Storage;
using appt.Models;

namespace appt.Processors
{
    public class Message : IArgumentProcessor
    {
        private const string Template = "[{0}]: {1}{2}";
        private const string ArgSelector = "-m";
        private readonly IApplicationContext _applicationContext;

        public Message(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Applies(string[] args)
        {
            int controlIndex = FindIndex(args);

            if (controlIndex == -1) return false;

            return !args[controlIndex + 1].StartsWith("-");
        }

        public async Task Execute(string[] args)
        {
            var costCenter = (await _applicationContext.GetConfigurationAsync()).CostCenter;

            var token = Token.Create(costCenter, args[FindIndex(args) + 1]);

            await _applicationContext.AddToken(token);
        }

        public static int FindIndex(string[] args)
        {
            return Array.FindIndex(args, 0, args.Count(), (v) => v == ArgSelector);
        }
    }
}
