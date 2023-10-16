using Common.Models;
using MongoDB.Bson;

namespace DBContext.MongoDB.Models
{
    public class tb_users
    {
        public ObjectId? id { get; set; }
        public string frist_name { get; set; }
        public string last_name { get; set; }
        public string user_row { get; set; }
        public CreateInfoSimply create_info { get; set; }
        public CreateInfoSimply update_info { get; set; }
    }
}
