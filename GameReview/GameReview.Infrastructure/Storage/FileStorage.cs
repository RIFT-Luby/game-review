using GameReview.Domain.Interfaces.Storage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Infrastructure.Storage
{
    public class FileStorage: IFileStorage
    {
        public async Task<bool> UploadFile(IFormFile file, string filePath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return true;
        }

        public Task<bool> RemoveFile(string filePath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            File.Delete(path);
            return Task.FromResult(true);
        }

        public FileStream GetFile(string filePath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), filePath);
            return File.OpenRead(path);
        }

        public Task<bool> IfNotExistCreateDirectory(string directory)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), directory);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Task.FromResult(true);
        }
    }
}
