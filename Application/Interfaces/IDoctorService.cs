using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentSystem.Application.DTOs;

namespace Application.Interfaces
{
    public interface IDoctorService
    {
        Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto dto);
        Task<DoctorDto?> GetDoctorByIdAsync(int id);
        Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
        Task<IEnumerable<DoctorDto>> GetDoctorsByDepartmentAsync(int departmentId);
        Task<bool> UpdateDoctorAsync(int id, CreateDoctorDto dto);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
