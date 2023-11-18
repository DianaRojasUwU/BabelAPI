﻿using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Runtime.CompilerServices;

namespace BabelAPI.Datos
{
    public class DUsuario
    {
        ConexionBD cn = new ConexionBD();
        public async Task<MUsuario> MostrarUsuarios(int id)
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
                                    ID = (int)reader["RolID"],  // Ajusta según el nombre de la columna
                                    Nombre = (string)reader["Nombre"]   // Asigna otras propiedades del rol desde el reader
                                }
                            };
                        }
                    }
                }
            }

            return musuario;
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


    }
}