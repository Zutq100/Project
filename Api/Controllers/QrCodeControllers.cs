using Application.DTO;
using Application.Request;
using MediatR;
using QRCoder;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QrCodeControllers : ControllerBase
    {
        private readonly IMediator _mediator;

        public QrCodeControllers(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создания Qr-кода на основе - названия мероприятия, пароля к мероприяютию, 
        /// локации, даты начала, даты конца, false/true на весь день.
        /// </summary>
        /// <param name="client">ДТО для создания клиента</param>
        /// <param name="cancellationToken">Токен для отмены выполнения задачи</param>
        /// <returns>Возвращает код ответа 201</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateItem([FromBody] CreateQrCodeItemCommand client, CancellationToken cancellationToken)
        {
            var entity = await _mediator.Send(client, cancellationToken);
            return Ok(entity);
        }

        /// <summary>
        /// Вовзращает массив бит Qr-кода по соответствующему идентификатору
        /// </summary>
        /// <param name="id">Идентификатор Qr-кода из базы данных</param>
        /// <param name="cancellationToken">Токен для отмены выполнения задачи</param>
        /// <returns> Id, массив бит Qr-кода, дату создания Qr-кода</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetItem(int id, CancellationToken cancellationToken)
        {
            var query = new GetQrCodeItemQuery(id);
            var item = await _mediator.Send(query, cancellationToken);

            return item is not null ? Ok(item) : NotFound();
        }

        /// <summary>
        /// Возвращает все Qr-коды из базы данных
        /// </summary>
        /// <param name="cancellationToken">Токен для отмены выполнения задачи</param>
        /// <returns>Все Qr-коды из таблицы</returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllItems(CancellationToken cancellationToken)
        {
            var query = new GetAllQrCodeItemsQuery();
            var items = await _mediator.Send(query, cancellationToken);

            return Ok(items);
        }


    }
}
