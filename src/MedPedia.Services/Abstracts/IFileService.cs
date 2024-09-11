using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Image;
using Microsoft.AspNetCore.Http;

namespace MedPedia.Services.Abstracts;

public interface IFileService
{
    Task<DataResult<ImageDto>> DownloadFile(string imageId, CancellationToken cancellationToken);
    Task<DataResult<UploadDto>> UploadFile(IFormFile file, CancellationToken cancellationToken);
}