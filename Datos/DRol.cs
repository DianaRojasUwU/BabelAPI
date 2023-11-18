// DRol.cs

using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BabelAPI.Datos
{
    public class DRol
    {
        ConexionBD cn = new ConexionBD();

        public async Task<List<MRol>> ObtenerTodosLosRoles()
        {
            List<MRol> roles = new List<MRol>();

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerTodosLosRoles", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
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

            return roles;
        }

        public async Task<MRol> ObtenerRolPorId(int id)
        {
            MRol rol = null;

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerRolPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
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

            return rol;
        }

        public async Task<int> InsertarRol(string nombre)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("InsertarRol", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre });

                    // Ejecutar la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Cambia esto según tu necesidad
        }

        public async Task ActualizarRol(MRol rol)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ActualizarRol", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@rolID", SqlDbType.Int) { Value = rol.rolID });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = rol.rolNombre });

                    // Ejecutar la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EliminarRol(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("EliminarRol", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@rolID", SqlDbType.Int) { Value = id });

                    // Ejecutar la eliminación
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
