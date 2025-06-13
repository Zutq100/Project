using Application.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler
{
    public record CreateQrCodeItemCommand(QrCodeCreateItemDTO Dto) : IRequest<QrCodeCreateItemDTO>;
    public class CreateQrCodeItemHandler : IRequestHandler<CreateQrCodeItemCommand, QrCodeCreateItemDTO>
    {
        private readonly QrCodeDbContext _dbContext;
        private readonly IQrCodeService _qrCodeService;
        public CreateQrCodeItemHandler(QrCodeDbContext dbContext, IQrCodeService qrCodeService)
        {
            _dbContext = dbContext;
            _qrCodeService = qrCodeService;
        }
        public async Task<QrCodeCreateItemDTO> Handle(CreateQrCodeItemCommand request, CancellationToken cancellationToken)
        {
            var item = new QrCodeCreateItemDTO(request.Dto.subject, request.Dto.password, request.Dto.location, request.Dto.start, request.Dto.end, request.Dto.allDay);
            var bytesQrCode = _qrCodeService.GenerationCalendarEvent(request.Dto.subject, request.Dto.password, request.Dto.location, request.Dto.start, request.Dto.end, request.Dto.allDay);

            var qrCode = new QrCode() { BytesQrCode = bytesQrCode, DateOfCreation = DateTime.UtcNow };
            await _dbContext.QrCodes.AddAsync(qrCode);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
