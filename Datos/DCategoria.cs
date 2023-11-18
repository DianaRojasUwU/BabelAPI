using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BabelAPI.Datos
{
    public class DCategoria
    {
        ConexionBD cn = new ConexionBD();

        public async Task<List<MCategoria>> ObtenerTodasLasCategorias()
        {
            List<MCategoria> categorias = new List<MCategoria>();

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerTodasLasCategorias", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
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

            return categorias;
        }

        public async Task<MCategoria> ObtenerCategoriaPorId(int id)
        {
            MCategoria categoria = null;

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerCategoriaPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
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

            return categoria;
        }

        public async Task<int> InsertarCategoria(string nombre)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("InsertarCategoria", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre });

                    // Ejecutar la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Cambia esto según tu necesidad
        }

        public async Task ActualizarCategoria(MCategoria categoria)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ActualizarCategoria", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = categoria.ID });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = categoria.Nombre });

                    // Ejecutar la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EliminarCategoria(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("EliminarCategoria", sql))
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
