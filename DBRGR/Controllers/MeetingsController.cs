using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DBRGR.Models;

namespace DBRGR.Controllers
{
    public class MeetingsController : Controller
    {
        private agencyEntities db = new agencyEntities();

        // GET: Meetings
        public ActionResult Index()
        {
            var meetings = db.Meetings.Include(m => m.Candidate).Include(m => m.Client).Include(m => m.Payment);
            return View(meetings.ToList());
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // GET: Meetings/Create
        public ActionResult Create()
        {
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name");
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientName");
            ViewBag.ReceiptID = new SelectList(db.Payments, "ReceiptID", "Seller");
            return View();
        }

        // POST: Meetings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvitationID,MeetDate,MeetTime,ReceiptID,ClientID,CandidateID")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", meeting.CandidateID);
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientName", meeting.ClientID);
            ViewBag.ReceiptID = new SelectList(db.Payments, "ReceiptID", "Seller", meeting.ReceiptID);
            return View(meeting);
        }

        // GET: Meetings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", meeting.CandidateID);
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientName", meeting.ClientID);
            ViewBag.ReceiptID = new SelectList(db.Payments, "ReceiptID", "Seller", meeting.ReceiptID);
            return View(meeting);
        }

        // POST: Meetings/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvitationID,MeetDate,MeetTime,ReceiptID,ClientID,CandidateID")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateID = new SelectList(db.Candidates, "CandidateID", "Name", meeting.CandidateID);
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "ClientName", meeting.ClientID);
            ViewBag.ReceiptID = new SelectList(db.Payments, "ReceiptID", "Seller", meeting.ReceiptID);
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
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
