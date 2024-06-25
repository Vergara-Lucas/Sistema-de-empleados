using CRUDCORE.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDCORE.Models.ViewModels;
using CRUDCORE.Interface;


namespace CRUDCORE.Controllers
{
    public class HomeController : Controller 
    {
        private readonly DBCrudContext _DBcontext;

        public HomeController(DBCrudContext context)
        {
            _DBcontext = context;
        }

        public IActionResult Index()
        {
            List<Empleado> empleados = _DBcontext.Empleados.Include(c=>c.oCargo).Include(e=>e.oEmpresa).ToList();
            return View(empleados);
        }

        [HttpGet]
        public IActionResult Empleado_Detalle(int idEmpleado)
        {
            EmpleadoVW oEmpleadoVM = new EmpleadoVW() {
                oEmpleado = new Empleado(),
                oListCargo = _DBcontext.Cargos.Select(cargo => new SelectListItem() {
                    Text = cargo.Descripcion,
                    Value = cargo.IdCargo.ToString()
                }).ToList(),
                oListEmpresa = _DBcontext.Empresas.Select(empresa=> new SelectListItem() { 
                    Text = empresa.NombreEmpresa,
                    Value = empresa.IdEmpresa.ToString()
                }).ToList()
            };
            if (idEmpleado!=0) {
                oEmpleadoVM.oEmpleado = _DBcontext.Empleados.Find(idEmpleado);
            } 
            return View(oEmpleadoVM);
        }
        [HttpPost]
        public IActionResult Empleado_Detalle(EmpleadoVW oEmpleadoVW)
        {
            //creamos empleado
            if (oEmpleadoVW.oEmpleado.IdEmpleado == 0)
            {
                _DBcontext.Empleados.Add(oEmpleadoVW.oEmpleado);
            }
            else {
                _DBcontext.Empleados.Update(oEmpleadoVW.oEmpleado);
            }
            _DBcontext.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        public IActionResult Eliminar(int IdEmpleado)
        {
            Empleado empleado = _DBcontext.Empleados.Find(IdEmpleado);
            _DBcontext.Attach(empleado);
            _DBcontext.Remove(empleado);
            _DBcontext.SaveChanges();            
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Cargo_Detalle(int IdCargo) {
            
            Cargo cargo = new Cargo()
            {
                IdCargo = 0
            };
            return View(cargo);
        }
        [HttpPost]
        public IActionResult Cargo_Detalle(Cargo oCargo)
        {
            Save<Cargo>(oCargo, _DBcontext);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Empresa_Detalle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Empresa_Save(Empresa empresa)
        {
            Save<Empresa>(empresa ,_DBcontext);
            return RedirectToAction("Index", "Home");
        }
        public static void Save<T>(T model, CRUDCORE.Models.DBCrudContext context)
        {
            
            try
            {
                
                context.Add(model);
                context.SaveChanges();
                
            }
            catch (Exception)
            {

                throw;
            }
        }
 
    }

}
