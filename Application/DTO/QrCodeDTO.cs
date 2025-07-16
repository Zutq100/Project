using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
        public record QrCodeCreateItemDTO(
            string? subject,
            string? password,
            string? location,
            DateTimeOffset start,
            DateTimeOffset end,
            bool allDay);
        
        public record QrCodeItemDTO(
            int id,
            byte[] bytesQrCode,
            DateTime dateOfCreation);

}
