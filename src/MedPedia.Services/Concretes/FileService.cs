using MedPedia.Common.Abstracts;
using MedPedia.Common.Utilities.Enums;
using MedPedia.Common.Utilities.Results;
using MedPedia.Entities.Dto.Image;
using MedPedia.Services.Abstracts;
using Microsoft.AspNetCore.Http;

namespace MedPedia.Services.Concretes;

public class FileService : IFileService, IScopedDependency
{
    private const string Path = @"/home/temp";


    public async Task<DataResult<ImageDto>> DownloadFile(string imageId, CancellationToken cancellationToken)
    {
        var filePath = $"{Path}/{imageId}";
        if (!File.Exists(filePath))
        {
            return new ErrorDataResult<ImageDto>(Messages.NotFound);
        }

        var fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);
        var imageDto = new ImageDto
        {
            Image = fileBytes
        };
        return new SuccessDataResult<ImageDto>(imageDto);
    }

    public async Task<DataResult<UploadDto>> UploadFile(IFormFile file, CancellationToken cancellationToken)
    {
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }

        var guid = Guid.NewGuid().ToString().Replace("-", "");
        var extension = System.IO.Path.GetExtension(file.FileName);
        var fileName = $"{guid}{extension}";
        var filePath = System.IO.Path.Combine(Path, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        var uploadDto = new UploadDto()
        {
            ImageId = fileName
        };

        return new SuccessDataResult<UploadDto>(uploadDto);
    }
}