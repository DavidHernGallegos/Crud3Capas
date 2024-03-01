using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura.Negocio
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }
        public string Descripcion { get; set; }

        public List<object> Departamentos { get; set; }



        public static Dictionary<string, object> GetAll()
        {
            Departamento departamento = new Departamento();

            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Departamento", departamento }, { "Resultado", false }, { "Mensaje", "" } };

            try
            {
                using (AccesoDatos.BD3CapasEntities context = new AccesoDatos.BD3CapasEntities())
                {
                    departamento.Departamentos = new List<object>();
                    var query = context.DepartamentoGetAll().ToList();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            Departamento departamentoObj = new Departamento();
                            departamentoObj.IdDepartamento = item.DepartamentoId;
                            departamentoObj.Descripcion = item.Descripcion;


                            departamento.Departamentos.Add(departamentoObj);

                        }

                        diccionario["Departamento"] = departamento;
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
