using AutoMapper;
using Common.AppSettings.TestAPI;
using Common.DTOs;
using Common.Model;
using Common.Models;
using Domain.Test.Models;
using Domain.Test.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Service.Test.API.DTOs;

namespace Service.Test.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/datas")]
    public class DatasController : BaseController
    {
        private readonly AppSettings _appSettings;
        private readonly ILogicUnitOfWork _logicUnitOfWork;
        public DatasController(IOptions<AppSettings> appSettings
            , ILogicUnitOfWork logicUnitOfWork
        )
        {
            _logicUnitOfWork = logicUnitOfWork;
            _appSettings = appSettings.Value;
        }

        #region Get

        [HttpGet(Name = "GetDatasAsync")]
        [ProducesResponseType(typeof(PaginationDTO<DataDTO>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(APIStandardErrorResponse), 500)]
        public async Task<IActionResult> GetDatasAsync([FromQuery] int? page_no, [FromQuery] int? page_size)
        {
            page_no = page_no.HasValue && page_no.Value > 0 ? page_no : _appSettings.Pagination.PageNo;
            page_size = page_size.HasValue && page_size.Value > 0 ? page_size : _appSettings.Pagination.PageSize;

            var datas = await _logicUnitOfWork.DataService.GetDataAsync(page_no, page_size);

            var dataPaginationDTO = new PaginationDTO<DataDTO>()
            {
                TotalRecords = datas.TotalRecords,
                PageNo = datas.PageNo,
                PageSize = datas.PageSize,
                TotalPages = datas.TotalPages,
                HasPreviousPage = datas.HasPreviousPage,
                HasNextPage = datas.HasNextPage,
                Items = datas.Items.Select(s => new DataDTO()
                {
                    Id = s.Id,
                    Data = s.Data
                }).ToList<DataDTO>(),
            };

            return APIResponse(dataPaginationDTO);
        }
        #endregion

        [HttpPost(Name = "CreateDatasAsync")]
        [ProducesResponseType(typeof(ResponseCreateDataDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(APIStandardErrorResponse), 500)]
        public async Task<IActionResult> CreateDataAsync([FromBody] CreateDataDTO obj_data)
        {
            var createDataModel = new CreateDataModel()
            {
                Data = obj_data.Data
            };

            var dataId = await _logicUnitOfWork.DataService.CreateDataAsync(createDataModel);

            var responseCreateDataDTO = new ResponseCreateDataDTO() { Id = dataId};

            return APIResponse(responseCreateDataDTO);
        }
        #region Post


        #endregion

        #region Put
        #endregion

        #region Delete
        #endregion

    }
}
