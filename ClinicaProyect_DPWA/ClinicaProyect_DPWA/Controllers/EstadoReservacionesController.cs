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
    public class EstadoReservacionesController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: EstadoReservaciones
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
            return View(db.EstadoReservaciones.ToList());
        }

        // GET: EstadoReservaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            if (estadoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(estadoReservacion);
        }

        // GET: EstadoReservaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoReservaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_EstadoReservacion,EstadoDeCita")] EstadoReservacion estadoReservacion)
        {
            if (ModelState.IsValid)
            {
                db.EstadoReservaciones.Add(estadoReservacion);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            return View(estadoReservacion);
        }

        // GET: EstadoReservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            if (estadoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(estadoReservacion);
        }

        // POST: EstadoReservaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_EstadoReservacion,EstadoDeCita")] EstadoReservacion estadoReservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoReservacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            return View(estadoReservacion);
        }

        // GET: EstadoReservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            if (estadoReservacion == null)
            {
                return HttpNotFound();
            }
            return View(estadoReservacion);
        }

        // POST: EstadoReservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoReservacion estadoReservacion = db.EstadoReservaciones.Find(id);
            db.EstadoReservaciones.Remove(estadoReservacion);
            db.SaveChanges();
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
