using System;
using System.ComponentModel.DataAnnotations;

namespace BrightIdeas.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        public bool IsUpLike { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        // Foreign Keys
        public int BrightIdeaId { get; set; }
        public int UserId { get; set; }

        // Navigation Properties
        public User Liker { get; set; }
        public BrightIdea Idea { get; set; }
    }
}