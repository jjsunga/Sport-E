using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sport_E.Controllers
{
    public class EventAddForm
    {

        public EventAddForm()
        {
            EventName = "";
            EventCreator = "";
            EventDate = DateTime.Now;
            PublicationStatus = "";
            City = "";
            Street = "";
            Province = "";
            PostalCode = "";
            Country = "";
            Gender = "";
            SkillLevel = "";
            PublicationStatus = "Under Review";
        }
        [Key]
        public int id { get; set; }

        [Display(Name = "Event Name")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The EventName must aphabetic with spaces.")]
        [Required, StringLength(60, ErrorMessage = "The EventName must be at least 3 characters long.", MinimumLength = 3)]
        //[, StringLength(100, ErrorMessage = "The EventName must be at least 5 characters long.", MinimumLength = 5)]
        public string EventName { get; set; }

        [Display(Name = "Event Creator")]
        [Required, StringLength(100, MinimumLength = 3)]
        public string EventCreator { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Hour")]
        public int Hour { get; set; }

        [Display(Name = "Minute")]
        public int Minute { get; set; }

        //[RegularExpression(@"[1-9][0-9]?$|^100$")]
        [Required]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 1,000")]
        public int Capacity { get; set; }

        [Display(Name = "Age Group")]
        [Required, StringLength(60)]
        public string AgeGroup { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The City name must be only aphabetic with spaces.")]
        [Required, StringLength(60)]
        public string City { get; set; }

        [RegularExpression(@"^([0-9]{1})+[ ]+[a-zA-Z ]*$", ErrorMessage = "The Street must be numeric followed by a space and alphabetic street name.")]
        [Required, StringLength(100)]
        public string Street { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Province name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Province { get; set; }


        [RegularExpression(@"^[A-Z][0-9][A-Z]+[ ]+[0-9][A-Z][0-9]$", ErrorMessage = "The PostalCode must be in the Canadian Postal code format 'L4E 5K6'.")]
        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Country name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Country { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string SkillLevel { get; set; }

        [Required, StringLength(100)]
        public string PublicationStatus { get; set; }

        [Required]
        [Display(Name = "Event Picture")]
        [DataType(DataType.Upload)]
        public string PhotoUpload { get; set; }


    }

    public class EventAdd
    {
        public EventAdd()
        {
            EventName = "";
            EventCreator = "";
            EventDate = DateTime.Now;
            PublicationStatus = "";
            City = "";
            Street = "";
            Province = "";
            PostalCode = "";
            Country = "";
            Gender = "";
            SkillLevel = "";
            PublicationStatus = "Under Review";
            JoinRequests = new List<JoinRequestBase>();
        }
        [Key]
        public int id { get; set; }

        [Display(Name = "Event Name")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The EventName must aphabetic with spaces.")]
        [Required, StringLength(60, ErrorMessage = "The EventName must be at least 3 characters long.", MinimumLength = 3)]
        //[, StringLength(100, ErrorMessage = "The EventName must be at least 5 characters long.", MinimumLength = 5)]
        public string EventName { get; set; }

        [Display(Name = "Event Creator")]
        [Required, StringLength(100, MinimumLength = 3)]
        public string EventCreator { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Hour")]
        public int Hour { get; set; }

        [Display(Name = "Minute")]
        public int Minute { get; set; }

        //[RegularExpression(@"[1-9][0-9]?$|^100$")]
        [Required]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 1,000")]
        public int Capacity { get; set; }

        [Required, StringLength(60)]
        public string AgeGroup { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The City name must be only aphabetic with spaces.")]
        [Required, StringLength(60)]
        public string City { get; set; }

        [RegularExpression(@"^([0-9]{1})+[ ]+[a-zA-Z ]*$", ErrorMessage = "The Street must be numeric followed by a space and alphabetic street name.")]
        [Required, StringLength(100)]
        public string Street { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Province name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Province { get; set; }


        [RegularExpression(@"^[A-Z][0-9][A-Z]+[ ]+[0-9][A-Z][0-9]$", ErrorMessage = "The PostalCode must be in the Canadian Postal code format 'L4E 5K6'.")]
        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Country name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Country { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string SkillLevel { get; set; }

        [Required, StringLength(100)]
        public string PublicationStatus { get; set; }

        [Required]
        public HttpPostedFileBase PhotoUpload { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }

        public int numRatings { get; set; }

        public ICollection<JoinRequestBase> JoinRequests { get; set; }

    }
    public class EventBase : EventAdd
    {
        public int Id { get; set; }

        [Display(Name = "Event Picture")]
        public string PhotoUrl
        {
            get
            {
                return $"/eventpicturephoto/{Id}";
            }
        }

    }

    public class EventPicture
    {
        public int Id { get; set; }
        public string EventPicturePhotoContentType { get; set; }
        public byte[] EventPicturePhoto { get; set; }
    }

    public class EventAddCommentForm
    {
        public EventAddCommentForm()
        {
           // CommentsInEvent = new List<CommentBase>();
        }

        [Key]
        public int EventId { get; set; }

        //[Editable(false)]
        //public string EventName { get; set; }

        [Editable(false)]
        public string Email { get; set; }

        [Required, StringLength(500)]
        [Display(Name = "Comment")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; } //comment message

        // Edited, replace with generic collection
        // For select list
        //[Display(Name = "All tracks")]
        //public MultiSelectList TrackList { get; set; }

        // For the extra list of tracks (before edits)
        //[Display(Name = "Comments Posted On Event")]
        //public IEnumerable<CommentBase> CommentsInEvent { get; set; }

    }

    public class EventAddComments
    {
        public EventAddComments()
        {
        }

        [Key]
        public int EventId { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }
    }


    /*
public class EventWithComments : EventBase
{
    public EventWithComments()
    {
        Comments = new List<CommentBase>();
    }

    public IEnumerable<CommentBase> Comments { get; set; }
}
*/


    //We're using this as the details page
    //Passes players and comments
    public class EventWithPlayers : EventBase
    {
        public EventWithPlayers()
        {
            Players = new List<JoinRequestBase>();
            pp = new string[1000];
            UserProfiles = new List<UserProfileBase>();
            Comments = new List<CommentBase>();
        }

        public string [] pp { get; set; }
        public IEnumerable<UserProfileBase> UserProfiles { get; set; }
        [Display(Name = "List of attendees")]
        public IEnumerable<JoinRequestBase> Players { get; set; }
        public IEnumerable<CommentBase> Comments { get; set; }

    }

    public class EventEditContactInfoForm
    {
        public EventEditContactInfoForm()
        {
            EventDate = DateTime.Now;
        }

        [Key]
        public int id { get; set; }

        [Display(Name = "Event Name")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The EventName must aphabetic with spaces.")]
        [Required, StringLength(60, ErrorMessage = "The EventName must be at least 3 characters long.", MinimumLength = 3)]
        public string EventName { get; set; }

        [Display(Name = "Event Creator")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The EventCreator must aphabetic with spaces.")]
        [Required, StringLength(100, ErrorMessage = "The EventCreator must be at least 3 characters long.", MinimumLength = 3)]
        public string EventCreator { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        //[RegularExpression(@"[1-9][0-9]?$|^100$")]
        [Required]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 1,000")]
        public int Capacity { get; set; }

        [Required, StringLength(60)]
        public string AgeGroup { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The City name must be only aphabetic with spaces.")]
        [Required, StringLength(60)]
        public string City { get; set; }

        [RegularExpression(@"^([0-9]{1})+[ ]+[a-zA-Z ]*$", ErrorMessage = "The Street must be numeric followed by a space and alphabetic street name.")]
        [Required, StringLength(100)]
        public string Street { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Province name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Province { get; set; }


        [RegularExpression(@"^[A-Z][0-9][A-Z]+[ ]+[0-9][A-Z][0-9]$", ErrorMessage = "The PostalCode must be in the Canadian Postal code format 'L4E 5K6'.")]
        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Country name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Country { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string SkillLevel { get; set; }

        [Required, StringLength(100)]
        public string PublicationStatus { get; set; }
    }

    public class EventEditContactInfo
    {
        public EventEditContactInfo()
        {
            EventDate = DateTime.Now;
        }

        [Key]
        public int id { get; set; }

        [Display(Name = "Event Name")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The EventName must aphabetic with spaces.")]
        [Required, StringLength(60, ErrorMessage = "The EventName must be at least 3 characters long.", MinimumLength = 3)]
        public string EventName { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        //[RegularExpression(@"[1-9][0-9]?$|^100$")]
        [Required]
        [Range(1, 100, ErrorMessage = "Capacity must be between 1 and 1,000")]
        public int Capacity { get; set; }

        [Required, StringLength(60)]
        public string AgeGroup { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The City name must be only aphabetic with spaces.")]
        [Required, StringLength(60)]
        public string City { get; set; }

        [RegularExpression(@"^([0-9]{1})+[ ]+[a-zA-Z ]*$", ErrorMessage = "The Street must be numeric followed by a space and alphabetic street name.")]
        [Required, StringLength(100)]
        public string Street { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Province name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Province { get; set; }


        [RegularExpression(@"^[A-Z][0-9][A-Z]+[ ]+[0-9][A-Z][0-9]$", ErrorMessage = "The PostalCode must be in the Canadian Postal code format 'L4E 5K6'.")]
        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "The Country name must be only aphabetic with spaces.")]
        [Required, StringLength(100)]
        public string Country { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string SkillLevel { get; set; }
    }

    public class EventRateForm
    {
        public EventRateForm() { }

        [Key]
        public int id { get; set; }


        public string EventName { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
    }

    public class EventRate
    {
        public EventRate() { }

        [Key]
        public int id { get; set; }

        public string EventName { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}