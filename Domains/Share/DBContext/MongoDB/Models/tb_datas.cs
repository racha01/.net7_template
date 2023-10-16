using Common.Models;
using MongoDB.Bson;

namespace DBContext.MongoDB.Models
{
    public class tb_datas
    {
        public ObjectId id { get; set; }
        public string data { get; set; }
        public CreateInfoSimply create_info { get; set; }
        public CreateInfoSimply update_info { get; set; }
    }
}
