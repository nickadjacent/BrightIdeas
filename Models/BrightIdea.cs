using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models
{
    public class BrightIdea
    {
        [Key]
        public int BrightIdeaId { get; set; }


        [Required(ErrorMessage = "is required")]
        [MinLength(5, ErrorMessage = "must be at least {1} characters")]
        public string Idea { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }


        // Navigation Property

        public User SubmittedBy { get; set; }

        public List<Like> Likes { get; set; } // Many to Many between User & Post: 1 Post can have Many votes

    }
}