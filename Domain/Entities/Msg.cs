using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class Msg
    {
        public int MsgId { get; set; }

        [Required]
        public string Code { get; set; }
        
        public string MsgRu { get; set; }
        
        public string MsgEn { get; set; }
    }

}
