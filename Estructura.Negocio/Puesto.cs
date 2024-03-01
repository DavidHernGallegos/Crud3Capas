using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Negocio
{
    public class Puesto
    {
        public int IdPuesto { get; set; }
        public string Descripcion { get; set; }

        public List<object> Puestos { get; set; }

        public static Dictionary<string, object> GetAll()
        {
            Puesto puesto = new Puesto();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Puesto", puesto }, { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (AccesoDatos.BD3CapasEntities context = new AccesoDatos.BD3CapasEntities())
                {
                    puesto.Puestos = new List<object>();
                    var query = context.PuestoGetAll().ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            Puesto PuestoObj = new Puesto();
                            PuestoObj.IdPuesto = item.PuestoID;
                            PuestoObj.Descripcion = item.Descripcion;
                           

                           puesto.Puestos.Add(PuestoObj);

                        }

                        diccionario["Puesto"] = puesto;
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
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Mensaje"] = "No se han cargado los datos" + ex;
            }

            return diccionario;
        }


    }
}
