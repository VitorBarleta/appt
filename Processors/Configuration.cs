using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appt.Processors.Contracts;
using appt.Storage;
using appt.Storage.Models;

namespace appt.Processors
{
    public class Configuration : IArgumentProcessor
    {
        private const string ArgSelector = "--set-costcenter";
        private readonly IApplicationContext _applicationContext;

        public Configuration(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Applies(string[] args)
        {
            var controlIndex = Array.FindIndex(args, 0, args.Count(), (v) => v == ArgSelector);

            if (controlIndex == -1) return false;

            return !args[controlIndex+1].StartsWith("-");
        }

        public static int FindIndex(string[] args)
        {
            return Array.FindIndex(args, 0, args.Count(), (v) => v == ArgSelector);
        }

        public async Task Execute(string[] args)
        {
            var argSelectorIndex = FindIndex(args);

            await _applicationContext.OverwriteConfiguration(new AppConfiguration(args[argSelectorIndex + 1]));
        }
    }
}