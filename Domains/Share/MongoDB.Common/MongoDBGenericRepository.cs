using AutoMapper;
using Common.Helpers;
using Common.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MongoDB.Common
{
    public interface IMongoDBGenericRepository<T> where T : class
    {
        IMongoCollection<T> Collection { get; }
        IMongoCollection<BsonDocument> CollectionDynamic { get; }

        Task AddAsync(T item);
        Task AddRangeAsync(IEnumerable<T> list);
        Task AddRangeAsync(IClientSessionHandle session, IEnumerable<T> list);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(BsonDocument where, FindOptions findOptions = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        Task DeleteAsync(object key);
        Task DeleteAsync(IClientSessionHandle session, FilterDefinition<T> where, DeleteOptions options = null);
        Task DeleteAsync(Expression<Func<T, bool>> where);
        IMongoQueryable<T> FindAll();
        IMongoQueryable<T> FindAll(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAllAsync(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection);
        Task<List<T2>> FindAllItemAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where, string field);
        Task<T> FindSingleAsync(FilterDefinition<BsonDocument> where, FindOptions findOptions);
        Task<T> FindSingleAsync(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> where);
        Task<T> FindSingleAsync(FilterDefinition<BsonDocument> where);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> where, FindOptions findOptions);
        Task<PaginationModel<T>> GetPaginationFilterAsync(FilterDefinition<BsonDocument> where, int pageIndex, int pageSize);
        Task<T> SelectAsync(object key);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where);
        Task DeleteAsync(FilterDefinition<T> where);
        Task UpdateAsync(T item, object key);
        Task UpdateAsync(IClientSessionHandle session, T item, object key);
        void ReplaceOne(Expression<Func<T, bool>> expression, T update, bool IsUpsert = false);
        Task<IEnumerable<T>> FindAllAsync(FilterDefinition<T> filter);
        Task<List<T2>> FindAllAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection, string field);
        Task<List<T2>> FindAllAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection);
        Task ReplaceOneAsync(Expression<Func<T, bool>> expression, T update, bool IsUpsert = false);
        Task<T2> FindSingleAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection, string field);
        Task<T2> SingleOrDefaultItemAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where, string field);
        Task<T2> SingleOrDefaultItemAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection, string field);
        Task<bool> AnyAsync(FilterDefinition<BsonDocument> where);
        Task<PaginationModel<T>> GetPaginationFilterAsync(FilterDefinition<BsonDocument> where,
            SortDefinition<BsonDocument> sort, int pageIndex, int pageSize, FindOptions findOptions);
        Task<PaginationModel<T>> GetPaginationFilterAsync(FilterDefinition<BsonDocument> where, SortDefinition<BsonDocument> sort, int pageIndex, int pageSize);
        Task<PaginationModel<T>> GetPaginationFilterAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument> projection, int pageIndex, int pageSize);
        Task<PaginationModel<T>> GetPagingAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize);
        Task<PaginationModel<T>> GetPagingQueryAsync(IMongoQueryable<T> query, int pageIndex, int pageSize);
        IMongoCollection<T> GetCollection();
        Task<PaginationModel<T>> GetPaginationAsync(string query, int pageIndex, int pageSize);
        Task<PaginationModel<T2>> GetPaginationAsync<T2>(string query, int pageIndex, int pageSize);
        Task<List<T2>> FindDistinct<T2>(FieldDefinition<BsonDocument, T2> field, FilterDefinition<BsonDocument> filter);
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<BsonDocument> field, UpdateDefinition<BsonDocument> fieldUpdate, UpdateOptions options = null);
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options = null);
        Task AddItemsAsync(FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task<T> AddAsync(IClientSessionHandle session, T item);
        Task<UpdateResult> UpdateOneAsync(IClientSessionHandle session, FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options = null);
        Task AddItemsAsync(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update);
        Task DeleteAsync(IClientSessionHandle session, Expression<Func<T, bool>> where);
        Task<PaginationModel<BsonDocument>> GetPaginationFilterSpecialAsync(FilterDefinition<BsonDocument> where, SortDefinition<BsonDocument> sort, int pageIndex, int pageSize);
        Task<UpdateResult> UpdateManyAsync(IClientSessionHandle session, FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options = null);
        Task<bool> ShellQueryAnyAsync(string query);
        Task<T> ShellQueryAsync(string query);
        Task<List<T2>> ShellQueryListAsync<T2>(string query);
        Task<List<T2>> ShellQueryAllAsync<T2>(string query);
        Task<List<T2>> FindAllItemsFilterAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where, string field);
        Task<T2> ShellQueryAsync<T2>(string query);
        Task<List<T2>> FindAllItemAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where);
        Task<T> ShellFindQueryAsync(string query);
        Task<List<T>> ShellFindListQueryAsync(string query);
        Task<List<T>> ShellFindListQueryAsync(string query, int pageIndex, int pageSize);
        Task<List<T>> ShellFindListQueryAsync(string query, SortDefinition<T> sort, int pageIndex, int pageSize);
        Task<long> ShellCountQueryAsync(string query);
        Task<UpdateResult> UpdateManyOptionsAsync(IClientSessionHandle session, FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options);
        Task<List<T>> FindAllFillterAsync(FilterDefinition<BsonDocument> where);
        Task<PaginationModel<T2>> GetPaginationRecommendAsync<T2>(string query, int pageIndex, int pageSize);
        Task<UpdateResult> UpdateManyOptionsAsync(FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options);
    }

    public class MongoDBGenericRepository<T> : IMongoDBGenericRepository<T> where T : class
    {
        private readonly IMapper mapper;
        public MongoDBGenericRepository(DatabaseContext context, IMapper mapper)
        {
            Database = context.MongoDatabase;
            _collection = Database.GetCollection<T>(typeof(T).Name.ToLower().Replace("tb_", ""));
            _collectionDynamic = Database.GetCollection<BsonDocument>(typeof(T).Name.ToLower().Replace("tb_", ""));
            this.mapper = mapper;
            var instanceField = typeof(MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer).GetField("__instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, new AlwaysAllowDuplicateNamesBsonDocumentSerializer());
        }

        public MongoDBGenericRepository(DatabaseContext context, string collectionName)
        {
            Database = context.MongoDatabase;
            _collection = Database.GetCollection<T>(collectionName.Replace("tb_", ""));
            _collectionDynamic = Database.GetCollection<BsonDocument>(collectionName.Replace("tb_", ""));
            var instanceField = typeof(MongoDB.Bson.Serialization.Serializers.BsonDocumentSerializer).GetField("__instance", BindingFlags.Static | BindingFlags.NonPublic);
            instanceField.SetValue(null, new AlwaysAllowDuplicateNamesBsonDocumentSerializer());
        }

        private IMongoCollection<T> _collection { get; }
        private IMongoCollection<BsonDocument> _collectionDynamic { get; }

        private IMongoDatabase Database { get; }
        public IMongoCollection<T> GetCollection() => _collection;
        public IMongoCollection<T> Collection => _collection;
        public IMongoCollection<BsonDocument> CollectionDynamic => _collectionDynamic;

        public Task AddAsync(T item)
        {
            return _collection.InsertOneAsync(item);
        }

        public Task AddRangeAsync(IEnumerable<T> list)
        {
            return _collection.InsertManyAsync(list);
        }
        public Task AddRangeAsync(IClientSessionHandle session, IEnumerable<T> list)
        {
            return _collection.InsertManyAsync(session, list);
        }

        public Task<bool> AnyAsync()
        {
            return _collection.Find(new BsonDocument()).AnyAsync();
        }

        public async Task<bool> AnyAsync(FilterDefinition<BsonDocument> where)
        {
            var obj = await _collectionDynamic.Find(where).AnyAsync();
            return obj;
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return _collection.Find(where).AnyAsync();
        }

        public Task DeleteAsync(object key)
        {
            return _collection.DeleteOneAsync(FilterId(key));
        }

        public Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            return _collection.DeleteManyAsync(where);
        }
        public Task DeleteAsync(FilterDefinition<T> where)
        {
            return _collection.DeleteManyAsync(where);
        }

        public virtual IMongoQueryable<T> FindAll()
        {
            return _collection.AsQueryable();
        }

        public virtual IMongoQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _collection.AsQueryable().Where(predicate);
        }
        public async Task<IEnumerable<T>> FindAllAsync(FilterDefinition<T> filter)
        {
            return await _collection.Find(filter).ToListAsync().ConfigureAwait(false);
        }
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync().ConfigureAwait(false);
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> where)
        {
            return await _collection.Find(where).FirstOrDefaultAsync();
        }

        public async Task<T> FindSingleAsync(FilterDefinition<BsonDocument> where)
        {
            var obj = await _collectionDynamic.Find(where).FirstOrDefaultAsync();
            if (obj != null)
            {
                return BsonSerializer.Deserialize<T>(obj);
            }
            return null;
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> where)
        {
            return await _collection.Find(where).ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<T>> FindAllAsync(FilterDefinition<BsonDocument> where)
        {
            var obj = await _collectionDynamic.Find(where).ToListAsync();
            if (obj != null)
            {
                List<T> items = obj.Select(p => BsonSerializer.Deserialize<T>(p)).ToList();
                return items;
            }
            return null;
        }

        public Task<T> SelectAsync(object key)
        {
            return _collection.Find(FilterId(key)).SingleOrDefaultAsync();
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return _collection.Find(where).SingleOrDefaultAsync();
        }

        public Task UpdateAsync(T item, object key)
        {
            return _collection.ReplaceOneAsync(FilterId(key), item);
        }

        public Task UpdateAsync(IClientSessionHandle session, T item, object key)
        {
            return _collection.ReplaceOneAsync(session, FilterId(key), item);
        }
        private static FilterDefinition<T> FilterId(object key)
        {
            return Builders<T>.Filter.Eq("_id", key);
        }

        public void ReplaceOne(Expression<Func<T, bool>> expression, T update, bool IsUpsert = false)
        {
#pragma warning disable CS0618 // 'IMongoCollection<T>.ReplaceOne(FilterDefinition<T>, T, UpdateOptions, CancellationToken)' is obsolete: 'Use the overload that takes a ReplaceOptions instead of an UpdateOptions.'
            if (IsUpsert) _collection.ReplaceOne(expression, update, new UpdateOptions { IsUpsert = true });
#pragma warning restore CS0618 // 'IMongoCollection<T>.ReplaceOne(FilterDefinition<T>, T, UpdateOptions, CancellationToken)' is obsolete: 'Use the overload that takes a ReplaceOptions instead of an UpdateOptions.'
            else _collection.ReplaceOne(expression, update);
        }

        public async Task ReplaceOneAsync(Expression<Func<T, bool>> expression, T update, bool IsUpsert = false)
        {
            if (IsUpsert) await _collection.ReplaceOneAsync(expression, update, new ReplaceOptions { IsUpsert = true });
            else await _collection.ReplaceOneAsync(expression, update);
        }

        public async Task<PaginationModel<T>> GetPagingAsync(Expression<Func<T, bool>> predicate, int pageIndex, int pageSize)
        {
            var source = _collection.AsQueryable().Where(predicate);
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var objPaging = new MongoDB.Common.Models.PagingDB<T>(items, count, pageIndex, pageSize);
            return new PaginationModel<T>()
            {
                TotalRecords = objPaging.TotalCount,
                TotalPages = objPaging.TotalPages,
                PageNo = objPaging.PageIndex,
                PageSize = objPaging.PageSize,
                HasPreviousPage = objPaging.HasPreviousPage,
                HasNextPage = objPaging.HasNextPage,
                Items = objPaging
            };
        }

        public async Task<PaginationModel<T>> GetPagingQueryAsync(IMongoQueryable<T> query, int pageIndex, int pageSize)
        {
            var count = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var objPaging = new MongoDB.Common.Models.PagingDB<T>(items, count, pageIndex, pageSize);
            return new PaginationModel<T>()
            {
                TotalRecords = objPaging.TotalCount,
                TotalPages = objPaging.TotalPages,
                PageNo = objPaging.PageIndex,
                PageSize = objPaging.PageSize,
                HasPreviousPage = objPaging.HasPreviousPage,
                HasNextPage = objPaging.HasNextPage,
                Items = objPaging
            };
        }

        public async Task<PaginationModel<T>> GetPaginationAsync(string query, int pageIndex, int pageSize)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            var pipelineCount = pipeline.Count();
            pipeline = pipeline.Skip((pageIndex - 1) * pageSize).Limit(pageSize);
            var countTask = _collection.AggregateAsync(pipelineCount, new AggregateOptions() { AllowDiskUse = true });
            var sourceTask = _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true });
            var source = await sourceTask.GetAwaiter().GetResult().ToListAsync();
            var sourceCount = await countTask.GetAwaiter().GetResult().FirstOrDefaultAsync();
            var count = sourceCount == null ? 0 : sourceCount.Count;
            if (source != null && source.Any())
            {
                var items = new List<T>();
                source.ForEach(q => items.Add(BsonSerializer.Deserialize<T>(q.ToJson())));
                var objPaging = new MongoDB.Common.Models.PagingDB<T>(items, (int)count, pageIndex, pageSize);
                return new PaginationModel<T>()
                {
                    TotalRecords = objPaging.TotalCount,
                    TotalPages = objPaging.TotalPages,
                    PageNo = objPaging.PageIndex,
                    PageSize = objPaging.PageSize,
                    HasPreviousPage = objPaging.HasPreviousPage,
                    HasNextPage = objPaging.HasNextPage,
                    Items = objPaging
                };
            }
            return new PaginationModel<T>();
        }

        public async Task<PaginationModel<T2>> GetPaginationAsync<T2>(string query, int pageIndex, int pageSize)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            var pipelineCount = pipeline.Count();
            pipeline = pipeline.Skip((pageIndex - 1) * pageSize).Limit(pageSize);
            var countTask = _collection.AggregateAsync(pipelineCount, new AggregateOptions() { AllowDiskUse = true });
            var sourceTask = _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true });
            var source = await sourceTask.GetAwaiter().GetResult().ToListAsync();
            var sourceCount = await countTask.GetAwaiter().GetResult().FirstOrDefaultAsync();
            var count = sourceCount == null ? 0 : sourceCount.Count;
            if (source != null && source.Any())
            {
                var items = new List<T2>();
                source.ForEach(q => items.Add(BsonSerializer.Deserialize<T2>(q.ToJson())));
                var objPaging = new MongoDB.Common.Models.PagingDB<T2>(items, (int)count, pageIndex, pageSize);
                return new PaginationModel<T2>()
                {
                    TotalRecords = objPaging.TotalCount,
                    TotalPages = objPaging.TotalPages,
                    PageNo = objPaging.PageIndex,
                    PageSize = objPaging.PageSize,
                    HasPreviousPage = objPaging.HasPreviousPage,
                    HasNextPage = objPaging.HasNextPage,
                    Items = objPaging
                };
            }
            return new PaginationModel<T2>();
        }
        public async Task<PaginationModel<T>> GetPaginationFilterAsync(FilterDefinition<BsonDocument> where,
            SortDefinition<BsonDocument> sort, int pageIndex, int pageSize, FindOptions findOptions)
        {
            var source = _collectionDynamic.Find(where, findOptions).Sort(sort);
            var totalRecordsTask = source.CountDocumentsAsync();
            var pageItemsTask = source.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            int totalRecords = Convert.ToInt32(await totalRecordsTask);
            var pageItems = await pageItemsTask;
            List<T> items = pageItems.Select(p => BsonSerializer.Deserialize<T>(p)).ToList();
            var objPaging = new MongoDB.Common.Models.PagingDB<T>(items, totalRecords, pageIndex, pageSize);
            return new PaginationModel<T>()
            {
                TotalRecords = objPaging.TotalCount,
                TotalPages = objPaging.TotalPages,
                PageNo = objPaging.PageIndex,
                PageSize = objPaging.PageSize,
                HasPreviousPage = objPaging.HasPreviousPage,
                HasNextPage = objPaging.HasNextPage,
                Items = objPaging
            };
        }
        public async Task<PaginationModel<T>> GetPaginationFilterAsync(FilterDefinition<BsonDocument> where, int pageIndex, int pageSize)
        {
            var source = _collectionDynamic.Find(where);
            var totalRecordsTask = source.CountDocumentsAsync();
            var pageItemsTask = source.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            int totalRecords = Convert.ToInt32(await totalRecordsTask);
            var pageItems = await pageItemsTask;
            List<T> items = pageItems.Select(p => BsonSerializer.Deserialize<T>(p)).ToList();
            var objPaging = new MongoDB.Common.Models.PagingDB<T>(items, totalRecords, pageIndex, pageSize);
            return new PaginationModel<T>()
            {
                TotalRecords = objPaging.TotalCount,
                TotalPages = objPaging.TotalPages,
                PageNo = objPaging.PageIndex,
                PageSize = objPaging.PageSize,
                HasPreviousPage = objPaging.HasPreviousPage,
                HasNextPage = objPaging.HasNextPage,
                Items = objPaging
            };
        }
        public async Task<PaginationModel<T>> GetPaginationFilterAsync(FilterDefinition<BsonDocument> where,
            SortDefinition<BsonDocument> sort, int pageIndex, int pageSize)
        {
            var source = _collectionDynamic.Find(where).Sort(sort);
            var totalRecordsTask = source.CountDocumentsAsync();
            var pageItemsTask = source.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            int totalRecords = Convert.ToInt32(await totalRecordsTask);
            var pageItems = await pageItemsTask;
            List<T> items = pageItems.Select(p => BsonSerializer.Deserialize<T>(p)).ToList();
            var objPaging = new MongoDB.Common.Models.PagingDB<T>(items, totalRecords, pageIndex, pageSize);
            return new PaginationModel<T>()
            {
                TotalRecords = objPaging.TotalCount,
                TotalPages = objPaging.TotalPages,
                PageNo = objPaging.PageIndex,
                PageSize = objPaging.PageSize,
                HasPreviousPage = objPaging.HasPreviousPage,
                HasNextPage = objPaging.HasNextPage,
                Items = objPaging
            };
        }

        public async Task<PaginationModel<BsonDocument>> GetPaginationFilterSpecialAsync(FilterDefinition<BsonDocument> where,
            SortDefinition<BsonDocument> sort, int pageIndex, int pageSize)
        {
            var source = _collectionDynamic.Find(where).Sort(sort);
            var totalRecordsTask = source.CountDocumentsAsync();
            var pageItemsTask = source.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            int totalRecords = Convert.ToInt32(await totalRecordsTask);
            var pageItems = await pageItemsTask;
            var objPaging = new MongoDB.Common.Models.PagingDB<BsonDocument>(pageItems, totalRecords, pageIndex, pageSize);
            return new PaginationModel<BsonDocument>()
            {
                TotalRecords = objPaging.TotalCount,
                TotalPages = objPaging.TotalPages,
                PageNo = objPaging.PageIndex,
                PageSize = objPaging.PageSize,
                HasPreviousPage = objPaging.HasPreviousPage,
                HasNextPage = objPaging.HasNextPage,
                Items = objPaging
            };

        }
        public async Task<PaginationModel<T>> GetPaginationFilterAsync<T2>(FilterDefinition<BsonDocument> where
            , ProjectionDefinition<BsonDocument> projection, int pageIndex, int pageSize)
        {
            var source = _collectionDynamic.Find(where).Project<T>(projection);
            var totalRecordsTask = source.CountDocumentsAsync();
            var pageItemsTask = source.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();

            int totalRecords = Convert.ToInt32(await totalRecordsTask);
            var pageItems = await pageItemsTask;
            List<T> items = pageItems.ToList();
            var objPaging = new MongoDB.Common.Models.PagingDB<T>(items, totalRecords, pageIndex, pageSize);
            return new PaginationModel<T>()
            {
                TotalRecords = objPaging.TotalCount,
                TotalPages = objPaging.TotalPages,
                PageNo = objPaging.PageIndex,
                PageSize = objPaging.PageSize,
                HasPreviousPage = objPaging.HasPreviousPage,
                HasNextPage = objPaging.HasNextPage,
                Items = objPaging
            };
        }
        public async Task<List<T2>> FindDistinct<T2>(FieldDefinition<BsonDocument, T2> field, FilterDefinition<BsonDocument> filter)
        {
            var obj = await _collectionDynamic.DistinctAsync(field, filter);
            var item = obj.ToList();
            if (obj != null)
            {
                return item;
            }
            return null;
        }
        public async Task<T2> SingleOrDefaultItemAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where, string field)
        {
            var data = await _collectionDynamic.Aggregate(where).FirstOrDefaultAsync();
            if (data != null)
                return BsonSerializer.Deserialize<T2>(data[field].ToJson());
            return default;
        }

        public async Task<T2> FindSingleAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection, string field)
        {
            var data = await _collectionDynamic.Find(where).Project(projection).FirstOrDefaultAsync();
            if (data != null)
            {
                //var document = BsonDocument.Create(data);
                var document = data.ToBsonDocument();
                return BsonSerializer.Deserialize<List<T2>>(document[field].ToJson()).FirstOrDefault();
                //var jData = JObject.FromObject(data);
                //var targetData = jData[field];
                //int totalRecords = targetData.Count();
                //var items = mapper.Map<List<T2>>(targetData);
                //return items.FirstOrDefault();
            }
            return default;
        }

        public async Task<T> FindSingleAsync(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection)
        {
            var data = await _collectionDynamic.Find(where).Project(projection).FirstOrDefaultAsync();
            return data;
        }

        public async Task<UpdateResult> UpdateOneAsync(FilterDefinition<BsonDocument> field, UpdateDefinition<BsonDocument> fieldUpdate, UpdateOptions options = null)
        {
            return await _collectionDynamic.UpdateOneAsync(field, fieldUpdate, options);
        }

        public async Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options = null)
        {
            return await _collection.UpdateOneAsync(field, fieldUpdate, options);
        }

        public Task AddItemsAsync(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return _collection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task<List<T2>> FindAllItemAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where, string field)
        {
            var results = new List<T2>();
            var data = await _collectionDynamic.Aggregate(where).ToListAsync();
            if (data != null && data.Any())
                data.ForEach(q => results.Add(BsonSerializer.Deserialize<T2>(q[field].ToJson())));
            return results;
        }
        public async Task<T> AddAsync(IClientSessionHandle session, T item)
        {
            await _collection.InsertOneAsync(session, item);
            return item;
        }

        public async Task<UpdateResult> UpdateOneAsync(IClientSessionHandle session, FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options = null)
        {
            return await _collection.UpdateOneAsync(session, field, fieldUpdate, options);
        }

        public Task AddItemsAsync(IClientSessionHandle session, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return _collection.FindOneAndUpdateAsync(session, filter, update);
        }

        public Task DeleteAsync(IClientSessionHandle session, Expression<Func<T, bool>> where)
        {
            return _collection.DeleteManyAsync(session, where);
        }
        public async Task<UpdateResult> UpdateManyAsync(IClientSessionHandle session, FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options = null)
        {
            return await _collection.UpdateManyAsync(session, field, fieldUpdate, new UpdateOptions { IsUpsert = true });
        }
        public async Task<List<T2>> FindAllAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection, string field)
        {
            var data = await _collectionDynamic.Find(where).Project(projection).FirstOrDefaultAsync();
            if (data != null)
            {
                //var document = BsonDocument.Create(data);
                var document = data.ToBsonDocument();
                return BsonSerializer.Deserialize<List<T2>>(document[field].ToJson());
                //var jData = JObject.FromObject(data);
                //var targetData = jData[field];
                //int totalRecords = targetData.Count();
                //var type_ = typeof(T2);
                //var items = mapper.Map<List<T2>>(targetData);
                //return items;
            }
            return default;
        }
        public async Task<List<T2>> FindAllAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection)
        {
            var data = await _collectionDynamic.Find(where).Project(projection).ToListAsync();
            if (data != null)
            {
                var items = mapper.Map<List<T2>>(data);
                return items;
            }
            return default;
        }
        public async Task<T2> SingleOrDefaultItemAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection, string field)
        {
            var data = await _collectionDynamic.Find(where).Project(projection).FirstOrDefaultAsync();
            if (data != null)
            {
                //var document = BsonDocument.Create(data);
                var document = data.ToBsonDocument();
                var values = BsonSerializer.Deserialize<List<T2>>(document[field].ToJson());
                return values.FirstOrDefault();
                //var jData = JObject.FromObject(data);
                //var targetData = jData[field];
                //var item = mapper.Map<T2>(targetData.FirstOrDefault());
                //return item;
            }
            return default;
        }

        public async Task<bool> ShellQueryAnyAsync(string query)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            return await _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true }).GetAwaiter().GetResult().AnyAsync();
        }

        public async Task<T> ShellQueryAsync(string query)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            var source = await _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true }).GetAwaiter().GetResult().FirstOrDefaultAsync();
            if (source != null)
                return BsonSerializer.Deserialize<T>(source.ToJson());
            else
                return default;
        }

        public async Task<T2> ShellQueryAsync<T2>(string query)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            var source = await _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true }).GetAwaiter().GetResult().FirstOrDefaultAsync();
            if (source != null)
                return BsonSerializer.Deserialize<T2>(source.ToJson());
            else
                return default;
        }

        public async Task<List<T>> ShellQueryListAsync(string query)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            var sources = await _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true }).GetAwaiter().GetResult().ToListAsync();
            if (sources != null && sources.Any())
            {
                var items = new List<T>();
                sources.ForEach(q => items.Add(BsonSerializer.Deserialize<T>(q.ToJson())));
                return items;
            }
            else
                return default;
        }

        public async Task<List<T2>> ShellQueryListAsync<T2>(string query)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            var sources = await _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true }).GetAwaiter().GetResult().ToListAsync();
            if (sources != null && sources.Any())
            {
                var items = new List<T2>();
                sources.ForEach(q => items.Add(BsonSerializer.Deserialize<T2>(q.ToJson())));
                return items;
            }
            else
                return default;
        }

        public async Task<List<T2>> FindAllItemsFilterAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where, string field)
        {
            var projectionBuilders = Builders<BsonDocument>.Projection;
            var projectionDefinition = new List<ProjectionDefinition<BsonDocument>>();
            where.Project(projectionBuilders.Combine(projectionDefinition));
            var items = new List<T2>();
            var data = await _collectionDynamic.Aggregate(where).ToListAsync();
            if (data != null)
            {
                if (data != null && data.Any())
                    data.ForEach(q => items.Add(BsonSerializer.Deserialize<T2>(q[field].ToJson())));
            }
            return items;
        }
        public async Task<List<T2>> ShellQueryAllAsync<T2>(string query)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<BsonDocument, BsonDocument>.Create(stage);
            var sources = await _collectionDynamic.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true }).GetAwaiter().GetResult().ToListAsync();
            if (sources != null && sources.Any())
            {
                var items = new List<T2>();
                sources.ForEach(q => items.Add(BsonSerializer.Deserialize<T2>(q.ToJson())));
                return items;
            }
            else
                return default;
        }
        public async Task<List<T2>> FindAllItemAsync<T2>(PipelineDefinition<BsonDocument, BsonDocument> where)
        {
            var results = new List<T2>();
            var data = await _collectionDynamic.Aggregate(where).ToListAsync();
            if (data != null && data.Any())
                data.ForEach(q => results.Add(BsonSerializer.Deserialize<T2>(q.ToJson())));
            return results;
        }

        public async Task<T> ShellFindQueryAsync(string query)
        {
            BsonDocument where = BsonSerializer.Deserialize<BsonDocument>(query);
            var data = await _collection.Find(where).FirstOrDefaultAsync();
            if (data != null)
                return BsonSerializer.Deserialize<T>(data.ToJson());
            else
                return default;
        }

        public async Task<List<T>> ShellFindListQueryAsync(string query)
        {
            BsonDocument where = BsonSerializer.Deserialize<BsonDocument>(query);
            var data = await _collection.Find(where).ToListAsync();
            if (data != null) return data;
            return default;
        }

        public async Task<List<T>> ShellFindListQueryAsync(string query, int pageIndex, int pageSize)
        {
            BsonDocument where = BsonSerializer.Deserialize<BsonDocument>(query);
            var data = await _collection.Find(where).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            if (data != null) return data;
            return default;
        }

        public async Task<List<T>> ShellFindListQueryAsync(string query, SortDefinition<T> sort, int pageIndex, int pageSize)
        {
            BsonDocument where = BsonSerializer.Deserialize<BsonDocument>(query);
            var data = await _collection.Find(where).Sort(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            if (data != null) return data;
            return default;
        }

        public async Task<long> ShellCountQueryAsync(string query)
        {
            BsonDocument where = BsonSerializer.Deserialize<BsonDocument>(query);
            return await _collection.CountDocumentsAsync(where);
        }

        public async Task<bool> AnyAsync(BsonDocument where, FindOptions findOptions = null)
        {
            if (findOptions is null)
            {
                var obj = await _collectionDynamic.Find(where).AnyAsync();
                return obj;
            }
            else
            {
                var obj = await _collectionDynamic.Find(where, findOptions).AnyAsync();
                return obj;
            }

        }

        public async Task<T> FindSingleAsync(FilterDefinition<BsonDocument> where, FindOptions findOptions)
        {
            try
            {
                if (findOptions is null)
                {
                    var obj = await _collectionDynamic.Find(where).FirstOrDefaultAsync();
                    if (obj != null)
                    {
                        return BsonSerializer.Deserialize<T>(obj);
                    }
                }
                else
                {
                    var obj = await _collectionDynamic.Find(where, findOptions).FirstOrDefaultAsync();
                    if (obj != null)
                    {
                        return BsonSerializer.Deserialize<T>(obj);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return null;
        }

        public async Task<IEnumerable<T>> FindAllAsync(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, T> projection)
        {
            return await _collectionDynamic.Find(where).Project(projection).ToListAsync().ConfigureAwait(false);
        }


        public async Task<T2> FindSingleAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, BsonDocument> projection, string field)
        {
            var data = await _collectionDynamic.Find(where).Project(projection).FirstOrDefaultAsync();
            if (data != null)
            {

                return BsonSerializer.Deserialize<List<T2>>(data[field].ToJson()).FirstOrDefault();
                //var jData = JObject.FromObject(data);
                //var targetData = jData[field];
                //int totalRecords = targetData.Count;
                //var items = mapper.Map<List<T2>>(targetData);
                //return items;
            }
            return default;
        }

        public async Task<List<T2>> FindAllAsync<T2>(FilterDefinition<BsonDocument> where, ProjectionDefinition<BsonDocument, BsonDocument> projection, string field)
        {
            var data = await _collectionDynamic.Find(where).Project(projection).FirstOrDefaultAsync();
            if (data != null)
            {
                return BsonSerializer.Deserialize<List<T2>>(data[field].ToJson());
                //var jData = JObject.FromObject(data);
                //var targetData = jData[field];
                //int totalRecords = targetData.Count();
                //var type_ = typeof(T2);
                //var items = mapper.Map<List<T2>>(targetData);
                //return items;
            }
            return default;
        }
        public async Task<List<T>> FindAllFillterAsync(FilterDefinition<BsonDocument> where)
        {
            var obj = await _collectionDynamic.Find(where).ToListAsync();
            if (obj != null)
            {
                List<T> items = obj.Select(p => BsonSerializer.Deserialize<T>(p)).ToList();
                return items;
            }
            return null;
        }
        public Task DeleteAsync(IClientSessionHandle session, FilterDefinition<T> where, DeleteOptions options)
        {
            return _collection.DeleteManyAsync(session, where, options);
        }
        public async Task<UpdateResult> UpdateManyOptionsAsync(IClientSessionHandle session, FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options)
        {
            return await _collection.UpdateManyAsync(session, field, fieldUpdate, options);
        }
        public async Task<UpdateResult> UpdateManyOptionsAsync(FilterDefinition<T> field, UpdateDefinition<T> fieldUpdate, UpdateOptions options)
        {
            return await _collection.UpdateManyAsync(field, fieldUpdate, options);
        }
        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> where, FindOptions findOptions = null)
        {
            return await _collection.Find(where, findOptions).FirstOrDefaultAsync();
        }

        public async Task<PaginationModel<T2>> GetPaginationRecommendAsync<T2>(string query, int pageIndex, int pageSize)
        {
            List<BsonDocument> stage = BsonSerializer.Deserialize<BsonDocument[]>(query).ToList();
            var pipeline = PipelineDefinition<T, BsonDocument>.Create(stage);
            var pipelineCount = pipeline.Count();
            pipeline = pipeline.Skip((pageIndex - 1) * pageSize).Limit(pageSize);
            var countTask = _collection.AggregateAsync(pipelineCount, new AggregateOptions() { AllowDiskUse = true });
            var sourceTask = _collection.AggregateAsync(pipeline, new AggregateOptions() { AllowDiskUse = true });
            var source = await sourceTask.GetAwaiter().GetResult().ToListAsync();
            var sourceCount = await countTask.GetAwaiter().GetResult().FirstOrDefaultAsync();
            var count = sourceCount == null ? 0 : sourceCount.Count;
            if (source != null && source.Any())
            {
                var items = new List<T2>();
                foreach (var item in source)
                {
                    try
                    {
                        items.Add(BsonSerializer.Deserialize<T2>(item.ToJson()));
                    }
                    catch (Exception)
                    { }
                }
                var objPaging = new MongoDB.Common.Models.PagingDB<T2>(items, (int)count, pageIndex, pageSize);
                return new PaginationModel<T2>()
                {
                    TotalRecords = objPaging.TotalCount,
                    TotalPages = objPaging.TotalPages,
                    PageNo = objPaging.PageIndex,
                    PageSize = objPaging.PageSize,
                    HasPreviousPage = objPaging.HasPreviousPage,
                    HasNextPage = objPaging.HasNextPage,
                    Items = objPaging
                };
            }
            return new PaginationModel<T2>();
        }
    }
}