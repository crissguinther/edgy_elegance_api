using EdgyElegance.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EdgyElegance.Infrastructure.Services;

public interface IImageService {
    /// <summary>
    /// Creates a thumbnail for a given <see cref="IFormFile"/>
    /// </summary>
    /// <param name="file">The <see cref="IFormFile"/> to be used</param>
    /// <returns>The path where the image was stored</returns>
    string CreateThumbnail(IFormFile file);

    /// <summary>
    /// Stores an Image from a <see cref="IFormFile"/>
    /// </summary>
    /// <param name="file">The <see cref="IFormFile"/> to have its Image stored</param>
    /// <returns>The path where the image was stored</returns>
    string StoreFileImage(IFormFile file);

    /// <summary>
    /// Deletes images according to their filenames
    /// </summary>
    /// <param name="filenames">A <see cref="List{String}"/> with the filenames of the images</param>
    void DeleteImages(IEnumerable<BaseImage> image);
}