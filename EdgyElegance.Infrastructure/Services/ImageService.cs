using EdgyElegance.Application.Constants;
using EdgyElegance.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace EdgyElegance.Infrastructure.Services;

public class ImageService : IImageService {
    private readonly string _imageDirectory;
    private readonly string _thumbsDirectory;

    public ImageService() {
        if (string.IsNullOrEmpty(ApplicationConstants.IMAGE_UPLOAD_FOLDER))
            throw new Exception("Upload directory is not defined");

        _imageDirectory = Path.Combine(ApplicationConstants.IMAGE_UPLOAD_FOLDER, "originals");
        _thumbsDirectory = Path.Combine(ApplicationConstants.IMAGE_UPLOAD_FOLDER, "thumbnails");

        // Creates directories if they don't exist
        if (!Directory.Exists(_imageDirectory))
            Directory.CreateDirectory(_imageDirectory);

        if (!Directory.Exists(_thumbsDirectory))
            Directory.CreateDirectory(_thumbsDirectory);
    }

    public string CreateThumbnail(IFormFile file) {
        // Copies the file to a new image stream
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);

        // Generates a new thumbnail from the image
        Image image = Bitmap.FromStream(memoryStream);
        Bitmap thumbnail = CreateThumbnailFromImage(image);

        // Save the thumbnail to the File System and returns is path
        string filename = Path.Combine(_thumbsDirectory, $"{Guid.NewGuid().ToString()}.png");
        thumbnail.Save(filename: filename);

        return filename;
    }

    public void DeleteImages(IEnumerable<BaseImage> images) {
        images.ToList().ForEach(f => {
            string? path = f.Path ?? string.Empty;
            
            if (!string.IsNullOrEmpty(path))
                if (File.Exists(path)) File.Delete(path);
        });
    }

    public string StoreFileImage(IFormFile file) {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);

        Image image = Bitmap.FromStream(memoryStream);

        string filename = Path.Combine(_imageDirectory, $"{Guid.NewGuid().ToString()}.png");
        image.Save(filename: filename);

        return filename;
    }

    private Bitmap CreateThumbnailFromImage(Image original) {
        // Calculates the radio, so we can keep the aspect
        double ratioX = 180.00 / original.Width;
        double ratioY = 320.00 / original.Height;
        double ratio = Math.Min(ratioX, ratioY);

        int width = Convert.ToInt32(original.Width * ratio);
        int height = Convert.ToInt32(original.Height * ratio);

        var newBitmap = new Bitmap(width: width, height: height);

        using var graphics = Graphics.FromImage(newBitmap);
        graphics.DrawImage(original, 0, 0, width, height);

        return newBitmap;
    }
}
