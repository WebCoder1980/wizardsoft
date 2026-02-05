using wizardsoft_testtask.Dtos;
using wizardsoft_testtask.Models;

namespace wizardsoft_testtask.Service
{
    public interface ITreeService
    {
        Task<IReadOnlyCollection<TreeNodeResponse>> ExportAsync(CancellationToken cancellationToken);
        Task<IReadOnlyCollection<TreeNodeResponse>> GetRootsAsync(CancellationToken cancellationToken);
    }
}
