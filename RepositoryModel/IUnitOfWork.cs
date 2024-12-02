using RepositoryModel.IRepository;
using RepositoryModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel
{
    public interface IUnitOFWork : IDisposable
    {
        IDatabaseRepository<Unit> Units { get; }
        IDatabaseRepository<UnitDescription> UnitDescriptions { get; }
        IDatabaseRepository<UnitImages> UnitImages { get; }
        IDatabaseRepository<UnitView> UnitViews { get; }
        IDatabaseRepository<Developer> Developers { get; }
        IAccountRepository Accounts { get; }
        int Compelet();
        void Dispose();
    }
}
