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
                            break;
                    }

                    return View("~/Views/Home/" + vista + ".cshtml");
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

        public ActionResult Admin()
        {
            User User = new User();
            User = (User)Session["User_loggedIn"];

            if (isLoggedIn() && User.User_Type == "Admin")
            {
                return View();
            }
            else
            {
                ViewBag.Error = "<span>Para acceder</span><br/><strong>Inicie Sesión con una cuenta de Admin.</strong>";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public ActionResult Estudiante()
        {
            User User = new User();
            User = (User)Session["User_loggedIn"];

            if (isLoggedIn() && User.User_Type == "Alumno")
            {
                return View();
            }
            else
            {
                ViewBag.Error = "<span>Para acceder</span><br/><strong>Inicie Sesión con una cuenta de Estudiante.</strong>";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        /// <summary>
        /// Verifica si el usuario está loggeado (si hay un User guardado en Session)
        /// </summary>
        /// <returns>bool</returns>
        private bool isLoggedIn()
        {
            User User = new User();
            if (Session["User_loggedIn"] != null)
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