using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Practicas.Models
{
    public class Alumnos
    {
        public int Id { get; set; }
        [Required]
        public String Clave { get; set; }
        [Required]
        public String Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public String Fecha_compra { get; set; }
        [Required]
        public Double Precio_uni { get; set; }
        [Required]
        public Double Total { get; set; }

    }
}
