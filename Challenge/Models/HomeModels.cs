using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Challenge.Models
{
    public class User 
    { 
        public int Id { get; set; }
        public string User_Type { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string IND { get; set; }
        public string Password { get; set; }
        public string Docket { get; set; }
    }

    public class Professor 
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string IND { get; set; }
        public string State { get; set; }
    }

    public class Subject 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public int IdProfessor { get; set; }
        public int Vacancies { get; set; }
    }

    public class Registration
    {
        public int Id { get; set; }
        public int IdStudent { get; set; }
        public int IdSubject { get; set; }
        public DateTime DateTime { get; set; }
    }
}
