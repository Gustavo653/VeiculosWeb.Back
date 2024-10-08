﻿using Microsoft.AspNetCore.Http;

namespace VeiculosWeb.Infrastructure.Service
{
    public interface IGoogleCloudStorageService
    {
        Task<string> UploadFileToGcsAsync(IFormFile file, string objectName);
        Task DeleteFileFromGcsAsync(string? objectName);
        string GetUrl();
    }
}
