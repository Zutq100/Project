namespace Application.Request
{
    public record GetAllQrCodeItemsQuery() : IRequest<IEnumerable<QrCodeItemDTO>>;
    public class GetAllQrCodeItemsHandler : IRequestHandler<GetAllQrCodeItemsQuery, IEnumerable<QrCodeItemDTO>>
    {
        private readonly QrCodeDbContext _dbContext;
        public GetAllQrCodeItemsHandler(QrCodeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<QrCodeItemDTO>> Handle(GetAllQrCodeItemsQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.QrCodes.AsQueryable();

            var items = await query.ToListAsync(cancellationToken);

            return items.Select(x => new QrCodeItemDTO(x.Id, x.BytesQrCode, x.DateOfCreation));

        }
    }
}

