using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appt.Processors.Contracts;

namespace appt
{
    public class App : IApp
    {
        private readonly IEnumerable<IArgumentProcessor> _argumentProcessors;

        public App(IEnumerable<IArgumentProcessor> argumentProcessors)
        {
            _argumentProcessors = argumentProcessors;
        }

        public async Task RunAsync(string[] args)
        {
            if (!args.Any())
            {
                throw new ArgumentException("No argument provided.");
            }

            var processors = _argumentProcessors.Where(ao => ao.Applies(args));

            if (!processors.Any())
            {
                throw new ArgumentException($"Invalid argument(s).");
            }

            foreach (var processor in processors)
            {
                await processor.Execute(args);
            }
        }
    }
}