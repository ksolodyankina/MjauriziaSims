﻿using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class Career
    {
        public int CareerId { get; set; }

        public string Code { get; set; }
        public string Title { get; set; }
        public int Pack { get; set; }
    }
}
