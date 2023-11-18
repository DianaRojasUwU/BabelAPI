using BabelAPI.Connection;
using BabelAPI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BabelAPI.Datos
{
    public class DEventoNoticia
    {
        ConexionBD cn = new ConexionBD();

        public async Task<List<MEventoNoticia>> ObtenerTodosLosEventosNoticias()
        {
            List<MEventoNoticia> eventosNoticias = new List<MEventoNoticia>();

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerTodosLosEventosNoticias", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var eventoNoticia = new MEventoNoticia
                            {
                                ID = (int)reader["ID"],
                                Titulo = (string)reader["Titulo"],
                                Descripcion = (string)reader["Descripcion"],
                                FechaInicio = (DateTime)reader["FechaInicio"],
                                FechaFin = (DateTime)reader["FechaFin"],
                                Ubicacion = (string)reader["Ubicacion"],
                                Imagen = (string)reader["Imagen"],
                                Enlace = (string)reader["Enlace"]
                            };
                            eventosNoticias.Add(eventoNoticia);
                        }
                    }
                }
            }

            return eventosNoticias;
        }

        public async Task<MEventoNoticia> ObtenerEventoNoticiaPorId(int id)
        {
            MEventoNoticia eventoNoticia = null;

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ObtenerEventoNoticiaPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            eventoNoticia = new MEventoNoticia
                            {
                                ID = (int)reader["ID"],
                                Titulo = (string)reader["Titulo"],
                                Descripcion = (string)reader["Descripcion"],
                                FechaInicio = (DateTime)reader["FechaInicio"],
                                FechaFin = (DateTime)reader["FechaFin"],
                                Ubicacion = (string)reader["Ubicacion"],
                                Imagen = (string)reader["Imagen"],
                                Enlace = (string)reader["Enlace"]
                            };
                        }
                    }
                }
            }

            return eventoNoticia;
        }

        public async Task<int> InsertarEventoNoticia(MEventoNoticia eventoNoticia)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("InsertarEventoNoticia", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = eventoNoticia.Titulo });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = eventoNoticia.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime) { Value = eventoNoticia.FechaInicio });
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime) { Value = eventoNoticia.FechaFin });
                    cmd.Parameters.Add(new SqlParameter("@Ubicacion", SqlDbType.NVarChar) { Value = eventoNoticia.Ubicacion });
                    cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.NVarChar) { Value = eventoNoticia.Imagen });
                    cmd.Parameters.Add(new SqlParameter("@Enlace", SqlDbType.NVarChar) { Value = eventoNoticia.Enlace });

                    // Agrega el parámetro de salida
                    var nuevoEventoNoticiaIDParam = new SqlParameter("@NuevoEventoNoticiaID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(nuevoEventoNoticiaIDParam);

                    // Ejecutar la inserción
                    await cmd.ExecuteNonQueryAsync();

                    // Obtén el valor del parámetro de salida
                    return (int)nuevoEventoNoticiaIDParam.Value;
                }
            }
        }


        public async Task ActualizarEventoNoticia(MEventoNoticia eventoNoticia)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("ActualizarEventoNoticia", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = eventoNoticia.ID });
                    cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = eventoNoticia.Titulo });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = eventoNoticia.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime) { Value = eventoNoticia.FechaInicio });
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime) { Value = eventoNoticia.FechaFin });
                    cmd.Parameters.Add(new SqlParameter("@Ubicacion", SqlDbType.NVarChar) { Value = eventoNoticia.Ubicacion });
                    cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.NVarChar) { Value = eventoNoticia.Imagen });
                    cmd.Parameters.Add(new SqlParameter("@Enlace", SqlDbType.NVarChar) { Value = eventoNoticia.Enlace });

                    // Ejecutar la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EliminarEventoNoticia(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync();

                using (var cmd = new SqlCommand("EliminarEventoNoticia", sql))
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
