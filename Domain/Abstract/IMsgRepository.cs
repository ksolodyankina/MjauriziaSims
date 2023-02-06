using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IMsgRepository
    {
        IEnumerable<Msg> Messages { get; }
        void SaveMsg(Msg msg);
        void DeleteMsg(Msg msg);
    }
}
