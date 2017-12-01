using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sport_E.Controllers
{
    public class UserProfileAddForm
    {
        public UserProfileAddForm()
        {
            DateOfBirth = DateTime.Now.AddYears(-20);
            JoinRequests = new List<JoinRequestBase>();
        }

        [StringLength(100), Key]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "First name should only contain letters.")]
        [Display(Name = "First Name")]
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Last name should only contain letters.")]
        [Display(Name = "Last Name")]
        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [Required, StringLength(100)]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "City should only contain letters.")]
        [Display(Name = "City")]
        [Required, StringLength(100)]
        public string City { get; set; }

        [Display(Name = "Street")]
        [Required, StringLength(100)]
        public string Street { get; set; }


        [RegularExpression(@"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$", ErrorMessage = "Does not match format and has to be uppercase! A1A 2B2")]
        [Display(Name = "Postal Code")]
        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Province/State should only contain letters.")]
        [Display(Name = "Province/State")]
        [Required, StringLength(100)]
        public string Province { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Country should only contain letters.")]
        [Display(Name = "Country")]
        [Required, StringLength(100)]
        public string Country { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Does not match format! 123-456-7890")] 
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Profile Picture")]
        [DataType(DataType.Upload)]
        public string PhotoUpload { get; set; }

        public ICollection<JoinRequestBase> JoinRequests { get; set; }

    }

    public class UserProfileAdd
    {
        public UserProfileAdd()
        {
            DateOfBirth = DateTime.Now.AddYears(-20);
        }

        [StringLength(100), Key]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "First name should only contain letters.")]
        [Display(Name = "First Name")]
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Last name should only contain letters.")]
        [Display(Name = "Last Name")]
        [Required, StringLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [Required, StringLength(100)]
        public string Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "City should only contain letters.")]
        [Display(Name = "City")]
        [Required, StringLength(100)]
        public string City { get; set; }

        [Display(Name = "Street")]
        [Required, StringLength(100)]
        public string Street { get; set; }


        [RegularExpression(@"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$", ErrorMessage = "Does not match format and has to be uppercase! A1A 2B2")]
        [Display(Name = "Postal Code")]
        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Province/State should only contain letters.")]
        [Display(Name = "Province/State")]
        [Required, StringLength(100)]
        public string Province { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Country should only contain letters.")]
        [Display(Name = "Country")]
        [Required, StringLength(100)]
        public string Country { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Does not match format! 123-456-7890")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public HttpPostedFileBase PhotoUpload { get; set; }

    }

    public class UserProfileBase : UserProfileAdd
    {
        
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        public string PhotoUrl
        {
            get
            {
                return $"/profilepicturephoto/{Id}";
            }
        }
    }

    public class ProfilePicture
    {
        public int Id { get; set; }
        public string ProfilePicturePhotoContentType { get; set; }
        public byte[] ProfilePicturePhoto { get; set; }
    }

    public class UserProfileEditinfoForm : UserProfileEditinfo
    {
        public UserProfileEditinfoForm()
        {

        }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "First name should only contain letters.")]
        [Display(Name = "First Name")]
        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Last name should only contain letters.")]
        [Display(Name = "Last Name")]
        [Required, StringLength(100)]
        public string LastName { get; set; }
    }


    public class UserProfileEditinfo
    {
        public UserProfileEditinfo()
        {
            DateOfBirth = DateTime.Now;
        }

        [StringLength(100), Key]
        public string Email { get; set; }


        [Display(Name = "Date of Birth")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "City should only contain letters.")]
        [Display(Name = "City")]
        [Required, StringLength(100)]
        public string City { get; set; }

        [Display(Name = "Street")]
        [Required, StringLength(100)]
        public string Street { get; set; }


        [RegularExpression(@"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$", ErrorMessage = "Does not match format and has to be uppercase! A1A 2B2")]
        [Display(Name = "Postal Code")]
        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Province/State should only contain letters.")]
        [Display(Name = "Province/State")]
        [Required, StringLength(100)]
        public string Province { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Country should only contain letters.")]
        [Display(Name = "Country")]
        [Required, StringLength(100)]
        public string Country { get; set; }

        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Does not match format! 123-456-7890")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

    }
}