using FileServer.Model.Entities;
using FileServer.Model.Entities.DTO;
using FileServer.Model.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileServer.Api
{
    [ApiController]
    [Route("/api/File/Product/")]
    public class ProductImagesApi : ControllerBase
    {
        private readonly IProductPathsHelper _pathsHelper;
        private readonly IFileService _fileService;
        public ProductImagesApi(IFileService fileService, IProductPathsHelper pathsHelper) 
            => (_fileService, _pathsHelper) = (fileService, pathsHelper);

        [Authorize]
        [HttpPost("AddImages")]
        public async Task AddImages([FromForm]IFormFileCollection files, [FromForm]SetProductImagesDTO productImagesDTO)
        {
            AddingFile[] addingFiles = new AddingFile[files.Count];
            for (int i = 0; i < addingFiles.Length; i++)
                addingFiles[i] = new AddingFile
                {
                    File = files[i],
                    Name = Convert.ToString(productImagesDTO.ImageNumbers[i])
                };

            await _fileService.Save(addingFiles,
                await _fileService.CreateOrFindDirectory(_pathsHelper.GetProductFolderPath() + $"/{productImagesDTO.ProductId}"));
        }

        [Authorize]
        [HttpDelete("DeleteImage")]
        public async Task DeleteImage(int productId, int imageNumber)
            => await _fileService.Delete(_pathsHelper.GetProductFilePathByName(productId, Convert.ToString(imageNumber)));

        [HttpGet("GetAllProductImagesZip")]
        public async Task<IActionResult> GetAllProductImagesZip(int productId)
            => new FileStreamResult(await _fileService.CompressToZip(_pathsHelper.GetAllProductImagesPaths(productId)), "application/zip")
            {
                FileDownloadName = "result.zip"
            };

        [HttpPost("GetProductMainImagesZip")]
        public async Task<IActionResult> GetProductMainImagesZip(int[] productIds)
        {
            string[] names = new string[productIds.Length];
            for(int i = 0; i < productIds.Length; i++)
                names[i] = Convert.ToString(productIds[i]);

            return new FileStreamResult(await _fileService.CompressToZip(_pathsHelper.GetMainProductsImagesPaths(productIds), names), "application/zip")
            {
                FileDownloadName = "result.zip"
            };
        }
    }
}
