using System;
using System.Collections.Generic;

namespace CRUDCORE.Models
{
    public partial class Empresa
    {
        public Empresa()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? Ubicacion { get; set; }
        public string? Contacto { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
