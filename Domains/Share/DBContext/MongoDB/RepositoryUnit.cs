
using AutoMapper;
using Common.Options;
using DBContext.MongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Common;
using MongoDB.Driver;

namespace DBContext.MongoDB
{
    public interface IRepositoryUnit
    {
        IMongoDBRepository MongoRepository { get; set; }
        DatabaseContext _mongoDBContext { get; }
        IClientSessionHandle Session { get; set; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        IMongoDBGenericRepository<tb_users> UserRepository { get; set; }
        IMongoDBGenericRepository<tb_datas> DataRepository { get; set; }
    }
    public class RepositoryUnit : IRepositoryUnit, IDisposable
    {
        public DatabaseContext _mongoDBContext { get; private set; }
        private readonly IMapper mapper;

        public RepositoryUnit(IOptions<Common.Options.MongoDBOptions> mongoDBOption)
        {
            _mongoDBContext = new DatabaseContext(mongoDBOption.Value.ConnectionString, mongoDBOption.Value.DatabaseName, mongoDBOption.Value.IsSSL);
        }

        public RepositoryUnit(IOptions<Common.Options.MongoDBOptions> mongoDBOption, IMapper mapper)
        {
            _mongoDBContext = new DatabaseContext(mongoDBOption.Value.ConnectionString, mongoDBOption.Value.DatabaseName, mongoDBOption.Value.IsSSL);
            this.mapper = mapper;
        }

        public RepositoryUnit(IOptions<MongoDBOptions> mongoDBOption, string collectionName)
        {
            _mongoDBContext = new DatabaseContext(mongoDBOption.Value.ConnectionString, mongoDBOption.Value.DatabaseName, mongoDBOption.Value.IsSSL);
        }

        private IMongoDBRepository mongoRepository;
        public IMongoDBRepository MongoRepository
        {
            get { return mongoRepository ?? (mongoRepository = new MongoDBRepository(_mongoDBContext)); }
            set { mongoRepository = value; }
        }

        private IMongoDBGenericRepository<tb_users> userRepository;
        public IMongoDBGenericRepository<tb_users> UserRepository
        {
            get { return userRepository ?? (userRepository = new MongoDBGenericRepository<tb_users>(_mongoDBContext, mapper)); }
            set { userRepository = value; }
        }

        private IMongoDBGenericRepository<tb_datas> dataRepository;
        public IMongoDBGenericRepository<tb_datas> DataRepository
        {
            get { return dataRepository ?? (dataRepository = new MongoDBGenericRepository<tb_datas>(_mongoDBContext, mapper)); }
            set { dataRepository = value; }
        }

        #region Structure
        public void Dispose()
        {
            // Method intentionally left empty.
        }

        private bool IsOpenTransaction = false;
        public IClientSessionHandle Session { get; set; }
        public void BeginTransaction()
        {
            if (Session == null)
            {
                Session = _mongoDBContext.MongoClient.StartSession();
                Session.StartTransaction(new TransactionOptions(
                             readConcern: ReadConcern.Snapshot,
                             writeConcern: WriteConcern.WMajority));
            }

            IsOpenTransaction = true;
        }

        public void CommitTransaction()
        {
            if (!IsOpenTransaction)
                throw new Exception("The transaction does not opened.");

            if (Session != null)
            {
                Session.CommitTransaction();
                Session = null;
            }
            IsOpenTransaction = false;
        }

        public void RollbackTransaction()
        {
            if (!IsOpenTransaction)
                throw new Exception("The transaction does not opened.");

            if (Session != null)
            {
                Session.AbortTransaction();
                Session = null;
            }

            IsOpenTransaction = false;
        }
        #endregion
    }
}
