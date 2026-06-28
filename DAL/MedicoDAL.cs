using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    public class MedicoDAL
    {
        // TODO: Mover la cadena de conexión a un archivo de configuración (App.config)
        private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";

        public void Insertar(Medico medico)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medico_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", medico.Nombre);
                    cmd.Parameters.AddWithValue("@Cedula", medico.Cedula);
                    cmd.Parameters.AddWithValue("@Tanda_Labor", medico.Tanda_Labor);
                    cmd.Parameters.AddWithValue("@Especialidad", medico.Especialidad);
                    cmd.Parameters.AddWithValue("@Estado", medico.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Medico medico)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medico_Actualizar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", medico.ID);
                    cmd.Parameters.AddWithValue("@Nombre", medico.Nombre);
                    cmd.Parameters.AddWithValue("@Cedula", medico.Cedula);
                    cmd.Parameters.AddWithValue("@Tanda_Labor", medico.Tanda_Labor);
                    cmd.Parameters.AddWithValue("@Especialidad", medico.Especialidad);
                    cmd.Parameters.AddWithValue("@Estado", medico.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inactivar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medico_Inactivar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Medico> Listar()
        {
            List<Medico> lista = new List<Medico>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medico_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Medico
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Cedula = reader["Cedula"].ToString(),
                                Tanda_Labor = reader["Tanda_Labor"].ToString(),
                                Especialidad = reader["Especialidad"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public bool ExisteCedula(string cedula, int id = 0)
        {
            int count = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medico_ExisteCedula", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cedula", cedula);
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return count > 0;
        }
    }
}
