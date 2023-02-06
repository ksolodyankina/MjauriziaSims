using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFMsgRepository : IMsgRepository
    {
        EFDbContext context;

        public EFMsgRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Msg> Messages
        {
            get { return context.Messages; }
        }


        public void SaveMsg(Msg msg)
        {
            if (msg.MsgId == 0)
            {
                context.Messages.Add(msg);
            }
            else
            {
                Msg dbEntry = context.Messages.Find(msg.MsgId);
                if (dbEntry != null)
                {
                    dbEntry.Code = msg.Code;
                    dbEntry.MsgRu = msg.MsgRu;
                    dbEntry.MsgEn = msg.MsgEn;
                }
            }
            context.SaveChanges();
        }

        public void DeleteMsg(Msg msg)
        {
            context.Messages.Remove(msg);
            context.SaveChanges();
        }
    }
}
