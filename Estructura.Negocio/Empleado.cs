using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Negocio
{

    
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public List<object> Empleados { get; set; }
        public Puesto Puesto { get; set; }
        public Departamento Departamento { get; set; }

        public static Dictionary<string,object> GetAll()
        {
            Empleado empleado = new Empleado(); 
            
            Dictionary<string,object> diccionario = new Dictionary<string, object> { {"Empleado", empleado },{"Resultado", false },{"Mensaje", "" } };

            try
            {
                using (AccesoDatos.BD3CapasEntities context = new AccesoDatos.BD3CapasEntities())
                {
                    empleado.Empleados = new List<object>();
                    var query = context.VistaUnions.ToList();

                    if(query != null)
                    {
                        foreach (var item in query)
                        {
                            Empleado empleadoObj = new Empleado();
                            empleadoObj.IdEmpleado = item.EmpleadoID;
                            empleadoObj.Nombre = item.Nombre;
                            empleadoObj.Puesto = new Puesto();
                            empleadoObj.Puesto.IdPuesto = item.PuestoID;
                            empleadoObj.Puesto.Descripcion = item.DescripcionPuesto;
                            empleadoObj.Departamento = new Departamento();
                            empleadoObj.Departamento.IdDepartamento = item.DepartamentoId;
                            empleadoObj.Departamento.Descripcion = item.DescripcionDepartamento;

                            empleado.Empleados.Add(empleadoObj);

                        }

                        diccionario["Empleado"] = empleado;
                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han cargado los datos";
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han cargado los datos";
                    }
                    
                }
            }
            catch(Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han cargado los datos";
            }

            return diccionario;
        }

        public static Dictionary<string, object> Add( Empleado empleado )
        {
           

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (AccesoDatos.BD3CapasEntities context = new AccesoDatos.BD3CapasEntities())
                {
                    
                    var query = context.AddEmpleado(empleado.Nombre, empleado.Puesto.IdPuesto, empleado.Departamento.IdDepartamento);

                    if (query > 0)
                    {
                       
                     
                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han guardado los datos";
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han guardado los datos";
                    }

                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han guardado los datos" + ex;
            }

            return diccionario;
        }

        public static Dictionary<string, object> Delete(int idEmpleado)
        {


            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (AccesoDatos.BD3CapasEntities context = new AccesoDatos.BD3CapasEntities())
                {

                    var query = context.DeleteEmpleado(idEmpleado);

                    if (query > 0)
                    {


                        diccionario["Resultado"] = true;
                        diccionario["Mensaje"] = "Se han eliminado los datos";
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                        diccionario["Mensaje"] = "No se han eliminado los datos";
                    }

                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han eliminado los datos" + ex;
            }

            return diccionario;
        }



    }
}
