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
    public class UsuarioAzure
    {
        static string connectionString = @"Server=DESKTOP-Q5I4QJN\SQLEXPRESS01;Database=Jugueteria_Entrega3;Trusted_Connection=True;";

        private static List<Usuario> usuario;

        public static List<Usuario> ObtenerUsuario()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = "select * from Usuario";

                var comando = ConsultaSqlUsuario(connection, consultaSql);

                var dataTablePlantas = LlenarDataTable(comando);

                return LLenadoUsuarios(dataTablePlantas);
            }
        }


        public static Usuario ObtenerUSuarioporID(String id_usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Usuario where id_usuario = {id_usuario}";

                var comando = ConsultaSqlUsuario(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionUsuarios(dataTable);
            }
        }

        public static Usuario ObtenerUsuarioPorRut(string rut)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var consultaSql = $"select * from Usuario where rut = '{rut}'";

                var comando = ConsultaSqlUsuario(connection, consultaSql);

                var dataTable = LlenarDataTable(comando);

                return CreacionUsuarios(dataTable);

            }
        }

        private static SqlCommand ConsultaSqlUsuario (SqlConnection connection, string consulta)
        {
            SqlCommand sqlCommand = new SqlCommand(null, connection);
            sqlCommand.CommandText = consulta;
            connection.Open();
            return sqlCommand;
        }

        public static int AgregarUsuario(string rut, string nombre, string apellido, string fono, string direccion)
        {
            int resultado = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Usuario (rut,nombre,apellido,fono,direccion) values (@rut,@nombre,@apellido,@fono,@direccion)";
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

        public static int AgregarUsuario(Usuario usuario)
        {
            int filasafectadas = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, connection);
                sqlCommand.CommandText = "Insert into Usuario (rut,nombre,apellido,fono,direccion) values (@rut,@nombre,@apellido,@fono,@direccion)";
                sqlCommand.Parameters.AddWithValue("@rut", usuario.rut);
                sqlCommand.Parameters.AddWithValue("@nombre", usuario.nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", usuario.apellido);
                sqlCommand.Parameters.AddWithValue("@fono", usuario.fono);
                sqlCommand.Parameters.AddWithValue("@direccion", usuario.direccion);
                try
                {
                    connection.Open();
                    filasafectadas = sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return filasafectadas;
        }
            private static Usuario CreacionUsuarios(DataTable dataTable)
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    Usuario usuario = new Usuario();
                    usuario.id_usuario = int.Parse(dataTable.Rows[0]["id_usuario"].ToString());
                    usuario.rut = dataTable.Rows[0]["rut"].ToString();
                    usuario.nombre = dataTable.Rows[0]["nombre"].ToString();
                    usuario.apellido = dataTable.Rows[0]["apellido"].ToString();
                    usuario.fono = dataTable.Rows[0]["fono"].ToString();
                    usuario.direccion = dataTable.Rows[0]["direccion"].ToString();
                return usuario;
                }
                else
                {
                    return null;
                }
            }

            public static int EliminarUsuarioporRut(string rut)
            {
                int resultado = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(null, connection);
                    sqlCommand.CommandText = "Delete from Usuario where rut = @rut";
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

            public static int ActualizarUsuarioID(Usuario usuario)
            {
                int resultado = 0;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                    sqlCommand.CommandText = "Update Usuario SET rut =@rut nombre = @nombre apellido = @apellido fono=@fono direccion=@direccion where id_usuario = @id_usuario";
                    sqlCommand.Parameters.AddWithValue("@rut", usuario.nombre);
                    sqlCommand.Parameters.AddWithValue("@nombre", usuario.nombre);
                    sqlCommand.Parameters.AddWithValue("@apellido", usuario.apellido);
                    sqlCommand.Parameters.AddWithValue("@fono", usuario.apellido);
                    sqlCommand.Parameters.AddWithValue("@direccion", usuario.apellido);
                    sqlCommand.Parameters.AddWithValue("@rut", usuario.rut);
                    sqlCommand.Parameters.AddWithValue("@id_usuario", usuario.id_usuario);

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

            private static DataTable LlenarDataTable(SqlCommand comando)
            {
                //2. llenamos el dataTable(conversion)
                var dataTable = new DataTable();
                var dataAdapter = new SqlDataAdapter(comando);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
            private static List<Usuario> LLenadoUsuarios(DataTable dataTable)
            {
                usuario = new List<Usuario>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Usuario usuarios = new Usuario();
                    usuarios.id_usuario = int.Parse(dataTable.Rows[i]["id_usuario"].ToString());
                    usuarios.rut = dataTable.Rows[i]["rut"].ToString();
                    usuarios.nombre = dataTable.Rows[i]["nombre"].ToString();
                    usuarios.apellido = dataTable.Rows[i]["apellido"].ToString();
                    usuarios.fono = dataTable.Rows[i]["fono"].ToString();
                    usuarios.direccion = dataTable.Rows[i]["direccion"].ToString();


                }
                return usuario;



            }
        }
    }


        

