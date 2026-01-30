using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto dto)
        {
            // Check if email already exists
            var existingDoctor = await _unitOfWork.Doctors.GetByEmailAsync(dto.Email);
            if (existingDoctor != null)
            {
                throw new InvalidOperationException("Bu email adresi zaten kullanılıyor.");
            }

            // Verify department exists
            var department = await _unitOfWork.Departments.GetByIdAsync(dto.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException("Departman bulunamadı.");
            }

            var doctor = new Doctor
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Specialization = dto.Specialization,
                LicenseNumber = dto.LicenseNumber,
                DepartmentId = dto.DepartmentId,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdDoctor = await _unitOfWork.Doctors.AddAsync(doctor);
            await _unitOfWork.SaveChangesAsync();

            // Reload with navigation properties
            var result = await _unitOfWork.Doctors.GetByIdAsync(createdDoctor.Id);
            return _mapper.Map<DoctorDto>(result);
        }

        public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            return doctor == null ? null : _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _unitOfWork.Doctors.GetAllAsync();
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsByDepartmentAsync(int departmentId)
        {
            var doctors = await _unitOfWork.Doctors.GetByDepartmentIdAsync(departmentId);
            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }

        public async Task<bool> UpdateDoctorAsync(int id, CreateDoctorDto dto)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null)
            {
                return false;
            }

            // Check if email is being changed and if it's already in use
            if (doctor.Email != dto.Email)
            {
                var existingDoctor = await _unitOfWork.Doctors.GetByEmailAsync(dto.Email);
                if (existingDoctor != null)
                {
                    throw new InvalidOperationException("Bu email adresi zaten kullanılıyor.");
                }
            }

            doctor.FirstName = dto.FirstName;
            doctor.LastName = dto.LastName;
            doctor.Email = dto.Email;
            doctor.Phone = dto.Phone;
            doctor.Specialization = dto.Specialization;
            doctor.LicenseNumber = dto.LicenseNumber;
            doctor.DepartmentId = dto.DepartmentId;

            await _unitOfWork.Doctors.UpdateAsync(doctor);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(id);
            if (doctor == null)
            {
                return false;
            }

            await _unitOfWork.Doctors.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

    

    public interface IDoctorService
    {
    }
}
