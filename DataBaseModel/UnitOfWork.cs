using DataBaseModel.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RepositoryModel;
using RepositoryModel.IRepository;
using RepositoryModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModel
{
    public class UnitOFWork : IUnitOFWork
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;

        public IDatabaseRepository<Unit> Units { get; private set; }
        public IDatabaseRepository<UnitImages> UnitImages { get; private set; }
        public IDatabaseRepository<Developer> Developers { get; private set; }
        public IDatabaseRepository<UnitDescription> UnitDescriptions { get; private set; }
        public IDatabaseRepository<UnitView> UnitViews { get; private set; }
        public IDatabaseRepository<LocationRoi> LocationRois { get; private set; }
        public IDatabaseRepository<ApplicationUser> ApplicationUsers { get; private set; }
        public IAccountRepository Accounts { get; private set; }
        public UnitOFWork(ApplicationDbContext context , IEmailService emailService , IConfiguration configuration
          , UserManager<ApplicationUser> userManager)
        {
             this.context = context;
            this.emailService = emailService;
            Accounts = new AccountRepository(userManager, context, configuration, emailService);
            UnitImages = new DatabaseRepository<UnitImages>(context);
            UnitDescriptions=new DatabaseRepository<UnitDescription>(context);
            UnitViews=new DatabaseRepository<UnitView>(context);
            Units=new DatabaseRepository<Unit>(context);
            Developers= new DatabaseRepository<Developer>(context);
			LocationRois = new DatabaseRepository<LocationRoi>(context);
			ApplicationUsers = new DatabaseRepository<ApplicationUser>(context);
        }

        public int Compelet()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }


    }
}
