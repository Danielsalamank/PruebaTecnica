using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using RestApi.Models;
using System.Globalization;
using Microsoft.OpenApi.Models;

namespace RestApi.Controllers
{
    public class Usuarios : Controller
    {
        /// <summary>
        /// Property encargada de manejar la conexión con la BD
        /// </summary>
        private readonly IConfiguration Connect;

        /// <summary>
        /// Asignacion de configuracion de la propiedad de conexión a la base de datos
        /// </summary>
        /// <param name="configuration"> Parametro de configuracion obtenido desde appsettings.json</param>
        public Usuarios(IConfiguration configuration)
        {
            Connect = configuration;
        }


        #region Get_Usuarios
        /// <summary>
        /// Metodo GetUsuarios
        /// </summary>
        /// <returns>lista de los usuarios</returns>
        [HttpGet]
        [EnableCors("CorsPruebaTecnica")]
        [Route("api/Usuarios/Obtener")]
        public JsonResult GetUsuarios()
        {
            SqlConnection CnxUsuarios = new SqlConnection(Connect["ConnectionStrings:Connect"]);
            CnxUsuarios.Open();
            string consulta = @" SELECT U.CODIGO, U.NOMBRES, U.APELLIDO, U.FECHA_NACIMIENTO, 
                              U.FOTO_USUARIO, 
                              T.NOMBRE AS ESTADO_CIVIL, U.TIENE_HERMANOS 
                              FROM USUARIOS U 
                              INNER JOIN TIPOSESTADO T ON T.CODIGO =U.CODIGO";
            SqlCommand cmd = new SqlCommand(consulta, CnxUsuarios);
            SqlDataReader reader = cmd.ExecuteReader();

            List<UsuariosModel> ListaUsuarios = new List<UsuariosModel>();

            while (reader.Read())
            {
                UsuariosModel Usuario = new UsuariosModel();

                
                Usuario.CODIGO = Convert.ToInt32(reader["CODIGO"].ToString());
                Usuario.NOMBRES = reader["NOMBRES"].ToString();
                Usuario.APELLIDO = reader["APELLIDO"].ToString();
                Usuario.FECHA_NACIMIENTO = (DateTime)reader["FECHA_NACIMIENTO"];
                Usuario.ESTADO_CIVIL = reader["ESTADO_CIVIL"].ToString();
                Usuario.FOTO_USUARIO = reader["FOTO_USUARIO"].ToString();
                Usuario.TIENE_HERMANOS = (Boolean)reader["TIENE_HERMANOS"];

                ListaUsuarios.Add(Usuario);
            }

            

            reader.Close();
            CnxUsuarios.Close();

            return Json(ListaUsuarios.AsEnumerable());
        }
        #endregion


        #region Post_Registro_Usuarios
        /// <summary>
        /// Metodo encargado de realizar el regstro de un usuario
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors("CorsPruebaTecnica")]
        [Route("api/Usuarios/RegistroUsuarios")]
        public JsonResult RegistroUsuarios([FromBody] UsuariosRegistro registro)
        {
            //Se asigna la Fecha y Hora Actual  
            string dateTime = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd hh:mm:ss");
            DateTime FechaActual = DateTime.ParseExact(createddate, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);

            SqlConnection CnxUsuarios = new SqlConnection(Connect["ConnectionStrings:Connect"]);

            //string sqltext11 = "INSERT INTO USUARIOS(NOMBRES, APELLIDO, FECHA_NACIMIENTO, FOTO_USUARIO, ESTADO_CIVIL, TIENE_HERMANOS) VALUES(@NOMBRES, @APELLIDO, @FECHA_NACIMIENTO, cast(N'' as xml).value('xs:base64Binary(sql:variable(""@FOTO_USUARIO""))', 'varbinary(max)'), @ESTADO_CIVIL, @TIENE_HERMANOS)";



            using SqlCommand cmd = new SqlCommand
            {
                Connection = CnxUsuarios,
                CommandType = CommandType.Text,
                CommandText = @"INSERT INTO USUARIOS(NOMBRES, APELLIDO, FECHA_NACIMIENTO, FOTO_USUARIO, ESTADO_CIVIL, TIENE_HERMANOS) 
                                VALUES(@NOMBRES, @APELLIDO, @FECHA_NACIMIENTO, @FOTO_USUARIO, @ESTADO_CIVIL, @TIENE_HERMANOS)"
            };

            cmd.Parameters.AddWithValue("@NOMBRES", registro.NOMBRES);
            cmd.Parameters.AddWithValue("@APELLIDO", registro.APELLIDO);
            cmd.Parameters.AddWithValue("@FECHA_NACIMIENTO", registro.FECHA_NACIMIENTO);
            cmd.Parameters.AddWithValue("@FOTO_USUARIO", registro.FOTO_USUARIO);
            cmd.Parameters.AddWithValue("@ESTADO_CIVIL", registro.ESTADO_CIVIL);
            cmd.Parameters.AddWithValue("@TIENE_HERMANOS", registro.TIENE_HERMANOS);

            try
            {
                CnxUsuarios.Open();
                cmd.ExecuteNonQuery();

                
                List<Respuesta> Listarespuesta = new List<Respuesta>();
                Respuesta respuesta = new Respuesta
                {
                    Error = "No",
                    Mensaje = "Usuario Creado exitosamente"
                };
                Listarespuesta.Add(respuesta);

                return Json(Listarespuesta.AsEnumerable());
            }
            catch (SqlException e)
            {
                List<Respuesta> Listarespuesta = new List<Respuesta>();
                Respuesta respuesta = new Respuesta
                {
                    Error = "Si",
                    Mensaje = e.Message.ToString()
                };
                Listarespuesta.Add(respuesta);

                return Json(Listarespuesta.AsEnumerable());
            }
        }
        #endregion


