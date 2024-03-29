﻿using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface ICareerRepository
    {
        IEnumerable<Career> Careers { get; }
        void SaveCareer(Career career);
    }
}
