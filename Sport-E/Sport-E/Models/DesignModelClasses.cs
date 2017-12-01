using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sport_E.Models
{
    // Add your design model classes below

    // Follow these rules or conventions:

    // To ease other coding tasks, the name of the 
    //   integer identifier property should be "Id"
    // Collection properties (including navigation properties) 
    //   must be of type ICollection<T>
    // Valid data annotations are pretty much limited to [Required] and [StringLength(n)]
    // Required to-one navigation properties must include the [Required] attribute
    // Do NOT configure scalar properties (e.g. int, double) with the [Required] attribute
    // Initialize DateTime and collection properties in a default constructor

    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

    };

    public class UserProfile 
    {
        public UserProfile()
        {
            DateOfBirth = DateTime.Now.AddYears(-20);
        }
        
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; }

        
        [Required, StringLength(100)]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]   
        public DateTime DateOfBirth { get; set; }

        public string Description { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        [Required, StringLength(100)]
        public string Street { get; set; }

        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [Required, StringLength(100)]
        public string Province { get; set; }

        [Required, StringLength(100)]
        public string Country { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(100), Key]
        public string Email { get; set; }

        public ICollection<Event> Events { get; set; }

        [StringLength(200)]
        public string ProfilePicturePhotoContentType { get; set; }
        public byte[] ProfilePicturePhoto { get; set; }

        public ICollection<JoinRequest> EventRequests { get; set; }

    };


    public class Event
    {
        public Event()
        {
            EventDate = DateTime.Now;

            UserProfiles = new List<UserProfile>();
            Comments = new List<Comment>();
            pp = new string[1000];

        }

        public int Id { get; set; }

        [Required, StringLength(100)]
        public string EventName { get; set; }

        [Required, StringLength(100)]
        public string EventCreator { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required, StringLength(100)]
        public string City { get; set; }

        [Required, StringLength(100)]
        public string Street { get; set; }

        [Required, StringLength(100)]
        public string PostalCode { get; set; }

        [Required, StringLength(100)]
        public string Province { get; set; }

        [Required, StringLength(100)]
        public string Country { get; set; }

        public string AgeGroup { get; set; }

        public string SkillLevel { get; set; }
        public string Gender { get; set; }

        public int numRatings { get; set; }
        public int totalRating { get; set; }
        public int Rating { get; set; }

        public string PublicationStatus { get; set; }

        public ICollection<UserProfile> UserProfiles { get; set; }

        [StringLength(200)]
        public string EventPicturePhotoContentType { get; set; }
        public byte[] EventPicturePhoto { get; set; }

        public ICollection<JoinRequest> JoinRequests { get; set; }

       // public ICollection<UserProfile> JoinedUsers { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public string[] pp { get; set; }

    };

    public class Notification
    {

        public Notification()
        {
            ChangeDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string ToEmail { get; set; }

        [StringLength(100)]
        public string ChangedByEmail { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public DateTime ChangeDate { get; set; }

        public bool Read { get; set; }

    }
    public class JoinRequest
    {

        public JoinRequest()
        {}

        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Email_j { get; set; }

        [StringLength(100)]
        public string Event_j { get; set; }

        public string PublicationStatus { get; set; }


    }

    public class Comment
    {

        public Comment()
        {
            //CommentDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Msg { get; set; }


        //public DateTime CommentDate { get; set; }



        [Required]
        public Event Event { get; set; }

        //Every comment has one event it belongs to but every event has 0 or many comments.

    }


}
