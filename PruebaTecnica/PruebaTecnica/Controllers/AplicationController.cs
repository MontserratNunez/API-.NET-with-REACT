using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Data;
using PruebaTecnica.Entities;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContribuyenteController : ControllerBase
    {
        private readonly IContribuyenteService _contribuyenteService;

        public ContribuyenteController(IContribuyenteService contribuyenteService)
        {
            _contribuyenteService = contribuyenteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContribuyentes(
            [FromQuery] string name = null, 
            [FromQuery] string type = null, 
            [FromQuery] string status = null)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var contribuyentes = await _contribuyenteService.GetAllContribuyentesAsync(name, type, status);
                response.Status = true;
                response.Message = "Success";
                response.Data = contribuyentes;

                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpGet("{rncCedula}/comprobantes")]
        public async Task<IActionResult> GetComprobantes(string rncCedula)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var comprobantes = await _contribuyenteService.GetComprobantesByContribuyenteAsync(rncCedula);
                var totalITBIS = await _contribuyenteService.GetTotalITBISAsync(rncCedula);
                response.Status = true;
                response.Message = "Success";
                response.Data = new { TotalITBIS = totalITBIS, Comprobantes = comprobantes };
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpGet("{rncCedula}")]
        public async Task<IActionResult> GetContribuyente(string rncCedula)
        {
            BaseResponseModel response = new BaseResponseModel();

            try
            {
                var contribuyente = await _contribuyenteService.GetContribuyenteAsync(rncCedula);
                response.Status = true;
                response.Message = "Success";
                response.Data = new { Contribuyente = contribuyente };
                return Ok(response);
            }
            catch (Exception)
            {
                response.Status = false;
                response.Message = "Something went wrong";

                return BadRequest(response);
            }
        }

        [HttpPost("addContribuyente")]
        public async Task<IActionResult> AddContribuyente(Contribuyente contribuyente)
        {
            var (status, message) = await _contribuyenteService.AddContribuyenteAsync(contribuyente);
            if (status)
            {
                return Ok(new { success = status, message });
            }
            return BadRequest(new { success = status, message });
        }

        [HttpPost("addComprobante")]
        public async Task<IActionResult> AddComprobante(ComprobanteFiscal comprobante)
        {
            var (status, message) = await _contribuyenteService.AddComprobanteFiscalAsync(comprobante);
            if (status)
            {
                return Ok(new { success = status, message });
            }
            return BadRequest(new { success = status, message });
        }

        [HttpDelete]
        [Route("deleteContribuyente/{rncCedula}")]
        public async Task<IActionResult> DeleteContribuyente(string rncCedula)
        {
            var (status, message) = await _contribuyenteService.DeleteContribuyenteAsync(rncCedula);

            if (status)
            {
                return Ok(new { success = status, message });
            }

            return BadRequest(new { success = status, message });
        }

        [HttpDelete]
        [Route("deleteComprobante/{ncf}")]
        public async Task<IActionResult> DeleteComprobante(string rncCedula, string ncf)
        {
            var (status, message) = await _contribuyenteService.DeleteComprobanteFiscalAsync(rncCedula, ncf);

            if (status)
            {
                return Ok(new { success = status, message });
            }

            return BadRequest(new { success = status, message });
        }
    }
}
