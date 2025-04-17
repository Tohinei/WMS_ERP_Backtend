using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WMS_ERP_Backend.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int MenuId { get; set; }
        public string SessionName { get; set; }

        //public List<Session>? SubItems { get; set; }
        //public bool IsTitle { get; set; }
        //public int ParentId { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
