using CMPG323.P2.Applications.Web.Contracts;
using CMPG323.P2.Services.BusinessLogic;
using CMPG323.P2.Services.DataAccess;
using CMPG323.P2.Services.Library.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMPG323.P2.Applications.Web.Controllers
{
    [Authorize]
    [Route("api/files")]
    [ApiController]
    public class FileController : ControllerBase
    {

        #region Private Fields

        private readonly IFileService _fileService;

        private readonly ImageContext _db;

        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructors

        public FileController(IFileService fileService, ImageContext db, IAuthenticationService authenticationService)
        {
            _fileService = fileService;
            _db = db;
            _authenticationService = authenticationService;
        }

        #endregion

        #region Public Methods

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FileItemCreateRequest fileItem)
        {
            int newFileItemId = await _fileService.CreateFile(new FileItemCreateDetail
            {
                File = fileItem.File,
                Geolocation = fileItem.Geolocation,
                Tags = fileItem.Tags,
                UserId = fileItem.UserId
            });
            return Ok(_db.FileItems.Where(x => x.Id == newFileItemId));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<FileStreamResult> Download([FromQuery] string guid, [FromQuery] string token)
        {
            _ = await _authenticationService.AuthorizeUser(token) ??
                throw new Exception("Unauthorized");
            return await _fileService.DownloadFile(guid);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fileService.DeleteFile(id);
            return Ok();
        }

        #endregion
    }
}
