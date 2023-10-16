
using Common.AppSettings.TestAPI;
using DBContext.MongoDB;
using Domain.Test.Services;
using Proxy.Line.Authorization.Core.Interfaces;
using Proxy.Line.V1;

namespace Domain.Test.UnitOfWorks
{
    public interface ILogicUnitOfWork
    {
        IDataService DataService { get; set; }
    }
    public class LogicUnitOfWork: ILogicUnitOfWork
    {
        private readonly IRepositoryUnit _repositoryUnit;
        private readonly ILineProxy _lineProxy;
        private readonly IOAuthLineManager _oAuthLineManager;

        public LogicUnitOfWork(
            IRepositoryUnit repositoryUnit,
            ILineProxy lineProxy,
            IOAuthLineManager oAuthLineManager
        )
        {
            _repositoryUnit = repositoryUnit;
            _lineProxy = lineProxy;
            _oAuthLineManager = oAuthLineManager;
        }

        private IDataService dataService;
        public IDataService DataService
        {
            get { return dataService ?? (dataService = new DataService(_repositoryUnit, _lineProxy, _oAuthLineManager)); }
            set { dataService = value; }
        }

    }
}
