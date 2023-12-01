using BabelAPI.Connection; 
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data; 

namespace BabelAPI.Datos 
{
    public class DLibro
    {
        ConexionBD cn = new ConexionBD(); // Instancia de la clase de conexión a la base de datos

        // Método para obtener todos los libros (almacenados en un procedimiento almacenado)
        public async Task<List<MLibro>> ObtenerTodosLosLibros()
        {
            List<MLibro> libros = new List<MLibro>(); // Lista para almacenar los libros

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerTodosLosLibros", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee los resultados y crea objetos de MLibro
                        while (await reader.ReadAsync())
                        {
                            var libro = new MLibro
                            {
                                ID = (int)reader["ID"],
                                Titulo = (string)reader["Titulo"],
                                Autor = (string)reader["Autor"],
                                Descripcion = (string)reader["Descripcion"],
                                Imagen = (string)reader["Imagen"],
                                Precio = (decimal)reader["Precio"],
                                Stock = (int)reader["Stock"],
                                CategoriaID = (int)reader["CategoriaID"],
                                Categoria = new MCategoria
                                {
                                    ID = (int)reader["CategoriaID"],
                                    Nombre = (string)reader["CategoriaNombre"]
                                }
                            };
                            libros.Add(libro);
                        }
                    }
                }
            }

            return libros; // Retorna la lista de libros obtenidos
        }

        // Método para obtener un libro por su ID (almacenado en un procedimiento almacenado)
        public async Task<MLibro> ObtenerLibroPorId(int id)
        {
            MLibro libro = null; // Variable para almacenar el libro

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerLibroPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id }); // Asigna el valor del parámetro

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee el resultado y crea un objeto de MLibro
                        if (await reader.ReadAsync())
                        {
                            libro = new MLibro
                            {
                                ID = (int)reader["ID"],
                                Titulo = (string)reader["Titulo"],
                                Autor = (string)reader["Autor"],
                                Descripcion = (string)reader["Descripcion"],
                                Imagen = (string)reader["Imagen"],
                                Precio = (decimal)reader["Precio"],
                                Stock = (int)reader["Stock"],
                                CategoriaID = (int)reader["CategoriaID"],
                                Categoria = new MCategoria
                                {
                                    ID = (int)reader["CategoriaID"],
                                    Nombre = (string)reader["CategoriaNombre"]
                                }
                            };
                        }
                    }
                }
            }

            return libro; // Retorna el libro obtenido
        }

        // Método para insertar un nuevo libro (almacenado en un procedimiento almacenado)
        public async Task<int> InsertarLibro(string titulo, string autor, string descripcion, string imagen, decimal precio, int stock, int categoriaID)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("InsertarLibro", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = titulo });
                    cmd.Parameters.Add(new SqlParameter("@Autor", SqlDbType.NVarChar) { Value = autor });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = descripcion });
                    cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.NVarChar) { Value = imagen});
                    cmd.Parameters.Add(new SqlParameter("@Precio", SqlDbType.Decimal) { Value = precio });
                    cmd.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = stock });
                    cmd.Parameters.Add(new SqlParameter("@CategoriaID", SqlDbType.Int) { Value = categoriaID });

                    // Ejecuta la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Cambia esto según tu necesidad
        }

        // Método para actualizar un libro (almacenado en un procedimiento almacenado)
        public async Task ActualizarLibro(MLibro libro)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ActualizarLibro", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = libro.ID });
                    cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = libro.Titulo });
                    cmd.Parameters.Add(new SqlParameter("@Autor", SqlDbType.NVarChar) { Value = libro.Autor });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = libro.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.NVarChar) { Value = libro.Imagen });
                    cmd.Parameters.Add(new SqlParameter("@Precio", SqlDbType.Decimal) { Value = libro.Precio });
                    cmd.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = libro.Stock });
                    cmd.Parameters.Add(new SqlParameter("@CategoriaID", SqlDbType.Int) { Value = libro.CategoriaID });

                    // Ejecuta la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para eliminar un libro por su ID (almacenado en un procedimiento almacenado)
        public async Task EliminarLibro(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("EliminarLibro", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id }); // Asigna el valor del parámetro

                    // Ejecuta la eliminación
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
