using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    public class MedicamentoDAL
    {
        // TODO: Mover la cadena de conexión a un archivo de configuración (App.config)
        private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";

        public void Insertar(Medicamento med)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medicamento_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", med.Descripcion);
                    cmd.Parameters.AddWithValue("@TipoFarmacoID", med.TipoFarmacoID);
                    cmd.Parameters.AddWithValue("@MarcaID", med.MarcaID);
                    cmd.Parameters.AddWithValue("@UbicacionID", med.UbicacionID);
                    cmd.Parameters.AddWithValue("@Dosis", med.Dosis);
                    cmd.Parameters.AddWithValue("@Estado", med.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Medicamento med)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medicamento_Actualizar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", med.ID);
                    cmd.Parameters.AddWithValue("@Descripcion", med.Descripcion);
                    cmd.Parameters.AddWithValue("@TipoFarmacoID", med.TipoFarmacoID);
                    cmd.Parameters.AddWithValue("@MarcaID", med.MarcaID);
                    cmd.Parameters.AddWithValue("@UbicacionID", med.UbicacionID);
                    cmd.Parameters.AddWithValue("@Dosis", med.Dosis);
                    cmd.Parameters.AddWithValue("@Estado", med.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inactivar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medicamento_Inactivar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Medicamento> Listar()
        {
            List<Medicamento> lista = new List<Medicamento>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Medicamento_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Medicamento
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Descripcion = reader["Descripcion"].ToString(),
                                TipoFarmacoID = Convert.ToInt32(reader["TipoFarmacoID"]),
                                TipoFarmacoDescripcion = reader["TipoFarmaco"].ToString(),
                                MarcaID = Convert.ToInt32(reader["MarcaID"]),
                                MarcaDescripcion = reader["Marca"].ToString(),
                                UbicacionID = Convert.ToInt32(reader["UbicacionID"]),
                                UbicacionDescripcion = reader["Ubicacion"].ToString(),
                                Dosis = reader["Dosis"].ToString(),
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
                using (SqlCommand cmd = new SqlCommand("sp_Medicamento_Existe", con))
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
