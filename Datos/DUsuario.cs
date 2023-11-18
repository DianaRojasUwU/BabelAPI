using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace BabelAPI.Datos
{
    public class DUsuario
    {
        ConexionBD cn = new ConexionBD();
        public async Task<MUsuario> MostrarUsuariosbyID(int id)
        {
            MUsuario musuario = null;

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerUsuarioPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    cmd.Parameters["@id"].Value = id;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
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

            return musuario;
        }
        public async Task<List<MUsuario>> MostrarUsuarios()
        {
            List<MUsuario> usuarios = new List<MUsuario>();

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerTodosLosUsuarios", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
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
                                    rolID= (int)reader["rolId"],  // Ajusta según el nombre de la columna
                                    rolNombre = (string)reader["rolNombre"]   // Asigna otras propiedades del rol desde el reader
                                }
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }

            return usuarios;
        }
        public async Task<int> InsertarUsuario(string nombre, string correoElectronico, string contrasena, int rolID)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("InsertarUsuario", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = nombre });
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", SqlDbType.NVarChar) { Value = correoElectronico });
                    cmd.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.NVarChar) { Value = contrasena });
                    cmd.Parameters.Add(new SqlParameter("@RolID", SqlDbType.Int) { Value = rolID });

                    // Ejecutar la inserción
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return 0; // Cambia esto según tu necesidad
        }
        public async Task ActualizarUsuario(MUsuario usuario)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ActualizarUsuario", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = usuario.ID });
                    cmd.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.NVarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", SqlDbType.NVarChar) { Value = usuario.CorreoElectronico });
                    cmd.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.NVarChar) { Value = usuario.Contrasena });
                    cmd.Parameters.Add(new SqlParameter("@RolID", SqlDbType.Int) { Value = usuario.RolID });

                    // Ejecutar la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EliminarUsuario(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("EliminarUsuario", sql))
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
