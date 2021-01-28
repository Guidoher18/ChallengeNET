using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Challenge.Models
{
    public class HomeManager : Controller
    {
        /// <summary>
        /// Consulta en BBDD si existe un User con ese Documento y lo devuelve.
        /// </summary>
        /// <param name="IND">Documento Nacional de Identidad. String de 8 dígitos</param>
        /// <returns>User</returns>
        [HttpPost]
        public User getUser(string IND)
        {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "SELECT * FROM Users WHERE IND = '" + IND + "'";

            try
            {
                SqlDataReader reader = Sentencia.ExecuteReader();
                User User = new User();

                while (reader.Read())
                {
                    User.Id = (int)reader["Id"];
                    User.User_Type = (string)reader["User_Type"];
                    User.First_Name = (string)reader["First_Name"];
                    User.Last_Name = (string)reader["Last_Name"];
                    User.IND = (string)reader["IND"];
                    User.Password = (string)reader["Password"];

                    try
                    {
                        User.Docket = (string)reader["Docket"];
                    }
                    catch (InvalidCastException) {
                        User.Docket = "";
                    }
                }

                Conexion.Close();
                return User;
            }
            catch (NullReferenceException)
            {
                return null;
            }

        }
    }
}
