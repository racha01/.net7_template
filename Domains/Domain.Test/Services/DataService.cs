
using Common.Models;
using DBContext.MongoDB;
using DBContext.MongoDB.Models;
using Domain.Test.Models;
using MongoDB.Bson;
using Proxy.Line.Authorization.Core.Interfaces;
using Proxy.Line.V1;

namespace Domain.Test.Services
{
    public interface IDataService
    {
        Task<PaginationModel<DataModel>> GetDataAsync(int? pageNo, int? pageSize);
        Task<DataModel> GetDataByIdAsync(ObjectId id);
        Task<string> CreateDataAsync(CreateDataModel objData);
    }
    public class DataService : IDataService
    {
        private readonly IRepositoryUnit _repositoryUnit;
        private readonly ILineProxy _lineProxy;
        private readonly IOAuthLineManager _oAuthLineManager;
        public readonly CancellationToken cancellationToken = new CancellationToken();

        public DataService(IRepositoryUnit repositoryUnit, ILineProxy lineProxy, IOAuthLineManager oAuthLineManager)
        {
            _repositoryUnit = repositoryUnit;
            _lineProxy = lineProxy;
            _oAuthLineManager = oAuthLineManager;
        }

        #region Get

        public async Task<PaginationModel<DataModel>> GetDataAsync(int? pageNo, int? pageSize)
        {
            var datas = await _repositoryUnit.DataRepository.GetPagingAsync(_ => true, pageNo.Value, pageSize.Value);

            string accessToken = await _oAuthLineManager.GetAccessTokenAsync(cancellationToken);
            var profileData = await _lineProxy.GetProfileAsync(accessToken, "Ufac0abfa0b67c229c49d2561a479883e", cancellationToken);

            var dataPaginationModel = new PaginationModel<DataModel>()
            {
                TotalRecords = datas.TotalRecords,
                PageNo = datas.PageNo,
                PageSize = datas.PageSize,
                TotalPages = datas.TotalPages,
                HasPreviousPage = datas.HasPreviousPage,
                HasNextPage = datas.HasNextPage,
                Items = datas.Items.Select(s => new DataModel()
                {
                    Id = s.id.ToString(),
                    Data = profileData.PictureUrl
                }).ToList<DataModel>(),
            };

            return dataPaginationModel;
        }

        public async Task<DataModel> GetDataByIdAsync(ObjectId id)
        {
            var dataDoc = await _repositoryUnit.DataRepository.FindSingleAsync(x => x.id.Equals(id));

            var dataModel = new DataModel()
            {
                Id = id.ToString(),
                Data = dataDoc.data
            };

            return dataModel;

        }
        #endregion

        #region Create

        public async Task<string> CreateDataAsync(CreateDataModel objData)
        {

            var createData = new tb_datas()
            {
                data = objData.Data,
                update_info = new CreateInfoSimply
                {
                    user_id = "user_id",
                    user_name = "user_name",
                    timestamp = DateTime.Now
                },
                create_info = new CreateInfoSimply
                {
                    user_id = "user_id",
                    user_name = "user_name",
                    timestamp = DateTime.Now
                }
            };

            await _repositoryUnit.DataRepository.AddAsync(createData);

            return createData.id.ToString();

        }
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}
