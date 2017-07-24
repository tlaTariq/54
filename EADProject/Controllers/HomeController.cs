using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EADProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Songs()
        {
            return View();
        }
        public ActionResult Rock()
        {
            return View();
        }

        public ActionResult Pop()
        {
            return View();
        }

        public ActionResult Jazz()
        {
            return View();
        }

        public ActionResult MyPL()
        {
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        //[ActionName("Login")]
        [HttpPost]
        public ActionResult Login2()
        {
            String login = Request["txtLogin"];
            String password = Request["txtPassword"];

            var dto = Models.DBHelper.Validate(login, password);
            if (dto != null)
            {
                Session["Login"] = login;
                Session["ID"] = dto.UserID;

                if (login == "admin")
                    Session["admin"] = "yes";
                else
                    Session["user"] = "yes";

                return Redirect("~/Home/Index");
            }
            else
            {
                ViewBag.Login = login;
                ViewBag.Msg = "Not valid";
            }

            return View("Login");
        }

        public ActionResult Signup()
        {
            String login = Request["txtLogin"];
            String password = Request["txtPassword"];

            if (Models.DBHelper.ValidateSignUp(login, password))
            {
                Session["Login"] = login;
                Session["ID"] = Models.DBHelper.addUser(login, password);

                if (login == "admin")
                    Session["admin"] = true;
                else
                    Session["admin"] = true;

                return Redirect("~/Home/Index");
            }
            else
            {
                ViewBag.Login = login;
                ViewBag.Msg = "Username already registered!";
            }

            return View("Login");
        }

        public ActionResult Logout()
        {
            Session["Login"] = null;
            Session.Abandon();

            return Redirect("~/Home/Index");
        }

        public ActionResult Add()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Add2(string txtName, string txtSinger, string txtCategory, HttpPostedFile file)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            String name = txtName;
            String singer = txtSinger;
            String category = txtCategory;
            var song = Request.Files.Get("file");
                 
            var allowedExtensions = new[] { ".mp3" };
            var checkextension = Path.GetExtension(song.FileName).ToLower();

            if (!allowedExtensions.Contains(checkextension))
            {
                TempData["notice"] = "PLEASE SELECT \"mp3\" FILE";
                return Redirect("Add");
            }



            if (song != null && song.ContentLength > 0)
            {
                var fileName = Path.GetFileName(song.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/songs"), fileName);
                song.SaveAs(path);

                Models.DBHelper.addSong(name, singer, category, fileName);
            }

            return View("Songs");
        }



        public ActionResult Edit(int id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Models.SongsDTO obj = Models.DBHelper.getSongByID(id);

            ViewBag.id = obj.SongID;
            ViewBag.singer = obj.Singer;
            ViewBag.name = obj.Name;

            return View();
        }

        public ActionResult Edit2()
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int id = Convert.ToInt32(Request["txtID"]);
            String name = Request["txtName"];
            String singer = Request["txtSinger"];

            Models.DBHelper.updateSong(id, name, singer);
            
            return View("Songs");
        }

        public ActionResult AddtoPlaylist(int id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int uid = Convert.ToInt32(Session["ID"]);

            Models.DBHelper.addUserSong(uid, id);
            
            return View("Songs");
        }

        public ActionResult RemoveFromPlaylist(int id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int uid = Convert.ToInt32(Session["ID"]);

            Models.DBHelper.removeUserSong(uid, id);

            return View("MyPL");
        }

        public ActionResult Delete(int id)
        {
            if (Session["Login"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Models.SongsDTO obj = Models.DBHelper.getSongByID(id);

            ViewBag.link = obj.Link;
            String link = obj.Link;

            var path = Path.Combine(Server.MapPath("~/Content/songs"), link);
            FileInfo f = new FileInfo(path);
            f.Delete();


            Models.DBHelper.deleteSong(id);

            return View("Songs");
        }
    }
}