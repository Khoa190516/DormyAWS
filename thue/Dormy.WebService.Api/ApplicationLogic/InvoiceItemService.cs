using Dormy.WebService.Api.Core.Constants;
using Dormy.WebService.Api.Core.Interfaces;
using Dormy.WebService.Api.Models.RequestModels;
using Dormy.WebService.Api.Models.ResponseModels;
using Dormy.WebService.Api.Presentation.Mappers;
using Dormy.WebService.Api.Startup;

namespace Dormy.WebService.Api.ApplicationLogic
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContextService _userContextService;
        private readonly InvoiceItemMapper _invoiceItemMapper;

        public InvoiceItemService(IUnitOfWork unitOfWork, IUserContextService userContextService)
        {
            _unitOfWork = unitOfWork;
            _invoiceItemMapper = new InvoiceItemMapper();
            _userContextService = userContextService;
        }

        public Task<ApiResponse> CreateInvoiceItemsBatch(List<InvoiceItemRequestModel> models)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> HardDeleteInvoiceItemsBatchByInvoiceId(Guid invoiceId)
        {
            var invoiceEntity = await _unitOfWork.InvoiceRepository.GetAsync(i => i.Id == invoiceId);
            if (invoiceEntity == null)
            {
                return new ApiResponse().SetNotFound(invoiceId, message: string.Format(ErrorMessages.PropertyDoesNotExist, "Invoice"));
            }

            var invoiceItems = await _unitOfWork.InvoiceItemRepository.GetAllAsync(x => x.InvoiceId == invoiceId);
            var invoiceItemIds = invoiceItems.Select(x => x.Id).ToList();
            foreach (var invoiceItemId in invoiceItemIds)
            {
                await _unitOfWork.InvoiceItemRepository.DeleteByIdAsync(invoiceItemId);
            }

            return new ApiResponse().SetOk(invoiceId);
        }
    }
}
