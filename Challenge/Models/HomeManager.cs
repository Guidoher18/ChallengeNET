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

        /// <summary>
        /// Obtiene las Materias de la BBDD
        /// </summary>
        /// <returns>Diccionario con las Materias ordenadas alfabéticamente</returns>
        public Dictionary<int, Subject> getSubjects() {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "SELECT * FROM Subjects ORDER BY Name";

            try
            {
                SqlDataReader reader = Sentencia.ExecuteReader();
                
                Dictionary<int, Subject> Subjects = new Dictionary<int, Subject>();
                int Counter = 0;

                while (reader.Read())
                {
                    Subject Subject = new Subject();

                    Subject.Id = (int)reader["Id"];
                    Subject.Name = (string)reader["Name"];

                    try
                    {
                        Subject.Description = (string)reader["Description"];
                    }
                    catch (InvalidCastException)
                    {
                        Subject.Description = "";
                    }

                    Subject.Schedule = (string)reader["Schedule"];
                    Subject.IdProfessor = (int)reader["IdProfessor"];
                    Subject.Vacancies = (int)reader["Vacancies"];

                    Subjects.Add(Counter, Subject);
                    Counter += 1;
                }

                Conexion.Close();
                return Subjects;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene los Docentes de la BBDD
        /// </summary>
        /// <returns>Diccionario con los Docentes ordenados alfabéticamente por Apellido. La clave es el Id</returns>
        public Dictionary<int, Professor> getProfessors()
        {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "SELECT * FROM Professors ORDER BY Last_Name";

            try
            {
                SqlDataReader reader = Sentencia.ExecuteReader();

                Dictionary<int, Professor> Professors = new Dictionary<int, Professor>();

                while (reader.Read())
                {
                    Professor Professor = new Professor();

                    Professor.Id = (int)reader["Id"];
                    Professor.First_Name = (string)reader["First_Name"];
                    Professor.Last_Name = (string)reader["Last_Name"];
                    Professor.IND = (string)reader["IND"];
                    Professor.State = (string)reader["State"];

                    Professors.Add(Professor.Id, Professor);
                }

                Conexion.Close();
                return Professors;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        //AM - MATERIAS

        /// <summary>
        /// Agrega una Materia a la tabla Subjects
        /// </summary>
        /// <param name="Subject"></param>
        [HttpPost]
        public void addSubject(Subject Subject) 
        {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "INSERT INTO Subjects (Name, Description, Schedule, IdProfessor, Vacancies) VALUES(@Name, @Description, @Schedule, @IdProfessor, @Vacancies)";

            Sentencia.Parameters.AddWithValue("@Name", Subject.Name);
            Sentencia.Parameters.AddWithValue("@Description", Subject.Description);
            Sentencia.Parameters.AddWithValue("@Schedule", Subject.Schedule);
            Sentencia.Parameters.AddWithValue("@IdProfessor", Subject.IdProfessor);
            Sentencia.Parameters.AddWithValue("@Vacancies", Subject.Vacancies);

            Sentencia.ExecuteNonQuery();
            Conexion.Close();
        }
        
        /// <summary>
        /// Actualiza una Materia en la BBDD
        /// </summary>
        /// <param name="Subject"></param>
        [HttpPost]
        public void updateSubject(Subject Subject) 
        {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "UPDATE Subjects SET Name = @Name, Description = @Description, Schedule = @Schedule, IdProfessor = @IdProfessor, Vacancies = @Vacancies WHERE Id = @Id";

            Sentencia.Parameters.AddWithValue("@Id", Subject.Id);
            Sentencia.Parameters.AddWithValue("@Name", Subject.Name);
            Sentencia.Parameters.AddWithValue("@Description", Subject.Description);
            Sentencia.Parameters.AddWithValue("@Schedule", Subject.Schedule);
            Sentencia.Parameters.AddWithValue("@IdProfessor", Subject.IdProfessor);
            Sentencia.Parameters.AddWithValue("@Vacancies", Subject.Vacancies);

            Sentencia.ExecuteNonQuery();
            Conexion.Close();
        }

        //AM - DOCENTES

        /// <summary>
        /// Agrega un Docente a la tabla Subjects
        /// </summary>
        /// <param name="Docente"></param>
        [HttpPost]
        public void addProfessor(Professor Professor)
        {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "INSERT INTO Professors (Last_Name, First_Name, IND, State) VALUES(@Last_Name, @First_Name, @IND, @State)";

            Sentencia.Parameters.AddWithValue("@Last_Name", Professor.Last_Name);
            Sentencia.Parameters.AddWithValue("@First_Name", Professor.First_Name);
            Sentencia.Parameters.AddWithValue("@IND", Professor.IND);
            Sentencia.Parameters.AddWithValue("@State", Professor.State);

            Sentencia.ExecuteNonQuery();
            Conexion.Close();
        }

        /// <summary>
        /// Actualiza un Docente de la BBDD
        /// </summary>
        /// <param name="Professor"></param>
        [HttpPost]
        public void updateProfessor(Professor Professor)
        {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "UPDATE Professors SET Last_Name = @Last_Name, First_Name = @First_Name, IND = @IND, State = @State WHERE Id = @Id";

            Sentencia.Parameters.AddWithValue("@Id", Professor.Id);
            Sentencia.Parameters.AddWithValue("@Last_Name", Professor.Last_Name);
            Sentencia.Parameters.AddWithValue("@First_Name", Professor.First_Name);
            Sentencia.Parameters.AddWithValue("@IND", Professor.IND);
            Sentencia.Parameters.AddWithValue("@State", Professor.State);

            Sentencia.ExecuteNonQuery();
            Conexion.Close();
        }

        //B
        /// <summary>
        /// Elimina un registro de la BBDD
        /// </summary>
        /// <param name="Tabla"></param>
        /// <param name="Id"></param>
        [HttpPost]
        public void delete(string Tabla, string Id) 
        {
            SqlConnection Conexion = new SqlConnection(ConfigurationManager.AppSettings["ConexionBBDD"]);
            Conexion.Open();
            SqlCommand Sentencia = Conexion.CreateCommand();
            Sentencia.CommandText = "DELETE FROM " + Tabla + " WHERE Id = " + Id;

            Sentencia.ExecuteNonQuery();
            Conexion.Close();
        }
    }
}
