using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IInheritanceLawRepository
    {
        IEnumerable<InheritanceLaw> InheritanceLaws { get; }
        void SaveInheritanceLaw(InheritanceLaw inheritanceLaw);
    }
}
