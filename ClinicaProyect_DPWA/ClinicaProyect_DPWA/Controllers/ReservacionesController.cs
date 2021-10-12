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
    public class ReservacionesController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Reservaciones
        public ActionResult Index()
        {
            var reservaciones = db.Reservaciones.Include(r => r.EstadoPago).Include(r => r.EstadoReservacion).Include(r => r.Medico).Include(r => r.Paciente);
            return View(reservaciones.ToList());
        }

        // GET: Reservaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservaciones/Create
        public ActionResult Create()
        {
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago");
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita");
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres");
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres");
            return View();
        }

        // POST: Reservaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Reservacion,Titulo_reservacion,Nota_reservacion,Direccion,Fecha_creado,Fecha_cita,Hora_cita,Sintomas,Medicamentos,Precio,Id_Medico,Id_Paciente,Id_EstadoReservacion,Id_TipoDePago")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservaciones.Add(reservacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            return View(reservacion);
        }

        // GET: Reservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            return View(reservacion);
        }

        // POST: Reservaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Reservacion,Titulo_reservacion,Nota_reservacion,Direccion,Fecha_creado,Fecha_cita,Hora_cita,Sintomas,Medicamentos,Precio,Id_Medico,Id_Paciente,Id_EstadoReservacion,Id_TipoDePago")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            return View(reservacion);
        }

        // GET: Reservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservaciones.Find(id);
            db.Reservaciones.Remove(reservacion);
            db.SaveChanges();
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
