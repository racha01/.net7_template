
using Common.AppSettings.TestAPI;
using DBContext.MongoDB;
using Domain.Test.Services;

namespace Domain.Test.UnitOfWorks
{
    public interface ILogicUnitOfWork
    {
        IDataService DataService { get; set; }
    }
    public class LogicUnitOfWork: ILogicUnitOfWork
    {
        private readonly IRepositoryUnit _repositoryUnit;

        public LogicUnitOfWork(
            IRepositoryUnit repositoryUnit
        )
        {
            _repositoryUnit = repositoryUnit;
        }

        private IDataService dataService;
        public IDataService DataService
        {
            get { return dataService ?? (dataService = new DataService(_repositoryUnit)); }
            set { dataService = value; }
        }

    }
}
