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
    public class ProveedorAzure
    {
        static string connectionString = @"Server=DESKTOP-Q5I4QJN\SQLEXPRESS01;Database=Jugueteria_Entrega3;Trusted_Connection=True;";

        private static List<Proveedor> proveedor;

        public static List<Proveedor> ObtenerProvedor()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from Proveedor";

                var comando = ConsultaSqlProveedor(connection, consultaSql);

                var dataTableProveedor = LlenarDataTable(comando);

                return LLenadoProveedor(dataTableProveedor);
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

        private static SqlCommand ConsultaSqlProveedor(SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }

        private static List<Proveedor> LLenadoProveedor(DataTable dataTable)
        {
            proveedor = new List<Proveedor>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Proveedor provedor = new Proveedor();
                provedor.id_proveedor = int.Parse(dataTable.Rows[i]["id_proveedor"].ToString());
                provedor.rut = dataTable.Rows[i]["rut"].ToString();
                provedor.nombre = dataTable.Rows[i]["nombre"].ToString();
                provedor.apellido = dataTable.Rows[i]["apellido"].ToString();
                provedor.fono = dataTable.Rows[i]["fono"].ToString();
                provedor.direccion = dataTable.Rows[i]["direccion"].ToString();
                proveedor.Add(provedor);
            }
            return proveedor;
        }

        private static Proveedor CreacionProveedor(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Proveedor provedor = new Proveedor();
                provedor.id_proveedor = int.Parse(dataTable.Rows[0]["id_proveedor"].ToString());
                provedor.rut = dataTable.Rows[0]["rut"].ToString();
                provedor.nombre = dataTable.Rows[0]["nombre"].ToString();
                provedor.apellido = dataTable.Rows[0]["apellido"].ToString();
                provedor.fono = dataTable.Rows[0]["fono"].ToString();
                provedor.direccion = dataTable.Rows[0]["direccion"].ToString();
                return provedor;
            }
            else
            {
                return null;
            }
        }

        public static Proveedor ObtenerProveedorporID(int id_proveedor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Proveedor where id_proveedor = {id_proveedor}";

                var comando = ConsultaSqlProveedor(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionProveedor(dataTable);
            }
        }

        public static int AgregarProvedor(Proveedor provedor)
        {
            int filasAfectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Proveedor (rut,nombre,apellido,fono,direccion) values (@rut,@nombre,@apellido,@fono,@direccion)";
                sqlCommand.Parameters.AddWithValue("@rut", provedor.rut);
                sqlCommand.Parameters.AddWithValue("@nombre", provedor.nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", provedor.apellido);
                sqlCommand.Parameters.AddWithValue("@fono", provedor.fono);
                sqlCommand.Parameters.AddWithValue("@direccion", provedor.direccion);

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

        public static int AgregarProvedor(string rut, string nombre, string apellido, string fono, string direccion)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Proveedor (rut,nombre,apellido,fono,direccion) values (@rut,@nombre,@apellido,@fono,@direccion)";
                sqlCommand.Parameters.AddWithValue("@rut", rut);
                sqlCommand.Parameters.AddWithValue("@nombre", nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", apellido);
                sqlCommand.Parameters.AddWithValue("@fono", fono);
                sqlCommand.Parameters.AddWithValue("@direccion", direccion);
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

        public static int EliminarProveedorPorRut(string rut)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Delete from Proveedor where rut=@rut";
                sqlCommand.Parameters.AddWithValue("@rut", rut);

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

        public static int ActualizarProvedorID(Proveedor provedor)
        {
            int resultado = 0;
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Proveedor SET rut = @rut, nombre = @nombre,apellido = @apellido, fono=@fono, direccion = @direccion where id_proveedor = @id_proveedor";
                sqlCommand.Parameters.AddWithValue("@rut", provedor.rut);
                sqlCommand.Parameters.AddWithValue("@nombre", provedor.nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", provedor.apellido);
                sqlCommand.Parameters.AddWithValue("@fono", provedor.fono);
                sqlCommand.Parameters.AddWithValue("@direccion", provedor.direccion);
                sqlCommand.Parameters.AddWithValue("@id_proveedor", provedor.id_proveedor);


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

