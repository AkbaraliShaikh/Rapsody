﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNetCore.Api.Services
{
    public class FileService : IFileService
    {
        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", DateTime.Now.ToString("ddmmyyhhmmss") + "_" + file.FileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return path;
        }
    }
}
