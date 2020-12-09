using Jugueteria_Prueba3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Jugueteria_Prueba3.Azure
{
    public class JugueteAzure
    {
        static string connectionString = @"Server=DESKTOP-Q5I4QJN\SQLEXPRESS01;Database=Jugueteria_Entrega3;Trusted_Connection=True;";

        private static List<Juguete> juguetes;

        public static List<Juguete> ObtenerJuguete()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from Juguete";

                var comando = ConsultaSqlJuguete(connection, consultaSql);

                var dataTableProveedor = LlenarDataTable(comando);

                return LLenadoJuguete(dataTableProveedor);
            }
        }

        public static Juguete ObtenerJuguetePorId(int idProbar)
        {
            throw new NotImplementedException();
        }

        public static Juguete ObtenerJugueteporID(int id_juguete)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Juguete where id_juguete = {id_juguete}";

                var comando = ConsultaSqlJuguete(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionJuguete(dataTable);
            }
        }


        public static Juguete ObtenerJuguetePorNombre(string nombre)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Juguete where nombre = '{nombre}'";

                var comando = ConsultaSqlJuguete(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionJuguete(dataTable);

            }
        }

        private static DataTable LlenarDataTable(SqlCommand comando)
        {
            //2. llenamos el dataTable(conversion)
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        private static SqlCommand ConsultaSqlJuguete(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }

        private static List<Juguete> LLenadoJuguete(DataTable dataTable)
        {
            juguetes= new List<Juguete>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Juguete juguete = new Juguete();
                juguete.id_juguete = int.Parse(dataTable.Rows[i]["id_juguete"].ToString());
                juguete.nombre = dataTable.Rows[i]["nombre"].ToString();
                juguete.marca = dataTable.Rows[i]["marca"].ToString();
                juguete.precioUnit = int.Parse(dataTable.Rows[i]["precioUnit"].ToString());
                juguetes.Add(juguete);
            }
            return juguetes;
        }

        private static Juguete CreacionJuguete(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Juguete juguete = new Juguete();
                juguete.id_juguete = int.Parse(dataTable.Rows[0]["id_juguete"].ToString());
                juguete.nombre = dataTable.Rows[0]["nombre"].ToString();
                juguete.marca = dataTable.Rows[0]["marca"].ToString();
                juguete.precioUnit = int.Parse(dataTable.Rows[0]["precioUnit"].ToString());
                return juguete;
            }
            else
            {
                return null;
            }
        }

        public static int AgregarJuguete(Juguete juguete)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Juguete (nombre,marca,precioUnit) values (@nombre,@marca,@precioUnit)";
                sqlCommand.Parameters.AddWithValue("@nombre", juguete.nombre);
                sqlCommand.Parameters.AddWithValue("@marca", juguete.nombre);
                sqlCommand.Parameters.AddWithValue("@precioUnit", juguete.precioUnit);

                try
                {
                    connection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
            return filasAfectadas;
        }

        public static int AgregarJuguete(string nombre, string marca, int precioUnit)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Juguete (nombre,marca,precioUnit) values (@nombre,@marca,@precioUnit)";
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@marca", marca);
                sqlCommand.Parameters.AddWithValue("@precioUnit", precioUnit);
                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return resultado;
        }

        public static int EliminarJugueteporId(int id_juguete)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from Juguete where id_juguete=@id_juguete";
                sqlCommand.Parameters.AddWithValue("@id_juguete", id_juguete);

                try
                {
                    connection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return resultado;
            }
        }

        public static int ActualizarJugueteId(Juguete juguete)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Juguete SET nombre = @nombre,marca = @marca, precioUnit=@precioUnit where id_juguete = @id_juguete";

                sqlCommand.Parameters.AddWithValue("@nombre", juguete.nombre);
                sqlCommand.Parameters.AddWithValue("@marca", juguete.marca);
                sqlCommand.Parameters.AddWithValue("@precioUnit", juguete.precioUnit);
                sqlCommand.Parameters.AddWithValue("@id_juguete", juguete.id_juguete);

                try
                {
                    sqlConnection.Open();
                    resultado = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return resultado;
        }


    }
}
