using NoorGeneralHospital.Areas.Admin.Models.Entities;
using NoorGeneralHospital.Areas.Admin.Models.OutputDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NoorGeneralHospital.Areas.Admin.Services.Interfaces
{
    public interface ISpecialityService 
    {
        List<Speciality> GetSpecialities();
        Speciality GetSpecialityById(Guid? specialityId);
        Task<GeneralResponse> AddOrUpdateEmployee(Speciality speciality);
        GeneralResponse DeleteSpeciality(Guid specialityId);
    }
}