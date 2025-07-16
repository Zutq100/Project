using System.Drawing;

namespace Application.Services
{
    public class QrCodeService : IQrCodeService
    {
        public byte[] GenerationCalendarEvent(string subject, string password, string location, DateTimeOffset start, DateTimeOffset end, bool allDay)
        {
            var payLoad = new PayloadGenerator.CalendarEvent(subject, password, location, start, end, allDay);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrData = qrGenerator.CreateQrCode(payLoad);

            BitmapByteQRCode qrCode = new BitmapByteQRCode(qrData);

            return qrCode.GetGraphic(20);
        }
    }
}
