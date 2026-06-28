using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    public class PacienteDAL
    {
        // TODO: Mover la cadena de conexión a un archivo de configuración (App.config)
        private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";

        public void Insertar(Paciente paciente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Paciente_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    cmd.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                    cmd.Parameters.AddWithValue("@No_Carnet", paciente.No_Carnet);
                    cmd.Parameters.AddWithValue("@TipoPaciente", paciente.TipoPaciente);
                    cmd.Parameters.AddWithValue("@Estado", paciente.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Paciente paciente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Paciente_Actualizar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", paciente.ID);
                    cmd.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                    cmd.Parameters.AddWithValue("@Cedula", paciente.Cedula);
                    cmd.Parameters.AddWithValue("@No_Carnet", paciente.No_Carnet);
                    cmd.Parameters.AddWithValue("@TipoPaciente", paciente.TipoPaciente);
                    cmd.Parameters.AddWithValue("@Estado", paciente.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inactivar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Paciente_Inactivar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Paciente> Listar()
        {
            List<Paciente> lista = new List<Paciente>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Paciente_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Paciente
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nombre = reader["Nombre"].ToString(),
                                Cedula = reader["Cedula"].ToString(),
                                No_Carnet = reader["No_Carnet"].ToString(),
                                TipoPaciente = reader["TipoPaciente"].ToString(),
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
                using (SqlCommand cmd = new SqlCommand("sp_Paciente_ExisteCedula", con))
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
