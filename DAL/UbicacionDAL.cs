using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    public class UbicacionDAL
    {
        // TODO: Mover la cadena de conexión a un archivo de configuración (App.config)
        private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";

        public void Insertar(Ubicacion ubicacion)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Ubicacion_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", ubicacion.Descripcion);
                    cmd.Parameters.AddWithValue("@Estante", ubicacion.Estante);
                    cmd.Parameters.AddWithValue("@Tramo", ubicacion.Tramo);
                    cmd.Parameters.AddWithValue("@Celda", ubicacion.Celda);
                    cmd.Parameters.AddWithValue("@Estado", ubicacion.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Ubicacion ubicacion)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Ubicacion_Actualizar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", ubicacion.ID);
                    cmd.Parameters.AddWithValue("@Descripcion", ubicacion.Descripcion);
                    cmd.Parameters.AddWithValue("@Estante", ubicacion.Estante);
                    cmd.Parameters.AddWithValue("@Tramo", ubicacion.Tramo);
                    cmd.Parameters.AddWithValue("@Celda", ubicacion.Celda);
                    cmd.Parameters.AddWithValue("@Estado", ubicacion.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inactivar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Ubicacion_Inactivar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Ubicacion> Listar()
        {
            List<Ubicacion> lista = new List<Ubicacion>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Ubicacion_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Ubicacion
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                Estante = reader["Estante"].ToString(),
                                Tramo = reader["Tramo"].ToString(),
                                Celda = reader["Celda"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool Existe(string descripcion, int id = 0)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Ubicacion_Existe", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count > 0;
        }
    }
}
