using BabelAPI.Connection;
using BabelAPI.Models; 
using Microsoft.Data.SqlClient; 
using System.Data;

namespace BabelAPI.Datos 
{
    public class DCategoria
    {
        ConexionBD cn = new ConexionBD(); // Instancia de la clase de conexión a la base de datos

        // Método para obtener todas las categorías (almacenadas en un procedimiento almacenado)
        public async Task<List<MCategoria>> ObtenerTodasLasCategorias()
        {
            List<MCategoria> categorias = new List<MCategoria>(); // Lista para almacenar las categorías

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerTodasLasCategorias", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee los resultados y crea objetos de MCategoria
                        while (await reader.ReadAsync())
                        {
                            var categoria = new MCategoria
                            {
                                ID = (int)reader["ID"],
                                Nombre = (string)reader["Nombre"],
                            };
                            categorias.Add(categoria);
                        }
                    }
                }
            }

            return categorias; // Retorna la lista de categorías obtenidas
        }

        // Método para obtener una categoría por su ID (almacenada en un procedimiento almacenado)
        public async Task<MCategoria> ObtenerCategoriaPorId(int id)
        {
            MCategoria categoria = null; // Variable para almacenar la categoría

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerCategoriaPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id; // Asigna el valor del parámetro

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee el resultado y crea un objeto de MCategoria
                        if (await reader.ReadAsync())
                        {
                            categoria = new MCategoria
                            {
                                ID = (int)reader["ID"],
                                Nombre = (string)reader["Nombre"],
                            };
                        }
                    }
                }
            }

            return categoria; // Retorna la categoría obtenida
        }

        // Método para insertar una nueva categoría (almacenada en un procedimiento almacenado)
        public async Task<int> InsertarCategoria(string nombre)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("InsertarCategoria", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre });

                    // Ejecuta la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Retorna un valor predeterminado, ajusta según tus necesidades
        }

        // Método para actualizar una categoría (almacenada en un procedimiento almacenado)
        public async Task ActualizarCategoria(MCategoria categoria)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ActualizarCategoria", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = categoria.ID });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = categoria.Nombre });

                    // Ejecuta la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para eliminar una categoría por su ID (almacenada en un procedimiento almacenado)
        public async Task EliminarCategoria(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("EliminarCategoria", sql))
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
