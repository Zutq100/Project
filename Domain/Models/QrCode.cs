using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class QrCode
    {
        public int Id { get; set; }
        public byte[] BytesQrCode { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
