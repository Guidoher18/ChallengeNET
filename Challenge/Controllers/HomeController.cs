using Challenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Challenge.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Inicia Sesión en la App
        /// </summary>
        /// <param name="Tipo_de_Usuario"> Admin o Alumno </param>
        /// <param name="IND">Documento Nacional de Identidad. String de 8 dígitos</param>
        /// <param name="Password">Contraseña: 6 caractéres</param>
        /// <param name="Docket">Legajo (sólo Estudiantes): 4 dígitos</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string Tipo_de_Usuario, string IND, string Password, string Docket)
        {
            HomeManager Manager = new HomeManager();
            User User = new User();
            User = Manager.getUser(IND);

            if (User.Id != 0)
            {
                if (User.User_Type == Tipo_de_Usuario &&
                    User.Password == Password &&
                    User.Docket == Docket)
                {
                    Session["User_loggedIn"] = User;

                    string vista = "";

                    switch (User.User_Type)
                    {
                        case "Alumno":
                            vista = "Estudiante";
                            break;
                        case "Admin":
                            vista = "Admin";
                            ViewBag.Iniciales = User.First_Name.Substring(0, 1) + User.Last_Name.Substring(0, 1);
                            break;
                    }
                    return RedirectToAction(vista);
                }
                else
                {
                    ViewBag.Error = "<strong>Los Datos son erróneos.</strong><br/><span>Verifique que los haya ingresado correctamente.</span>";
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            else
            {
                ViewBag.Error = "<strong>El Usuario es Inexistente.</strong><br/> <span> Contáctese con el Administrador</span>";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        /// <summary>
        /// Cierra la Sesión y elimina los datos almacenados en Session 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Admin()
        {
            User User = new User();
            User = (User)Session["User_loggedIn"];

            if (isLoggedIn("Admin"))
            {
                ViewBag.Materias = Consultar_Materias();
                ViewBag.Docentes = Consultar_Docentes();

                ViewBag.Iniciales = User.First_Name.Substring(0, 1) + User.Last_Name.Substring(0, 1);
                return View();
            }
            else
            {
                ViewBag.Error = "<span>Para acceder</span><br/><strong>Inicie Sesión con una cuenta de Admin</strong>";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        private Dictionary<int, Subject> Consultar_Materias() {
            HomeManager Manager = new HomeManager();
            Dictionary <int,Subject> Subjects = Manager.getSubjects();

            return Subjects;
        }

        private Dictionary<int, Professor> Consultar_Docentes()
        {
            HomeManager Manager = new HomeManager();
            Dictionary<int, Professor> Professors = Manager.getProfessors();

            return Professors;
        }

        //ABM - MATERIAS

        /// <summary>
        /// Permite Agregar una Materia a la BBDD
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Descripcion"></param>
        /// <param name="Horario"></param>
        /// <param name="Docente"></param>
        /// <param name="Cupo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AMateria(string Nombre, string Descripcion, string Horario, string Docente, int Cupo)
        {
            Subject Subject = new Subject();

            Subject.Name = Nombre;
            Subject.Description = Descripcion;
            Subject.Schedule = Horario;
            Subject.IdProfessor = int.Parse(Docente);
            Subject.Vacancies = Cupo;

            HomeManager Manager = new HomeManager();
            Manager.addSubject(Subject);

            return RedirectToAction("Admin");
        }

        /// <summary>
        /// Permite Eliminar una Materia de la BBDD
        /// </summary>
        /// <param name="Cod"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BMateria(string Cod) 
        {
            HomeManager Manager = new HomeManager();
            Manager.delete("Subjects", Cod);

            return RedirectToAction("Admin");
        }

        /// <summary>
        /// Permite Modificar una Materia de la BBDD
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Nombre"></param>
        /// <param name="Descripcion"></param>
        /// <param name="Horario"></param>
        /// <param name="Docente"></param>
        /// <param name="Cupo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MMateria(string Id, string Nombre, string Descripcion, string Horario, string Docente, int Cupo)
        {
            Subject Subject = new Subject();

            Subject.Id = int.Parse(Id);
            Subject.Name = Nombre;
            Subject.Description = Descripcion;
            Subject.Schedule = Horario;
            Subject.IdProfessor = int.Parse(Docente);
            Subject.Vacancies = Cupo;

            HomeManager Manager = new HomeManager();
            Manager.updateSubject(Subject);

            return RedirectToAction("Admin");
        }

        //ABM - DOCENTES

        /// <summary>
        /// Permite Agregar un Docente a la BBDD
        /// </summary>
        /// <param name="ApellidoDocente"></param>
        /// <param name="NombreDocente"></param>
        /// <param name="DNIDocente"></param>
        /// <param name="EstadoDocente"></param>
        /// <returns></returns>
        public ActionResult ADocente(string ApellidoDocente, string NombreDocente, string DNIDocente, string EstadoDocente)
        {
            Professor Professor = new Professor();

            Professor.Last_Name = ApellidoDocente;
            Professor.First_Name = NombreDocente;
            Professor.IND = DNIDocente;
            Professor.State = EstadoDocente;

            HomeManager Manager = new HomeManager();
            Manager.addProfessor(Professor);

            return RedirectToAction("Admin");
        }

        /// <summary>
        /// Permite Eliminar un Docente de la BBDD
        /// </summary>
        /// <param name="Cod"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BDocente(string Cod)
        {
            HomeManager Manager = new HomeManager();
            Manager.delete("Professors", Cod);

            return RedirectToAction("Admin");
        }

        /// <summary>
        /// Permite Modificar un Docente de la BBDD
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ApellidoDocente"></param>
        /// <param name="NombreDocente"></param>
        /// <param name="DNIDocente"></param>
        /// <param name="EstadoDocente"></param>
        /// <returns></returns>
        public ActionResult MDocente(string Id, string ApellidoDocente, string NombreDocente, string DNIDocente, string EstadoDocente)
        {
            Professor Professor = new Professor();

            Professor.Id = int.Parse(Id);
            Professor.Last_Name = ApellidoDocente;
            Professor.First_Name = NombreDocente;
            Professor.IND = DNIDocente;
            Professor.State = EstadoDocente;

            HomeManager Manager = new HomeManager();
            Manager.updateProfessor(Professor);

            return RedirectToAction("Admin");
        }

        public ActionResult Estudiante()
        {
            User User = new User();
            User = (User)Session["User_loggedIn"];

            if (isLoggedIn("Alumno"))
            {
                return View();
            }
            else
            {
                ViewBag.Error = "<span>Para acceder</span><br/><strong>Inicie Sesión con una cuenta de Estudiante</strong>";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        /// <summary>
        /// Verifica si el usuario está loggeado (si hay un User guardado en Session), y si coincide con el Type requerido
        /// </summary>
        /// <param name="Type"> Admin o Alumno </param>
        /// <returns>bool</returns>
        private bool isLoggedIn(string Type)
        {
            User User = new User();
            User = (User)Session["User_loggedIn"];

            if (Session["User_loggedIn"] != null && User.User_Type == Type)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}