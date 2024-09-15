using PruebaTecnica.Entities;

namespace PruebaTecnica.Services
{
    public interface IContribuyenteService
    {
        Task<IEnumerable<Contribuyente>> GetAllContribuyentesAsync(string name = null, string type = null, string status = null);
        Task<Contribuyente> GetContribuyenteAsync(string rncCedula);
        Task<IEnumerable<ComprobanteFiscal>> GetComprobantesByContribuyenteAsync(string rncCedula);
        Task<decimal> GetTotalITBISAsync(string rncCedula);
        Task<(bool, string)> AddContribuyenteAsync(Contribuyente contribuyente);
        Task<(bool, string)> AddComprobanteFiscalAsync(ComprobanteFiscal comprobante);
        Task<(bool, string)> DeleteContribuyenteAsync(string rncCedula);
        Task<(bool, string)> DeleteComprobanteFiscalAsync(string rncCedula, string ncf);
    }
}
