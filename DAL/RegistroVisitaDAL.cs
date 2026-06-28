using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    public class RegistroVisitaDAL
    {
        // TODO: Mover a App.config
        private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";

        public void Insertar(RegistroVisita visita)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegistroVisita_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MedicoID", visita.MedicoID);
                    cmd.Parameters.AddWithValue("@PacienteID", visita.PacienteID);
                    cmd.Parameters.AddWithValue("@Fecha_Visita", visita.Fecha_Visita);
                    cmd.Parameters.AddWithValue("@Hora_Visita", visita.Hora_Visita);
                    cmd.Parameters.AddWithValue("@Sintomas", visita.Sintomas);
                    cmd.Parameters.AddWithValue("@MedicamentoID", visita.MedicamentoID);
                    cmd.Parameters.AddWithValue("@Recomendaciones", visita.Recomendaciones);
                    cmd.Parameters.AddWithValue("@Estado", visita.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<RegistroVisita> Consultar(int? medicoID, int? pacienteID, DateTime? fecha)
        {
            List<RegistroVisita> lista = new List<RegistroVisita>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegistroVisita_Consultar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Manejo de parámetros nulos para consulta dinámica
                    cmd.Parameters.AddWithValue("@MedicoID", (object)medicoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PacienteID", (object)pacienteID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fecha", (object)fecha ?? DBNull.Value);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new RegistroVisita
                            {
                                ID_Visita = Convert.ToInt32(reader["ID_Visita"]),
                                MedicoID = Convert.ToInt32(reader["MedicoID"]),
                                MedicoNombre = reader["MedicoNombre"].ToString(),
                                PacienteID = Convert.ToInt32(reader["PacienteID"]),
                                PacienteNombre = reader["PacienteNombre"].ToString(),
                                Fecha_Visita = Convert.ToDateTime(reader["Fecha_Visita"]),
                                Hora_Visita = (TimeSpan)reader["Hora_Visita"],
                                Sintomas = reader["Sintomas"].ToString(),
                                MedicamentoID = Convert.ToInt32(reader["MedicamentoID"]),
                                MedicamentoDescripcion = reader["MedicamentoDescripcion"].ToString(),
                                Recomendaciones = reader["Recomendaciones"].ToString(),
                                Estado = Convert.ToBoolean(reader["Estado"])
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}
