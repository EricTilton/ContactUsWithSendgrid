using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Models.Requests
{
   public class ContactUsUpdateRequest : ContactUsAddRequest
    {
        [Required, Range(1, Int32.MaxValue)]
        public int Id { get; set; }
    }
}
