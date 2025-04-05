using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Poll
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Option1Text { get; set; }
        public required string Option2Text { get; set; }
        public required string Option3Text { get; set; }
        public int Option1VotesCount { get; set; }
        public int Option2VotesCount { get; set; }
        public int Option3VotesCount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
