using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Entities
{
    public class ComprobanteFiscal
    {
        [Required(ErrorMessage = "Ingrese la cédula")]
        public string RncCedula { get; set; }
        [Required(ErrorMessage = "Ingrese el NCF")]
        public string NCF { get; set; }
        [Required(ErrorMessage = "Ingrese el monto")]
        public decimal Amount { get; set; }
        public decimal Itbis { get; set; }

        public void CalculateItbis()
        {
            Itbis = Amount * 0.18m;
        }
    }
}
