using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.ViewModels
{
	public class Register
	{

		[Key]
		public int Id { get; set; }

		[Required]
		[RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First Name should only contain letters.")]
		[DataType(DataType.Text)]
		public string FirstName { get; set; }

		[Required]
		[RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last Name should only contain letters.")]
		[DataType(DataType.Text)]
		public string LastName { get; set; }

		[Required]
		public string Gender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z]([0-9]{7})[A-Za-z]$", ErrorMessage = "NRIC should start and end with letters, with 7 digits in the middle.")]
        [DataType(DataType.Text)]
        [StringLength(9, ErrorMessage = "NRIC should be 9 characters.")]
        public string NRIC { get; set; }

        [Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MinLength(12, ErrorMessage = "Password must be at least 12 characters.")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$",
			ErrorMessage = "Password must include lower-case, upper-case, numbers, and special characters.")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "Password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[Required]
		[FileExtensions(Extensions = ".docx,.pdf", ErrorMessage = "Only .docx or .pdf files are allowed.")]
		public string Resume { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		public string WhoAmI { get; set; }

		public void EncryptNRIC()
		{
			if (!string.IsNullOrEmpty(NRIC))
			{
				// Encryption logic...
			}
		}

		public Register()
		{
			DateOfBirth = new DateTime(1980, 1, 1);
		}
	}
}
