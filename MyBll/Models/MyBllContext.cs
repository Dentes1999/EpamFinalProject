using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using MyRepository.Models;
using MyRepository.Control;
using AutoMapper;
using Ninject;

namespace MyBll.Models
{
    public interface IBll
    {
        void AddApartment(ApartmentEditor ae);
        void DeleteApartment();
        void EditApartment(ApartmentEditor ae);
        void ReserveDates();
        Apartment GetExactApartment(string id);
        ApartmentEditor GetExactApartmentEditor(string id);
        List<ApartmentView> GetAllAppliableViewApartments();
        List<ApartmentView> GetAllViewApartments();
        void AddApplicationForAny(StartFormViewModel st);
        void AddApplication(StartFormViewModel st);
        List<ApartmentView> GetApartments(QueryView qv);
        IQueryable<Application> GetApplications();
        List<ApartmentView> GetSpecialApartments(QueryView qv);
        List<BaseApplicationManager> GetApplicationsForManager();
        List<ApartmentView> GetAppropriateApartments(BaseApplicationManager av);
        void CompleteApplication(BaseApplicationManager bam, string ToChoose,string PricePerNight);
        void DenyApplication(BaseApplicationManager bam);
        List<BaseApplicationManager> GetRequests(string userId);
        List<Application> GetApplications(string userId);
    }
    public class MyBllContext : IBll
    {
        private readonly IRepository _hotelContext;

        public MyBllContext()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepository>().To<HotelRepository>();

             
            _hotelContext = ninjectKernel.Get<IRepository>();
        }

