using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Models.Requests
{
    public class ContactUsRequest
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Question { get; set; }
        public string SendGridKey { get; set; }
        public string AdminEmail { get; set; }
        public string ContactUsEmailTemplate { get; set; }
    }
}
