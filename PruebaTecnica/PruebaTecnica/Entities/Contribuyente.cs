using System.ComponentModel.DataAnnotations;

namespace PruebaTecnica.Entities
{
    public class Contribuyente
    {
        [Key]
        [Required(ErrorMessage = "Ingrese la cédula")]
        public string RncCedula { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ingrese el tipo")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Ingrese el estatus")]
        public string Status { get; set; }
    }
}
