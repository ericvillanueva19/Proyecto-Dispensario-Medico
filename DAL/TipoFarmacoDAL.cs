using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    public class TipoFarmacoDAL
    {
        // TODO: Mover la cadena de conexión a un archivo de configuración (App.config)
        private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";

        public void Insertar(TipoFarmaco tipoFarmaco)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoFarmaco_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", tipoFarmaco.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", tipoFarmaco.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(TipoFarmaco tipoFarmaco)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoFarmaco_Actualizar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", tipoFarmaco.ID);
                    cmd.Parameters.AddWithValue("@Descripcion", tipoFarmaco.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", tipoFarmaco.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inactivar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoFarmaco_Inactivar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TipoFarmaco> Listar()
        {
            List<TipoFarmaco> lista = new List<TipoFarmaco>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TipoFarmaco_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new TipoFarmaco
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Descripcion = reader["Descripcion"].ToString(),
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
                using (SqlCommand cmd = new SqlCommand("sp_TipoFarmaco_Existe", con))
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
