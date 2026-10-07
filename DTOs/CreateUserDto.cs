using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid Phone Number format.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;
    }
}
