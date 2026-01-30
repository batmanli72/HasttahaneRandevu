using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentSystem.Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterPatientDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<PatientDto> GetPatientByIdAsync(int id);
    }
}
