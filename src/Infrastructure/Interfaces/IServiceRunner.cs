using System.Threading;
using System.Threading.Tasks;

namespace BibleUpload.Infrastructure.Interfaces
{
    public interface IServiceRunner
    {
        Task Run(CancellationToken cancellationToken = default);
    }
}