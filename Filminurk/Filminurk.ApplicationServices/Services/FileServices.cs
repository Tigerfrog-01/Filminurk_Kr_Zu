﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filminurk.Core.Domain;
using Filminurk.Core.dto;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;

namespace Filminurk.ApplicationServices.Services
{
    public class FileServices : IFilesServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly FilminurkTARpe24Context _context;

        public FileServices(IHostEnvironment webHost, FilminurkTARpe24Context context)
        {
            _webHost = webHost;
            _context = context;
        }
        public void FilesToApi(MoviesDTO dto, Movie domain)
        {
            if (dto.Files == null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\wwroot\\multipleFileUpload"))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\wwroot\\multipleFileUpload");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "wwroot", "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        FileToApi path = new FileToApi
                        {
                            ImageID = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            MovieID = domain.ID,
                        };
                        _context.FilesToApi.Add(path);
                    }
                }
            }
        }
        public async Task<FileToApi> RemoveImagesFromApi(FileToApiDTO dto)
        {
            var imageID = await _context.FilesToApi.FirstOrDefaultAsync(x => x.ImageID == dto.ImageID);

            var filePath = _webHost.ContentRootPath + "\\wwroot\\multipleFileUpload\\" + imageID.ExistingFilePath;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FilesToApi.Remove(imageID);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDTO[] dtos)
        {
            foreach (var dto in dtos)
            {
            RemoveImagesFromApi(dto);
            }
    return null;
        }
    }
}
