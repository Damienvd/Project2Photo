using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPG323.P2.Services.Library.Models.Entities
{
    public partial class FileItem
    {
        public int Id { get; set; }

        public string Guid { get; set; }

        public string DisplayName { get; set; }

        public string Geolocation { get; set; }

        public long Size { get; set; }

        public string ContentType { get; set; }

        public string Extension { get; set; }

        public DateTime DateCaptured { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<string> Tags { get; set; }
    }
}
