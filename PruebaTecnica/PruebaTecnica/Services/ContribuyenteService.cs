using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Data;
using PruebaTecnica.Entities;
using System.Text.RegularExpressions;

namespace PruebaTecnica.Services
{
    public class ContribuyenteService : IContribuyenteService
    {
        private readonly ApplicationDbContext _context;

        public ContribuyenteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contribuyente>> GetAllContribuyentesAsync(string name = null, string type = null, string status = null)
        {
            var query = _context.Contribuyentes.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(c => c.Type == type.ToUpper());
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(c => c.Status == status.ToUpper());
            }

            return await query.ToListAsync();
        }

        public async Task<Contribuyente> GetContribuyenteAsync(string rncCedula)
        {
            return await _context.Contribuyentes.SingleOrDefaultAsync(c => c.RncCedula == rncCedula); ;
        }

        public async Task<IEnumerable<ComprobanteFiscal>> GetComprobantesByContribuyenteAsync(string rncCedula)
        {
            return await _context.ComprobantesFiscales.Where(c => c.RncCedula == rncCedula).ToListAsync();
        }

        public async Task<decimal> GetTotalITBISAsync(string rncCedula)
        {
            return await _context.ComprobantesFiscales.Where(c => c.RncCedula == rncCedula).SumAsync(c => c.Itbis);
        }

        public async Task<(bool, string)> AddContribuyenteAsync(Contribuyente contribuyente)
        {

            contribuyente.Name = contribuyente.Name.ToUpper();
            contribuyente.Type = contribuyente.Type.ToUpper();

            if (contribuyente.Type != "PERSONA FISICA" && contribuyente.Type != "PERSONA JURIDICA")
            {
                return (false, "El tipo de contribuyente solo puede ser 'PERSONA FISICA' o 'PERSONA JURIDICA'.");
            }

            if (contribuyente.Type == "PERSONA FISICA")
            {
                if (contribuyente.RncCedula.Length != 11 || !Regex.IsMatch(contribuyente.RncCedula, @"^\d+$"))
                {
                    return (false, "La cédula debe tener 11 caracteres numéricos.");
                }
            }else if (contribuyente.Type == "PERSONA JURIDICA")
            {
                if (contribuyente.RncCedula.Length != 9 || !Regex.IsMatch(contribuyente.RncCedula, @"^\d+$"))
                {
                    return (false, "El RNC debe tener 9 caracteres numéricos.");
                }
            }

            var exists = await _context.Contribuyentes.AnyAsync(c => c.RncCedula == contribuyente.RncCedula);
            if (exists)
            {
                return (false, "Ya existe un contribuyente con esa cédula.");
            }

            contribuyente.Status = contribuyente.Status.ToUpper();
            if (contribuyente.Status != "ACTIVO" && contribuyente.Status != "INACTIVO")
            {
                return (false, "El estatus solo puede ser 'ACTIVO' o 'INACTIVO'.");
            }

            _context.Contribuyentes.Add(contribuyente);
            await _context.SaveChangesAsync();

            return (true, "Contribuyente agregado exitosamente.");
        }

        public async Task<(bool, string)> AddComprobanteFiscalAsync(ComprobanteFiscal comprobante)
        {
            var contribuyenteExists = await _context.Contribuyentes.AnyAsync(c => c.RncCedula == comprobante.RncCedula);
            if (!contribuyenteExists)
            {
                return (false, "El RNC/Cédula no existe en el sistema.");
            }

            if (comprobante.NCF.Length != 11 || !Regex.IsMatch(comprobante.NCF, @"^[A-Z]{1}\d{10}$"))
            {
                return (false, "El NCF debe estar compuesto por 1 letra y 10 números.");
            }

            comprobante.CalculateItbis();

            _context.ComprobantesFiscales.Add(comprobante);
            await _context.SaveChangesAsync();

            return (true, "Comprobante fiscal agregado exitosamente.");
        }

        public async Task<(bool, string)> DeleteContribuyenteAsync(string rncCedula)
        {
            var contribuyente = await _context.Contribuyentes.FindAsync(rncCedula);
            if (contribuyente == null)
            {
                return (false, "El contribuyente no existe.");
            }

            var hasComprobantes = await _context.ComprobantesFiscales.AnyAsync(c => c.RncCedula == rncCedula);
            if (hasComprobantes)
            {
                return (false, "No se puede eliminar el contribuyente porque tiene comprobantes fiscales asociados.");
            }

            _context.Contribuyentes.Remove(contribuyente);
            await _context.SaveChangesAsync();

            return (true, "Contribuyente eliminado exitosamente.");
        }

        public async Task<(bool, string)> DeleteComprobanteFiscalAsync(string rncCedula, string ncf)
        {
            var comprobante = await _context.ComprobantesFiscales.FirstOrDefaultAsync(c => c.RncCedula == rncCedula && c.NCF == ncf);
            if (comprobante == null)
            {
                return (false, "El comprobante fiscal no existe.");
            }

            _context.ComprobantesFiscales.Remove(comprobante);
            await _context.SaveChangesAsync();

            return (true, "Comprobante fiscal eliminado exitosamente.");
        }
    }
}
