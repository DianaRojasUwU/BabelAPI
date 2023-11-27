using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BabelAPI.Datos 
{
    public class DUsuario
    {
        ConexionBD cn = new ConexionBD(); // Instancia de la clase de conexión a la base de datos

        // Método para obtener un usuario por su ID (almacenado en un procedimiento almacenado)
        public async Task<MUsuario> MostrarUsuariosbyID(int id)
        {
            MUsuario musuario = null; // Variable para almacenar el usuario

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerUsuarioPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id }); // Asigna el valor del parámetro

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee el resultado y crea un objeto de MUsuario
                        if (await reader.ReadAsync())
                        {
                            musuario = new MUsuario
                            {
                                ID = (int)reader["ID"],
                                Nombre = (string)reader["Nombre"],
                                RolID = (int)reader["RolID"],
                                CorreoElectronico = (string)reader["CorreoElectronico"],
                                Contrasena = (string)reader["Contrasena"],
                                Rol = new MRol
                                {
                                    rolID = (int)reader["RolID"],  // Ajusta según el nombre de la columna
                                    rolNombre = (string)reader["rolNombre"]   // Asigna otras propiedades del rol desde el reader
                                }
                            };
                        }
                    }
                }
            }

            return musuario; // Retorna el usuario obtenido
        }

        // Método para obtener todos los usuarios (almacenados en un procedimiento almacenado)
        public async Task<List<MUsuario>> MostrarUsuarios()
        {
            List<MUsuario> usuarios = new List<MUsuario>(); // Lista para almacenar los usuarios

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerTodosLosUsuarios", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee los resultados y crea objetos de MUsuario
                        while (await reader.ReadAsync())
                        {
                            var usuario = new MUsuario
                            {
                                ID = (int)reader["ID"],
                                Nombre = (string)reader["Nombre"],
                                RolID = (int)reader["RolID"],
                                CorreoElectronico = (string)reader["CorreoElectronico"],
                                Contrasena = (string)reader["Contrasena"],
                                Rol = new MRol
                                {
                                    rolID = (int)reader["rolId"],  // Ajusta según el nombre de la columna
                                    rolNombre = (string)reader["rolNombre"]   // Asigna otras propiedades del rol desde el reader
                                }
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios; // Retorna la lista de usuarios obtenidos
        }

        // Método para insertar un nuevo usuario (almacenado en un procedimiento almacenado)
        public async Task<int> InsertarUsuario(string nombre, string correoElectronico, string contrasena, int rolID)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("InsertarUsuario", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre });
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", SqlDbType.NVarChar) { Value = correoElectronico });
                    cmd.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.NVarChar) { Value = contrasena });
                    cmd.Parameters.Add(new SqlParameter("@RolID", SqlDbType.Int) { Value = rolID });

                    // Ejecuta la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Cambia esto según tu necesidad
        }

        // Método para actualizar un usuario (almacenado en un procedimiento almacenado)
        public async Task ActualizarUsuario(MUsuario usuario)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ActualizarUsuario", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = usuario.ID });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", SqlDbType.NVarChar) { Value = usuario.CorreoElectronico });
                    cmd.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.NVarChar) { Value = usuario.Contrasena });
                    cmd.Parameters.Add(new SqlParameter("@RolID", SqlDbType.Int) { Value = usuario.RolID });

                    // Ejecuta la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para eliminar un usuario por su ID (almacenado en un procedimiento almacenado)
        public async Task EliminarUsuario(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("EliminarUsuario", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });

                    // Ejecuta la eliminación
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}

