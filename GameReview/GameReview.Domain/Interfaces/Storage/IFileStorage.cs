using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Domain.Interfaces.Storage
{
    public interface IFileStorage
    {
        Task<bool> UploadFile(IFormFile file, string filePath);
        Task<bool> RemoveFile(string filePath);
        Task<bool> IfNotExistCreateDirectory(string directory);
        FileStream GetFile(string filePath);
    }
}
