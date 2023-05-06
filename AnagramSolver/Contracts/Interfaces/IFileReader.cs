using Contracts.Models;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Contracts.Interfaces
{
    public interface IFileReader
    {
        IList<string> ReadFile();
    }
}