        public void AddApartment(ApartmentEditor ae)
        {
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApartmentEditor, Apartment>()
                    .ForMember(f => f.Href,
                        t => t.MapFrom(a => a.Href1));

            }).CreateMapper().Map<Apartment>(ae);
            _hotelContext.AddApartment(baseApl);
        }

        public void DeleteApartment()
        {
            throw new NotImplementedException();
        }

        public void EditApartment(ApartmentEditor ae)
        {
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApartmentEditor, Apartment>()
                    .ForMember(f => f.Href,
                        t => t.MapFrom(a => a.Href1));

            }).CreateMapper().Map<Apartment>(ae);
            _hotelContext.EditApartment(baseApl);
        }

        public List<ApartmentView> GetSpecialApartments(QueryView qv)
        {
            string price = qv.Price;
            var t1=price.Split(new string[]{" - "},StringSplitOptions.None);
            var low = Convert.ToInt32(t1[0].Substring(1));
            var high = Convert.ToInt32(t1[1].Substring(1));
            var ap=_hotelContext.GetAllAppliableApartments();

            ap = from a in ap
                where (a.PricePerNight > low && a.PricePerNight < high)
                select a;
            if (!String.IsNullOrEmpty(qv.Class))
            {
                var p = qv.Class.Split(new string[] { ", " }, StringSplitOptions.None).ToList();
                ap = from a in ap
                    where p.Contains(a.Class)
                    select a;
            }
            if (!String.IsNullOrEmpty(qv.People))
            {
                var n = Convert.ToInt32(qv.People);
                ap = from a in ap
                    where n <= a.AmountOfPeople
                    select a;
            }

            switch (qv.Sort)
            {
                case "Name":
                    ap = ap.OrderByDescending(a => a.Name);
                    break;
                case "Price":
                    ap = ap.OrderBy(a => a.PricePerNight);
                    break;
                case "Class":
                    ap = ap.Where(a=>a.Class=="Econom").Concat(ap.Where(a => a.Class == "Standard"))
                        .Concat(ap.Where(a => a.Class == "Junior Suite")).Concat(ap.Where(a => a.Class == "Lux"));
                    break;
            }


            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<Apartment, ApartmentView>()
                    .ForMember(f => f.Description,
                        t => t.MapFrom(a => a.Description.Split(',')));
            }).CreateMapper().Map<List<ApartmentView>>(ap.ToList());
            return baseApl;

            



            
        }

        public IQueryable<Application> GetApplications()
        {
            throw new NotImplementedException();
        }

        public void ReserveDates()
        {
            throw new NotImplementedException();
        }

        public void AddApplicationForAny(StartFormViewModel st)
        {
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<StartFormViewModel, BaseApplication>()
                    .ForMember(f => f.NumOfPeople,
                        t => t.MapFrom(a => Convert.ToInt32(a.People)))
                    .ForMember(f => f.DateIn,
                        t => t.MapFrom(a => DateTime.ParseExact(a.DateIn, "yyyy-MM-dd",null)))
                    .ForMember(f => f.DateOut,
                        t => t.MapFrom(a => DateTime.ParseExact(a.DateOut, "yyyy-MM-dd", null)));
            }).CreateMapper().Map<BaseApplication>(st);
            _hotelContext.AddApplicationForAny(baseApl);
        }

        public List<ApartmentView> GetAllAppliableViewApartments()
        {
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<Apartment, ApartmentView>()
                    .ForMember(f => f.Description,
                        t => t.MapFrom(a => a.Description.Split(',')));
            }).CreateMapper().Map<List<ApartmentView>>(_hotelContext.GetAllAppliableApartments().ToList());
            return baseApl;
        }
        public List<ApartmentView> GetAllViewApartments()
        {
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<Apartment, ApartmentView>()
                    .ForMember(f => f.Description,
                        t => t.MapFrom(a => a.Description.Split(',')));
            }).CreateMapper().Map<List<ApartmentView>>(_hotelContext.GetAllApartments().ToList());
            return baseApl;
        }

        public List<ApartmentView> GetApartments(QueryView qv)
        {
            throw new NotImplementedException();
        }

        public Apartment GetExactApartment(string id)
        {
            return _hotelContext.GetExactApartment(id);
        }

        public ApartmentEditor GetExactApartmentEditor(string id)
        {
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<Apartment, ApartmentEditor>();
                    
            }).CreateMapper().Map<ApartmentEditor>(_hotelContext.GetExactApartment(id));
            return baseApl;
        }


        public void AddApplication(StartFormViewModel st)
        {
            var baseApl = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StartFormViewModel, Application>()
                    .ForMember(f => f.NumOfPeople,
                        t => t.MapFrom(a => Convert.ToInt32(a.People)))
                    .ForMember(f => f.Price,
                        t => t.MapFrom(a => Convert.ToInt32(a.Price)));
            }).CreateMapper().Map<Application>(st);
            var r=new ReservedDates();
            r.DateStart = DateTime.ParseExact(st.DateIn, "yyyy-MM-dd", null);
            r.DateEnd = DateTime.ParseExact(st.DateOut, "yyyy-MM-dd", null);
            baseApl.ForDates = r;
            
            DateTime afttom=DateTime.Today.AddDays(2);
            if (afttom > r.DateStart) afttom = r.DateStart;
            baseApl.ExpirationDate = afttom;
            _hotelContext.AddApplication(baseApl,Convert.ToInt32(st.ApId));
        }

        public List<BaseApplicationManager> GetApplicationsForManager()
        {
            var ba= _hotelContext.GetApplicationsForManager().ToList();
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<BaseApplication, BaseApplicationManager>()
                    .ForMember(f => f.NumOfPeople,
                        t => t.MapFrom(a => a.NumOfPeople.ToString()))
                    .ForMember(f => f.DateIn,
                        t => t.MapFrom(a => a.DateIn.ToString("yyyy-MM-dd")))
                    .ForMember(f => f.DateOut,
                        t => t.MapFrom(a => a.DateOut.ToString("yyyy-MM-dd")));
            }).CreateMapper().Map<List<BaseApplicationManager>>(ba);
            return baseApl;
        }

        private static bool IsValidDateInterval(string datein, string dateout, ICollection<Application> appls)
        {
            if (appls == null) return true;
            bool res = true;
            var t1 = DateTime.ParseExact(datein, "yyyy-MM-dd", null);
            var t2 = DateTime.ParseExact(dateout, "yyyy-MM-dd", null);
            foreach (var appl in appls)
            {
                var datestart = appl.ForDates.DateStart;
                var dateend = appl.ForDates.DateEnd;
                if (t1 >= datestart && t1 <= dateend) res = false;
                if (t2 >= datestart && t2 <= dateend) res = false;
                if (t1 <= datestart && t2 >= dateend) res = false;
            }

            return res;
        }

        public List<ApartmentView> GetAppropriateApartments(BaseApplicationManager av)
        {
            var all = _hotelContext.GetAllAppliableApartments().ToList();
            /**/
            var resap = (from ap in all
                where (ap.Class == av.Class && Convert.ToInt32(av.NumOfPeople) < ap.AmountOfPeople
                                            && IsValidDateInterval(av.DateIn, av.DateOut, ap.Applications))
                select ap).ToList();
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<Apartment, ApartmentView>()
                    .ForMember(f => f.Description,
                        t => t.MapFrom(a => a.Description.Split(',')));
            }).CreateMapper().Map<List<ApartmentView>>(resap);
            return baseApl;
        }

        public void CompleteApplication(BaseApplicationManager bam, string ToChoose,string PricePerNight)
        {
            string bamId = bam.BaseApplicationId;
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<BaseApplicationManager, Application>()
                    .ForMember(f => f.NumOfPeople,
                        t => t.MapFrom(a => Convert.ToInt32(a.NumOfPeople)))
                    .ForMember(f=>f.Status,t=>t.Ignore());
            }).CreateMapper().Map<Application>(bam);
            var r = new ReservedDates();
            r.DateStart = DateTime.ParseExact(bam.DateIn, "yyyy-MM-dd", null);
            r.DateEnd = DateTime.ParseExact(bam.DateOut, "yyyy-MM-dd", null);
            baseApl.ForDates = r;

            DateTime afttom = DateTime.Today.AddDays(2);
            if (afttom > r.DateStart) afttom = r.DateStart;
            baseApl.ExpirationDate = afttom;
            baseApl.Price = Convert.ToInt32((r.DateEnd - r.DateStart).TotalDays)*baseApl.NumOfPeople*Convert.ToInt32(PricePerNight);
            _hotelContext.AddApplicationWithDeleting(baseApl,ToChoose,bamId);
        }

        public void DenyApplication(BaseApplicationManager bam)
        {
            _hotelContext.DenyApplication(Convert.ToInt32(bam.BaseApplicationId));
        }

        public List<BaseApplicationManager> GetRequests(string userId)
        {
            var res = _hotelContext.GetAllUserRequestsWithDeleting(userId).ToList();
            var baseApl = new MapperConfiguration(cfg => {
                cfg.CreateMap<BaseApplication, BaseApplicationManager>()
                    .ForMember(f => f.NumOfPeople,
                        t => t.MapFrom(a => a.NumOfPeople.ToString()))
                    .ForMember(f => f.DateIn,
                        t => t.MapFrom(a => a.DateIn.ToString("yyyy-MM-dd")))
                    .ForMember(f => f.DateOut,
                        t => t.MapFrom(a => a.DateOut.ToString("yyyy-MM-dd")));
            }).CreateMapper().Map<List<BaseApplicationManager>>(res);
            return baseApl;
            
        }

        public List<Application> GetApplications(string userId)
        {
            var res = _hotelContext.GetApplications(userId).ToList();
            return res;
        }
    }
}
