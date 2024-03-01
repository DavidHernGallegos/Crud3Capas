using Estructura.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estructura.Presentacion.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetALL()
        {
            Dictionary<string,object> respuesta = Negocio.Empleado.GetAll();
            bool resultado = (bool)respuesta["Resultado"];
            if(resultado)
            {
                Empleado empleado = (Empleado)respuesta["Empleado"];
                return View(empleado);

            }
            else
            {
                return View();
            }
        }


        [HttpGet] 
        public ActionResult Form() {
        
            Empleado empleado = new Empleado();

            Dictionary<string,object> respuestaPuesto = Negocio.Puesto.GetAll();
            bool resultado = (bool)respuestaPuesto["Resultado"];
            if (resultado)
            {
                Puesto puestos = (Puesto)respuestaPuesto["Puesto"];
                empleado.Puesto = new Puesto();
                empleado.Puesto = puestos;
            }

            Dictionary<string, object> respuestaDepartamento = Negocio.Departamento.GetAll();
            bool resultadoDep = (bool)respuestaDepartamento["Resultado"];
            if (resultadoDep)
            {
                Departamento departamentos = (Departamento)respuestaDepartamento["Departamento"];
                empleado.Departamento = new Departamento();
                empleado.Departamento = departamentos;
            }

          
           
            return View(empleado);
        }

        [HttpPost]
        public ActionResult Form(Empleado empleado)
        {
            Dictionary<string, object> respuesta = Negocio.Empleado.Add(empleado);
            bool resultado = (bool)respuesta["Resultado"];
            string mensaje = (string)respuesta["Mensaje"];
            if (resultado)
            {

                ViewBag.Mensaje = mensaje;
                return View("Modal");

            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View("Modal");
            }

        }

        public ActionResult Delete(int idEmpleado) {
            Dictionary<string, object> respuesta = Negocio.Empleado.Delete(idEmpleado);

            bool resultado = (bool)respuesta["Resultado"];
            string mensaje = (string)respuesta["Mensaje"];
            if (resultado)
            {

                ViewBag.Mensaje = mensaje;
                return View("Modal");

            }
            else
            {
                ViewBag.Mensaje = mensaje;
                return View("Modal");
            }
        }
    }
}