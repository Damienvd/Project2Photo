using CMPG323.P2.Services.DataAccess;
using CMPG323.P2.Services.Library.Models.Domain;
using CMPG323.P2.Services.Library.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMPG323.P2.Services.BusinessLogic
{

    #region DI Interface

    public interface IFileService
    {
        Task<int> CreateFile(FileItemCreateDetail fileItemCreateDetail);
        Task<FileStreamResult> DownloadFile(string fileGuid);
        Task DeleteFile(int fileItemId);
    }

    #endregion

    public class FileService : IFileService
    {

        #region Private Fields

        private readonly ImageContext _db;

        private readonly string _filesDirectory;

        #endregion

        #region Constructors

        public FileService(ImageContext db, IConfiguration configuration)
        {
            _db = db;
            _filesDirectory = configuration.GetConnectionString("ImageFiles");
        }

        #endregion

        #region Public Methods

        public async Task<int> CreateFile(FileItemCreateDetail fileCreateDetail)
        {
            if (fileCreateDetail.File != null)
            {
                string path = Path.Combine(_filesDirectory, fileCreateDetail.File.FileName);
                string displayName = Path.GetFileNameWithoutExtension(path);
                string fileExtension = Path.GetExtension(path);

                #region Save Details to Database

                /*
                 * Create FileItem database entry first, as we use the identity 
                 * inserted Id to rename the file name before uploading.
                 */
                FileItem fileItem = new()
                {
                    DisplayName = displayName,
                    ContentType = fileCreateDetail.File.ContentType,
                    Size = fileCreateDetail.File.Length,
                    Extension = fileExtension,
                    Guid = Guid.NewGuid().ToString(),
                    UserId = fileCreateDetail.UserId,
                    DateCaptured = DateTime.Now,
                    Geolocation = fileCreateDetail.Geolocation,
                    Tags = fileCreateDetail.Tags
                };
                _db.Add(fileItem);
                await _db.SaveChangesAsync();

                #endregion

                #region Upload File

                string fileName = $"{fileItem.Id}{fileExtension}"; // FileItem Id as file name to ensure uniqueness.
                string filePath = Path.Combine(_filesDirectory, fileName);
                using (FileStream fileStream = File.Create(filePath))
                {
                    Stream inputStream = fileCreateDetail.File.OpenReadStream();
                    await inputStream.CopyToAsync(fileStream);
                }

                #endregion

                _db.Entry(fileItem).State = EntityState.Detached;
                return fileItem.Id;
            }
            else
            {
                throw new Exception("No file specified.");
            }
        }

        public async Task<FileStreamResult> DownloadFile(string fileGuid)
        {
            FileItem fileItem = await _db.FileItems.AsNoTracking().SingleOrDefaultAsync(e => e.Guid.Equals(fileGuid)) ??
                throw new Exception("File not found");

            try
            {
                FileStream fileStream = File.OpenRead(GetFilePath(fileItem));
                return new FileStreamResult(fileStream, fileItem.ContentType)
                {
                    FileDownloadName = $"{fileItem.DisplayName}{fileItem.Extension}"
                };
            }
            catch (FileNotFoundException)
            {
                throw new Exception("File not found");
            }
        }

        public async Task DeleteFile(int fileItemId)
        {
            FileItem fileItem = await _db.FileItems.SingleOrDefaultAsync(e => e.Id == fileItemId) ??
                throw new Exception("File not found");

            _db.Remove(fileItem);
            await _db.SaveChangesAsync();
        }

        #endregion

        #region Private Methods

        private string GetFilePath(FileItem fileItem)
        {
            string fileName = $"{fileItem.Id}{fileItem.Extension}";
            return Path.Combine(_filesDirectory, fileName);
        }

        #endregion


    }
}
