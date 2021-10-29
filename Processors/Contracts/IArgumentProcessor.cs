using System;
using System.Linq;
using System.Threading.Tasks;

namespace appt.Processors.Contracts
{
    public interface IArgumentProcessor
    {
        Task Execute(string[] args);

        Boolean Applies(string[] args);
    }
}