
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<DepartmentDto> CreateDepartmentAsync(CreateDepartmentDto dto);
        Task<DepartmentDto?> GetDepartmentByIdAsync(int id);
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<IEnumerable<DepartmentDto>> GetActiveDepartmentsAsync();
        Task<bool> UpdateDepartmentAsync(int id, CreateDepartmentDto dto);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
