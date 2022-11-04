﻿namespace SportsHub.Domain.Models
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
