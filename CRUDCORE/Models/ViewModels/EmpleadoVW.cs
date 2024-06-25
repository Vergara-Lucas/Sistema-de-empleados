using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDCORE.Models.ViewModels
{
    public class EmpleadoVW
    {
        public Empleado oEmpleado { get; set; }
        public List<SelectListItem> oListCargo{ get; set; }
        public List<SelectListItem> oListEmpresa { get; set; }
    }
}
