using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NoorGeneralHospital.Models.Sp_Model;

namespace NoorGeneralHospital.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

      
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Doctor_GetDoctorDetails> Sp_Doctor_GetDoctorDetails { get; set; }
        public virtual DbSet<Doctor_GetDoctorDetailsByID> Sp_Doctor_GetDoctorDetailsByID { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Days> Days { get; set; }
        public virtual DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Activities> activities { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }


}