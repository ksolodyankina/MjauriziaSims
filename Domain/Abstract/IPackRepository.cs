using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IPackRepository
    {
        IEnumerable<Pack> Packs { get; }
        void SavePack(Pack pack);
    }
}
