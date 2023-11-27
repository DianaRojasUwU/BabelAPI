using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient; 
using System.Data;

namespace BabelAPI.Datos 
{
    public class DRol
    {
        ConexionBD cn = new ConexionBD(); // Instancia de la clase de conexión a la base de datos

        // Método para obtener todos los roles (almacenados en un procedimiento almacenado)
        public async Task<List<MRol>> ObtenerTodosLosRoles()
        {
            List<MRol> roles = new List<MRol>(); // Lista para almacenar los roles

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerTodosLosRoles", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee los resultados y crea objetos de MRol
                        while (await reader.ReadAsync())
                        {
                            var rol = new MRol
                            {
                                rolID = (int)reader["rolID"],
                                rolNombre = (string)reader["rolNombre"]
                            };
                            roles.Add(rol);
                        }
                    }
                }
            }

            return roles; // Retorna la lista de roles obtenidos
        }

        // Método para obtener un rol por su ID (almacenado en un procedimiento almacenado)
        public async Task<MRol> ObtenerRolPorId(int id)
        {
            MRol rol = null; // Variable para almacenar el rol

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerRolPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id }); // Asigna el valor del parámetro

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee el resultado y crea un objeto de MRol
                        if (await reader.ReadAsync())
                        {
                            rol = new MRol
                            {
                                rolID = (int)reader["rolID"],
                                rolNombre = (string)reader["rolNombre"]
                            };
                        }
                    }
                }
            }

            return rol; // Retorna el rol obtenido
        }

        // Método para insertar un nuevo rol (almacenado en un procedimiento almacenado)
        public async Task<int> InsertarRol(string nombre)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("InsertarRol", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre });

                    // Ejecuta la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Cambia esto según tu necesidad
        }

        // Método para actualizar un rol (almacenado en un procedimiento almacenado)
        public async Task ActualizarRol(MRol rol)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ActualizarRol", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@rolID", SqlDbType.Int) { Value = rol.rolID });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = rol.rolNombre });

                    // Ejecuta la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para eliminar un rol por su ID (almacenado en un procedimiento almacenado)
        public async Task EliminarRol(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("EliminarRol", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@rolID", SqlDbType.Int) { Value = id }); // Asigna el valor del parámetro

                    // Ejecuta la eliminación
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
