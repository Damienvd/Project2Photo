using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPG323.P2.Services.Library.Models.Domain
{
    public class FileItemCreateDetail
    {
        public IFormFile File { get; set; }

        public string Geolocation { get; set; }

        public int UserId { get; set; }

        public List<string> Tags { get; set; }
    }
}
