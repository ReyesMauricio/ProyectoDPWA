using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaProyect_DPWA.Context;
using ClinicaProyect_DPWA.Models;

namespace ClinicaProyect_DPWA.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class MedicosController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Medicos
        
        public ActionResult Index()
        {
            if (TempData["Accion"] != null)
            {
                var accion = Convert.ToString(TempData["Accion"]);
                if (accion == "Insertado")
                {
                    ViewBag.Accion = "Insertado";
                }
                else if (accion == "Editado")
                {
                    ViewBag.Accion = "Editado";
                }
                else if (accion == "Eliminado")
                {
                    ViewBag.Accion = "Eliminado";
                }
            }
            var medicos = db.Medicos.Include(m => m.CategoriaMedico);
            return View(medicos.ToList());
        }

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicos, "Id_Categoria", "NombreCategoria");
            return View();
        }

        // POST: Medicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Medico,Nombres,Apellidos,Genero,FechaNacimiento,Email,Direccion,Movil,Fecha_Creacion,IsActive,Numero_documento,Id_Categoria")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Medicos.Add(medico);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicos, "Id_Categoria", "NombreCategoria", medico.Id_Categoria);
            return View(medico);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicos, "Id_Categoria", "NombreCategoria", medico.Id_Categoria);
            ViewBag.FechaNacimiento = string.Format("{0:dd/MM/yyyy}", medico.FechaNacimiento);
            ViewBag.Fecha_Creacion = string.Format("{0:dd/MM/yyyy}", medico.Fecha_Creacion);
            ViewBag.Movil = medico.Movil;
            ViewBag.Dui = medico.Numero_documento;
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Medico,Nombres,Apellidos,Genero,FechaNacimiento,Email,Direccion,Movil,Fecha_Creacion,IsActive,Numero_documento,Id_Categoria")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            ViewBag.Id_Categoria = new SelectList(db.CategoriaMedicos, "Id_Categoria", "NombreCategoria", medico.Id_Categoria);
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Medicos.Find(id);
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Medicos.Find(id);
            db.Medicos.Remove(medico);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.ex = "No se puede borrar este dato porque tiene relacion con otras tablas";
                throw;
            }

            TempData["Accion"] = "Eliminado";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
