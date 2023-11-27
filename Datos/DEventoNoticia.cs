using BabelAPI.Connection;
using BabelAPI.Models; 
using Microsoft.Data.SqlClient;
using System.Data;

namespace BabelAPI.Datos 
{
    public class DEventoNoticia
    {
        ConexionBD cn = new ConexionBD(); // Instancia de la clase de conexión a la base de datos

        // Método para obtener todos los eventos y noticias (almacenados en un procedimiento almacenado)
        public async Task<List<MEventoNoticia>> ObtenerTodosLosEventosNoticias()
        {
            List<MEventoNoticia> eventosNoticias = new List<MEventoNoticia>(); // Lista para almacenar los eventos y noticias

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerTodosLosEventosNoticias", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee los resultados y crea objetos de MEventoNoticia
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

            return eventosNoticias; // Retorna la lista de eventos y noticias obtenidos
        }

        // Método para obtener un evento o noticia por su ID (almacenado en un procedimiento almacenado)
        public async Task<MEventoNoticia> ObtenerEventoNoticiaPorId(int id)
        {
            MEventoNoticia eventoNoticia = null; // Variable para almacenar el evento o noticia

            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ObtenerEventoNoticiaPorId", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id }); // Asigna el valor del parámetro

                    // Ejecuta el comando y obtiene los resultados
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        // Lee el resultado y crea un objeto de MEventoNoticia
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

            return eventoNoticia; // Retorna el evento o noticia obtenido
        }

        // Método para insertar un nuevo evento o noticia (almacenado en un procedimiento almacenado)
        public async Task<int> InsertarEventoNoticia(MEventoNoticia eventoNoticia)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("InsertarEventoNoticia", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
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

                    // Ejecuta la inserción
                    await cmd.ExecuteNonQueryAsync();

                    // Obtén el valor del parámetro de salida
                    return (int)nuevoEventoNoticiaIDParam.Value;
                }
            }
        }

        // Método para actualizar un evento o noticia (almacenado en un procedimiento almacenado)
        public async Task ActualizarEventoNoticia(MEventoNoticia eventoNoticia)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("ActualizarEventoNoticia", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // Establece el tipo de comando como procedimiento almacenado
                    cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = eventoNoticia.ID });
                    cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = eventoNoticia.Titulo });
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.NVarChar) { Value = eventoNoticia.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", SqlDbType.DateTime) { Value = eventoNoticia.FechaInicio });
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", SqlDbType.DateTime) { Value = eventoNoticia.FechaFin });
                    cmd.Parameters.Add(new SqlParameter("@Ubicacion", SqlDbType.NVarChar) { Value = eventoNoticia.Ubicacion });
                    cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.NVarChar) { Value = eventoNoticia.Imagen });
                    cmd.Parameters.Add(new SqlParameter("@Enlace", SqlDbType.NVarChar) { Value = eventoNoticia.Enlace });

                    // Ejecuta la actualización
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // Método para eliminar un evento o noticia por su ID (almacenado en un procedimiento almacenado)
        public async Task EliminarEventoNoticia(int id)
        {
            using (var sql = new SqlConnection(cn.ConnectionString()))
            {
                await sql.OpenAsync(); // Abre la conexión a la base de datos

                using (var cmd = new SqlCommand("EliminarEventoNoticia", sql))
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
