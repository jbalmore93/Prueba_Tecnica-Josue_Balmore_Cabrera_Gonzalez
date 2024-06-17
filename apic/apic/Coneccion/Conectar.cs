using apic.Modelo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace apic.Coneccion
{
    public class Conectar
    {   
        private static string server = "LAPTOP - F4MGA7VC";
        private static string db = "prueba";
        private static string cadena = "Data Source="+server+";Initial Catalog="+db+";Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True";
        private static string insert = "INSERT INTO usuario (nombre_usuario, suma_puntos) VALUES (@usuario, @puntos)";
        private static string update = "update usuario  set suma_puntos = @puntos where nombre_usuario = @usuario";
        private static string select = "select * from usuario where nombre_usuario = @usuario";
        public static object insertar(Usuario user)
        {
            using (SqlConnection conectar = new SqlConnection(cadena))
            {
                try
                {
                    string usuario = user.Nombreusuario;
                    double puntos = user.SumaPuntos;
                    conectar.Open();
                    DataSet comprobar = Mostrar(usuario);
                    if (comprobar.Tables.Count == 0)
                    {
                        string consulta = insert;
                        using (SqlCommand comando = new SqlCommand(consulta, conectar))
                        {
                            comando.CommandType = CommandType.Text;
                            comando.Parameters.AddWithValue("@usuario", usuario);
                            comando.Parameters.AddWithValue("@puntos", puntos);
                            comando.ExecuteNonQuery();
                            conectar.Close();
                            return new
                            {
                                usuario = usuario,
                                mensaje = "Insertado con éxito",
                            };
                        }
                    }
                    else
                    {
                        string consulta = update;
                        using (SqlCommand comando = new SqlCommand(consulta, conectar))
                        {
                            comando.CommandType = CommandType.Text;
                            comando.Parameters.AddWithValue("@usuario", usuario);
                            comando.Parameters.AddWithValue("@puntos", puntos);
                            comando.ExecuteNonQuery();
                            conectar.Close();
                            return new
                            {
                                usuario = usuario,
                                mensaje = "modificado con exito",
                            };
                        }
                    }
                    
                    
                }
                catch (Exception ex)
                {
                    conectar.Close();
                    // Devolver información detallada del error
                    return new
                    {
                        mensaje = "Error al insertar",
                        error = ex.Message
                    };
                }
            }
        }
        public static DataSet Mostrar(string usuario)
        {
            using (SqlConnection conectar = new SqlConnection(cadena))
            {
                try
                {
                    conectar.Open();
                    string consultar = select;
                    using (SqlCommand consult = new SqlCommand(consultar, conectar))
                    {
                        consult.CommandType = CommandType.Text;
                        consult.Parameters.AddWithValue("@usuario", usuario);
                        DataSet tabla = new DataSet();
                        SqlDataAdapter da = new SqlDataAdapter(consult);
                        da.Fill(tabla);
                        conectar.Close();
                        return tabla;
                    }
                }
                catch (Exception ex)
                {
                    return new DataSet();
                }
            }
        }
    }
}
