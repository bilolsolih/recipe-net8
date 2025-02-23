using RecipeBackend.Core.Exceptions;
using RecipeBackend.Features.Recipes;

namespace RecipeBackend.Core;

public class ServiceBase(string folderName, IWebHostEnvironment webEnv, IHttpContextAccessor httpContextAccessor)
{
    private string UploadsBaseAbsolutePath { get; set; } = webEnv.GetUploadBasePath();
    private string FolderName { get; set; } = folderName;
    protected string BaseUrl { get; set; } = httpContextAccessor.HttpContext!.GetUploadsBaseUrl();
    protected HttpContext HttpContext = httpContextAccessor.HttpContext!;


    protected async Task<string> SaveUploadsFileAsync(IFormFile file)
    {
        var fileExtension = GetFileExtension(file);
        var lastPeriodIndex = file.FileName.LastIndexOf('.');
        var fileName = file.FileName[..lastPeriodIndex] + GenerateShortGuid();
        fileName += fileExtension;

        var filePath = Path.Combine(UploadsBaseAbsolutePath, FolderName, fileName);

        if (!Directory.Exists(UploadsBaseAbsolutePath))
        {
            Directory.CreateDirectory(UploadsBaseAbsolutePath);
        }

        if (!Directory.Exists(Path.Combine(UploadsBaseAbsolutePath, FolderName)))
        {
            Directory.CreateDirectory(Path.Combine(UploadsBaseAbsolutePath, FolderName));
        }

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        var relativeFilePath = FolderName + '/' + fileName;

        return relativeFilePath;
    }

    protected bool DeleteUploadsFile(string filename)
    {
        if (!CheckUploadsFileExists(filename))
        {
            return false;
        }

        var absoluteFilePath = Path.Combine(UploadsBaseAbsolutePath, filename);
        File.Delete(absoluteFilePath);
        return true;
    }

    protected bool CheckUploadsFileExists(string filename)
    {
        var absoluteFilePath = Path.Combine(UploadsBaseAbsolutePath, filename);

        return File.Exists(absoluteFilePath);
    }

    protected string GetFileExtension(IFormFile file)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        InvalidFileException.ThrowIfNull(fileExtension, $"No file extension: {file.FileName}");

        return fileExtension;
    }

    protected string GenerateShortGuid(int length = 8)
    {
        var guid = Guid.NewGuid().ToString("N");
        return guid[..length];
    }
}