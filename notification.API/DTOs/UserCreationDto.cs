// DTOs/UserCreationDto.cs

using System.ComponentModel.DataAnnotations;

namespace notification.API.DTOs
{
    public class UserCreationDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string FullName { get; set; }
    }
}