        #region Post_Actualizar_Usuarios
        /// <summary>
        /// Metodo encargado de realizar el regstro de un usuario
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        [HttpPost]
        [EnableCors("CorsPruebaTecnica")]
        [Route("api/Usuarios/ActualizarUsuario")]
        public JsonResult ActualizarUsuario([FromBody] UsuariosModel registro)
        {
            //Se asigna la Fecha y Hora Actual  
            string dateTime = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd hh:mm:ss");
            DateTime FechaActual = DateTime.ParseExact(createddate, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);

            SqlConnection CnxUsuarios = new SqlConnection(Connect["ConnectionStrings:Connect"]);


            using SqlCommand cmd = new SqlCommand
            {
                Connection = CnxUsuarios,
                CommandType = CommandType.Text,
                CommandText = @"UPDATE USUARIOS 
                                    SET NOMBRES = @NOMBRES,
                                        APELLIDO = @APELLIDO, 
                                        FECHA_NACIMIENTO = @FECHA_NACIMIENTO, 
                                        FOTO_USUARIO = @FOTO_USUARIO,
                                        ESTADO_CIVIL = @ESTADO_CIVIL,
                                        TIENE_HERMANOS = @TIENE_HERMANOS
                                WHERE CODIGO = @CODIGO"
            };

            cmd.Parameters.AddWithValue("@CODIGO", registro.CODIGO);
            cmd.Parameters.AddWithValue("@NOMBRES", registro.NOMBRES);
            cmd.Parameters.AddWithValue("@APELLIDO", registro.APELLIDO);
            cmd.Parameters.AddWithValue("@FECHA_NACIMIENTO", registro.FECHA_NACIMIENTO);
            cmd.Parameters.AddWithValue("@FOTO_USUARIO", registro.FOTO_USUARIO);
            cmd.Parameters.AddWithValue("@ESTADO_CIVIL", registro.ESTADO_CIVIL);
            cmd.Parameters.AddWithValue("@TIENE_HERMANOS", registro.TIENE_HERMANOS);

            try
            {
                CnxUsuarios.Open();
                cmd.ExecuteNonQuery();


                List<Respuesta> Listarespuesta = new List<Respuesta>();
                Respuesta respuesta = new Respuesta
                {
                    Error = "No",
                    Mensaje = "Usuario Actualizado exitosamente"
                };
                Listarespuesta.Add(respuesta);

                return Json(Listarespuesta.AsEnumerable());
            }
            catch (SqlException e)
            {
                List<Respuesta> Listarespuesta = new List<Respuesta>();
                Respuesta respuesta = new Respuesta
                {
                    Error = "Si",
                    Mensaje = e.Message.ToString()
                };
                Listarespuesta.Add(respuesta);

                return Json(Listarespuesta.AsEnumerable());
            }
        }
        #endregion


        #region Get_Borrar
        /// <summary>
        /// Metodo BorrarUsuario de tipo delete
        /// </summary>
        /// <returns>Mensaje</returns>
        [HttpDelete]
        [EnableCors("CorsPruebaTecnica")]
        [Route("api/Usuarios/BorrarUsuario")]
        public JsonResult BorrarUsuario(Int32 CODIGO)
        {
            //Se asigna la Fecha y Hora Actual  
            string dateTime = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd hh:mm:ss");
            DateTime FechaActual = DateTime.ParseExact(createddate, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);

            SqlConnection CnxUsuarios = new SqlConnection(Connect["ConnectionStrings:Connect"]);

            using SqlCommand cmd = new SqlCommand
            {
                Connection = CnxUsuarios,
                CommandType = CommandType.Text,
                CommandText = @"DELETE FROM USUARIOS WHERE CODIGO = @CODIGO"
            };

            cmd.Parameters.AddWithValue("@CODIGO", CODIGO);

            try
            {
                CnxUsuarios.Open();
                cmd.ExecuteNonQuery();


                List<Respuesta> Listarespuesta = new List<Respuesta>();
                Respuesta respuesta = new Respuesta
                {
                    Error = "No",
                    Mensaje = "Usuario Borrado exitosamente"
                };
                Listarespuesta.Add(respuesta);

                return Json(Listarespuesta.AsEnumerable());
            }
            catch (SqlException e)
            {
                List<Respuesta> Listarespuesta = new List<Respuesta>();
                Respuesta respuesta = new Respuesta
                {
                    Error = "Si",
                    Mensaje = e.Message.ToString()
                };
                Listarespuesta.Add(respuesta);

                return Json(Listarespuesta.AsEnumerable());
            }
        }
        #endregion


    }
}
