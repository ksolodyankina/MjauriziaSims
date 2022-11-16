﻿using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string Title { get; set; } = "";
        public bool IsChild { get; set; } = false;
    }
}
