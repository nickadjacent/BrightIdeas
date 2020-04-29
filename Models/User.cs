using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrightIdeas.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }


        [Required(ErrorMessage = "is required.")]
        [RegularExpression(@"^[a-zA-Z\s]{1,40}$", ErrorMessage = "Name should be letters and spaces only.")]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "is required.")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,40}$", ErrorMessage = "Alias should be letters and numbers only.")]
        // [MinLength(2, ErrorMessage = "must be at least {1} characters")]
        [Display(Name = "Alias")]
        public string Alias { get; set; }


        [Required(ErrorMessage = "is required.")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "is required.")]
        [MinLength(8, ErrorMessage = "must be at least {1} characters")]
        [DataType(DataType.Password)] // auto fills input type attr
        public string Password { get; set; }


        [NotMapped] // don't add to DB
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "doesn't match password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public DateTime UpdatedAt { get; set; } = DateTime.Now;



        // Navigation properties

        public List<BrightIdea> Ideas { get; set; } // 1 User to Many Posts relationship
        public List<Like> Likes { get; set; } // Many to Many between User & Post: 1 User can have Many votes
    }
}
