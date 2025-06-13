namespace Application.Request
{
    public record GetQrCodeItemQuery(int id) : IRequest<QrCodeItemDTO>;
    public class GetQrCodeItemQueryHandler : IRequestHandler<GetQrCodeItemQuery, QrCodeItemDTO>
    {
        private readonly QrCodeDbContext _dbContext;
        public GetQrCodeItemQueryHandler(QrCodeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<QrCodeItemDTO> Handle(GetQrCodeItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _dbContext.QrCodes.FirstOrDefaultAsync(i => i.Id == request.id, cancellationToken);

            return item is null ? null : new QrCodeItemDTO(item.Id, item.BytesQrCode, item.DateOfCreation);
        }
    }
}
