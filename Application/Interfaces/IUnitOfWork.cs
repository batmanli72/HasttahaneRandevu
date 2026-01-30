using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointments { get; }
        IDoctorRepository Doctors { get; }
        IDepartmentRepository Departments { get; }
        IPatientRepository Patients { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

    public interface IAppointmentRepository
    {
    }

    public interface IDepartmentRepository
    {
    }

    public interface IPatientRepository
    {
    }

    public interface IDoctorRepository
    {
    }
}
