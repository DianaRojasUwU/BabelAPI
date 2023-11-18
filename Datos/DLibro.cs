using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BabelAPI.Datos
{
    public class DLibro
    {
        ConexionBD cn = new ConexionBD();

        public async Task<List<MLibro>> ObtenerTodosLosLibros()
        {
            List<MLibro> libros = new List<MLibro>();

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerTodosLosLibros", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var libro = new MLibro
                            {
                                ID = (int)reader["ID"],
                                Titulo = (string)reader["Titulo"],
                                Autor = (string)reader["Autor"],                                
                                Descripcion = (string)reader["Descripcion"],
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

            return libros;
        }

        public async Task<MLibro> ObtenerLibroPorId(int id)
        {
            MLibro libro = null;

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerLibroPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            libro = new MLibro
                            {
                                ID = (int)reader["ID"],
                                Titulo = (string)reader["Titulo"],
                                Autor = (string)reader["Autor"],                                
                                Descripcion = (string)reader["Descripcion"],
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

            return libro;
        }

        public async Task<int> InsertarLibro(string titulo, string autor, string descripcion, decimal precio, int stock, int categoriaID)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("InsertarLibro", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = titulo });
                    cmd.Parameters.Add(new SqlParameter("@Autor", SqlDbType.NVarChar) { Value = autor });                    
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = descripcion });
                    cmd.Parameters.Add(new SqlParameter("@Precio", SqlDbType.Decimal) { Value = precio });
                    cmd.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = stock });
                    cmd.Parameters.Add(new SqlParameter("@CategoriaID", SqlDbType.Int) { Value = categoriaID });

                    // Ejecutar la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Cambia esto según tu necesidad
        }

        public async Task ActualizarLibro(MLibro libro)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ActualizarLibro", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = libro.ID });
                    cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = libro.Titulo });
                    cmd.Parameters.Add(new SqlParameter("@Autor", SqlDbType.NVarChar) { Value = libro.Autor });                    
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = libro.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("@Precio", SqlDbType.Decimal) { Value = libro.Precio });
                    cmd.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int) { Value = libro.Stock });
                    cmd.Parameters.Add(new SqlParameter("@CategoriaID", SqlDbType.Int) { Value = libro.CategoriaID });

                    // Ejecutar la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EliminarLibro(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("EliminarLibro", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });

                    // Ejecutar la eliminación
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
