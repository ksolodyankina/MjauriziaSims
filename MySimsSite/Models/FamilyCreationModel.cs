using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Web;
using MjauriziaSims.MessageManager;

namespace WebUI.Models
{
    public class FamilyCreationModel
    {
        public Family Family { get; set; }
        public IEnumerable<InheritanceLaw> InheritanceLaws { get; set; }
        public MessageManager MsgManager { get; set; }
        
        public List<SelectListItem> GetInheritanceSelectCategory(int category)
        {
            var items = new List<SelectListItem>();

            foreach (var law in InheritanceLaws.Where(l => (int)l.Category == category))
            {
                items.Add(new SelectListItem { Text = MsgManager.Msg(law.Title), Value = law.InheritanceId.ToString() });
            }
            return items;
        }


    }
}