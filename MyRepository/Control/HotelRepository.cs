using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRepository.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Mvc.Html;
using MyRepository.Context;
using MyRepository.Models;

namespace MyRepository.Control
{
    public interface IRepository
    {
        void AddApartment(Apartment ap);
        void DeleteApartment();
        void EditApartment(Apartment ae);
        void ReserveDates();
        
        void AddApplicationForAny(BaseApplication st);
        void AddApplication(Application st,int apartid);
        void AddApplicationWithDeleting(Application st, string apartid,string manId);
        IEnumerable<Apartment> GetAllApartments();
        IEnumerable<Apartment> GetAllAppliableApartments();
        IEnumerable<Application> GetApplications(string userId);
        Apartment GetExactApartment(string id);
        IEnumerable<BaseApplication> GetApplicationsForManager();
        void DenyApplication(int Id);
        IEnumerable<BaseApplication> GetAllUserRequestsWithDeleting(string userId);
        
    }

    public class HotelRepository : IDisposable, IRepository
    {
        private readonly HotelContext _context;

        public HotelRepository()
        {
            _context=new HotelContext();
        }
        public void AddApartment()
        {
            throw new NotImplementedException();
        }

        public void DeleteApartment()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void EditApartment(Apartment ae)
        {
            var tochange = _context.Apartments.Find(ae.ApartmentId);
            var href = tochange.Href;
            if (ae.Href == "fake") ae.Href = href;
            _context.Entry(tochange).CurrentValues.SetValues(ae);

            _context.SaveChanges();
        }

        public IEnumerable<Apartment> GetAllApartments()
        {
            return _context.Apartments;
        }

        public IEnumerable<Apartment> GetAllAppliableApartments()
        {
            return _context.Apartments.Where(a=>a.NotAppliable!=true);
        }

        

        public IEnumerable<Application> GetApplications(string userId)
        {
            var res = from ap in _context.Applications where ap.Status == "Waiting for payment" &&ap.ForDates.DateStart>=DateTime.Today&&ap.UserId==userId select ap;
            var deleting = from ap in _context.Applications where  ap.ForDates.DateStart < DateTime.Today select ap;
            foreach (var appl in deleting)
            {
                _context.Applications.Remove(appl);
            }

            _context.SaveChanges();
            return res;
        }

        public void ReserveDates()
        {
            throw new NotImplementedException();
        }

        public void AddApplicationForAny(BaseApplication st)
        {
            _context.BaseApplications.Add(st);
            _context.SaveChanges();
            
        }

        public Apartment GetExactApartment(string id)
        {
            int o = Convert.ToInt32(id);
            var t= _context.Apartments.Where(a => (a.ApartmentId == o));
            var y = t.First();
            return y;


        }

        public void AddApplication(Application st,int apartid)
        {
            
            var t = _context.Apartments.Where(w => (w.ApartmentId == apartid));
            var y = t.First();
            //var a = _context.Apartments.FirstOrDefault(f =>( f.ApartmentId == apartid));
            y.Applications.Add(st);
            //_context.Applications.Add(st);
            _context.SaveChanges();
        }

        public void AddApartment(Apartment ap)
        {
            _context.Apartments.Add(ap);
            _context.SaveChanges();
        }

        public IEnumerable<BaseApplication> GetApplicationsForManager()
        {
            
            var all = _context.BaseApplications.ToList();
            var allvalid=from ba in all where (DateTime.Today<=ba.DateIn && ba.Status!="No appropriate apartments") select ba;
            var allnotvalid = from ba in all where DateTime.Today > ba.DateIn select ba;
            foreach (var v in allnotvalid)
            {
                _context.BaseApplications.Remove(v);
            }

            _context.SaveChanges();
            return allvalid;
        }

        public void AddApplicationWithDeleting(Application st, string apartid,string bamid)
        {
            int apId = Convert.ToInt32(apartid);
            int bamId = Convert.ToInt32(bamid);
            var t = _context.Apartments.Where(w => (w.ApartmentId == apId));
            var y = t.First();
            //var a = _context.Apartments.FirstOrDefault(f =>( f.ApartmentId == apartid));
            y.Applications.Add(st);
            var del=_context.BaseApplications.Find(bamId);
            _context.BaseApplications.Remove(del);
            //_context.Applications.Add(st);
            _context.SaveChanges();

        }

        public void DenyApplication(int Id)
        {
            var bam = _context.BaseApplications.Find(Id);
            bam.Status = "No appropriate apartments";
            _context.SaveChanges();
        }

        public IEnumerable<BaseApplication> GetAllUserRequestsWithDeleting(string userId)
        {
            var res = from bam in _context.BaseApplications where bam.UserId == userId &&bam.DateIn>=DateTime.Today select bam;
            var allnotvalid = from ba in _context.BaseApplications where DateTime.Today > ba.DateIn select ba;
            foreach (var v in allnotvalid)
            {
                _context.BaseApplications.Remove(v);
            }

            _context.SaveChanges();
            return res;
        }
    }
}
