using MonitorExchange.Dtos.FileExchange;
using MonitorExchange.Models;


namespace MonitorExchange.Services.FileExchangeService
{
    public interface IFileExchangeService
    {
        Task<ServiceResponse<List<GetFileExchangeDto>>> GetAllFileExchange();

        Task<ServiceResponse<GetFileExchangeDto>> GetSingleFileExchangeById(string strId);

        Task<ServiceResponse<List<GetFileExchangeDto>>> GetFileExchange(int page, int pageSize);

        Task<ServiceResponse<List<GetFileExchangeDto>>> AddFileExchange(AddFileExchangeDto newFileExchange);

        Task<ServiceResponse<GetFileExchangeDto>> UpdateFileExchange(UpdateFileExchangeDto updateFileExchange);

        Task<ServiceResponse<List<GetFileExchangeDto>>> DeleteFileExchange(string strId);

        Task<FileExchange> SaveFileExchangeToBaseAsync(FileExchange fileExchanges);

        Task<FileExchange> UpdateFileExchangeToBaseAsync(FileExchange updateFileExchange);

        Task<List<FEImport>> SaveFileExchangeImportToBaseAsync(List<FEImport> imports);

        Task<List<FEOffers>> SaveFileExchangeOffersToBaseAsync(List<FEOffers> offers);

    }
}
