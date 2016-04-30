using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpecialSymbol.Models
{
    public class Post
    {
        public long  Id { get; set; }

        public string Key
        {
            get
            {
                if (Title == null)
                    return null;
                var key = Regex.Replace(Title, @"[^a-zA-z0-9\- ]", string.Empty);
                return key.Replace(" ","-").ToLower();
            }
            set
            {
                Key = value;
            }
        }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="Post Title")]
        [StringLength(100,MinimumLength =5 ,ErrorMessage = "{0} should be minimum {2} char lenth to {1} char length")]
        public string Title { get; set; }
        public DateTime PostedDate { get; set; }
        public string Author { get; set; }

        [Required]
        [MinLength(5,ErrorMessage = "Blog {0} atleast {1} charcter lenght")]
        public string Body { get; set; }
    }
}
