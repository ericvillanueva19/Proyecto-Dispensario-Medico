using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DispensarioMedico.Entities;

namespace DispensarioMedico.DAL
{
    public class MarcaDAL
    {
        // TODO: Mover la cadena de conexión a un archivo de configuración (App.config)
        private readonly string connectionString = "Server=TU_SERVIDOR;Database=DispensarioMedico;Integrated Security=True;";

        public void Insertar(Marca marca)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Insertar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", marca.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Marca marca)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Actualizar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", marca.ID);
                    cmd.Parameters.AddWithValue("@Descripcion", marca.Descripcion);
                    cmd.Parameters.AddWithValue("@Estado", marca.Estado);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Inactivar(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Inactivar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Marca> Listar()
        {
            List<Marca> lista = new List<Marca>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Listar", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Marca
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
                using (SqlCommand cmd = new SqlCommand("sp_Marca_Existe", con))
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
