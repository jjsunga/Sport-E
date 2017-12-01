using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Sport_E.Models;
using System.Security.Claims;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.AspNet.Identity;



namespace Sport_E.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Declare a property to hold the user account for the current request
        // Can use this property here in the Manager class to control logic and flow
        // Can also use this property in a controller 
        // Can also use this property in a view; for best results, 
        // near the top of the view, add this statement:
        // var userAccount = new ConditionalMenu.Controllers.UserAccount(User as System.Security.Claims.ClaimsPrincipal);
        // Then, you can use "userAccount" anywhere in the view to render content
        public UserAccount UserAccount { get; private set; }

        public Manager()
        {
            // If necessary, add constructor code here

            // Initialize the UserAccount property
            UserAccount = new UserAccount(HttpContext.Current.User as ClaimsPrincipal);

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }



        // ############################################################
        // RoleClaim

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        public IEnumerable<EventBase> EventGetAll()
        {
            return Mapper.Map<IEnumerable<EventBase>>(ds.Events.OrderBy(evet => evet.EventName));
        }
        public EventBase EventAdd(EventAdd newItem)
        {
            var addedItem = ds.Events.Add(Mapper.Map<Event>(newItem));

            byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
            newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.EventPicturePhoto = photoBytes;
            addedItem.EventPicturePhotoContentType = newItem.PhotoUpload.ContentType;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<EventBase>(addedItem);
        }
        public EventBase EventGetOne(int? id)
        {
            var artist = ds.Events.Find(id.GetValueOrDefault());

            return (artist == null) ? null : Mapper.Map<EventBase>(artist);
        }

        public JoinRequestBase JoinRequestGetOne(int? id)
        {
            var a = ds.JoinRequest.Find(id.GetValueOrDefault());

            return (a == null) ? null : Mapper.Map<JoinRequestBase>(a);
        }

        public EventBase EventJoinRequest(int? eventid, string username)
        {
            var e = ds.Events.Find(eventid.GetValueOrDefault());

            var u = ds.UserProfiles.Where(s => String.Compare(s.Email, username) == 0).SingleOrDefault();

            ds.JoinRequest.Add(new JoinRequest { Event_j = e.EventName, Email_j = u.Email, PublicationStatus = "Under Review" });

            ds.SaveChanges();

            return (e == null) ? null : Mapper.Map<EventBase>(e);
        }
        public EventBase EventJoin(int? eventid, string username)
        {
            var e = ds.Events.Find(eventid.GetValueOrDefault());

            var u = ds.UserProfiles.Where(s => String.Compare(s.Email, username) == 0).SingleOrDefault();

            e.UserProfiles.Add(u);
            ds.SaveChanges();
            return (e == null) ? null : Mapper.Map<EventBase>(e);
        }

        public IEnumerable<EventBase> EventPendingGetAll()
        {
            return Mapper.Map<IEnumerable<EventBase>>(ds.Events.Where(e => e.PublicationStatus.Contains("Under Review")));//LISTS ALL .OrderBy(e => e.EventName));
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public IEnumerable<JoinRequestBase> JoinRequestGetAll()
        {
            return Mapper.Map<IEnumerable<JoinRequestBase>>(ds.JoinRequest.Where(e => e.PublicationStatus.Contains("Under Review")));
        }

        public EventBase EventEditContactInfo(EventEditContactInfo newItem)
        {
            // Attempt to fetch the object
            var o = ds.Events.Find(newItem.id);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<EventBase>(o);
            }
        }

        public EventBase EventRate(EventRate newItem)
        {
            // Attempt to fetch the object
            var o = ds.Events.Find(newItem.id);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                o.numRatings++;
                o.totalRating += newItem.Rating;
                o.Rating = o.totalRating / o.numRatings;

                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<EventBase>(o);
            }
        }

        public bool EventDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Events.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Events.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }

        public UserProfileBase UserProfileGetOne(string email)
        {

            // var obj = ds.UserProfiles.Find(email);
            var obj = ds.UserProfiles.SingleOrDefault(user => user.Email == email);
            return (obj == null) ? null : Mapper.Map<UserProfileBase>(obj);

        }

        public UserProfileBase UserProfileGetOneWithUserId(string UserId)
        {

            var userIdObj = ds.Users.Find(UserId);
            var obj = new UserProfile();
            if (userIdObj != null)
                obj = ds.UserProfiles.Find(userIdObj.Email);
            return (obj == null) ? null : Mapper.Map<UserProfileBase>(obj);

        }

        public UserProfileBase UserProfileGetOneWithId(int UserId)
        {

            var obj = ds.UserProfiles.SingleOrDefault(user => user.Id == UserId);
            return (obj == null) ? null : Mapper.Map<UserProfileBase>(obj);

        }



        public UserProfileBase UserProfileAdd(UserProfileAdd newItem)
        {
            var addedItem = ds.UserProfiles.Add(Mapper.Map<UserProfile>(newItem));

            byte[] photoBytes = new byte[newItem.PhotoUpload.ContentLength];
            newItem.PhotoUpload.InputStream.Read(photoBytes, 0, newItem.PhotoUpload.ContentLength);

            // Then, configure the new object's properties
            addedItem.ProfilePicturePhoto = photoBytes;
            addedItem.ProfilePicturePhotoContentType = newItem.PhotoUpload.ContentType;

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<UserProfileBase>(addedItem);
        }


        public UserProfileBase UserProfileEditinfo(UserProfileEditinfo newItem)
        {
            // Attempt to fetch the object
            // var o = ds.UserProfiles.Find(newItem.Email);
            var o = ds.UserProfiles.SingleOrDefault(user => user.Email == newItem.Email);
            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return Mapper.Map<UserProfileBase>(o);
            }
        }


        public IEnumerable<NotificationBase> NotificationsAll()
        {
            return Mapper.Map<IEnumerable<NotificationBase>>(ds.Notification.Where(e => e.ToEmail.Contains(HttpContext.Current.User.Identity.Name)));
            //return Mapper.Map<IEnumerable<NotificationBase>>(ds.Notifications.Where(e => e.Read.Equals(false)));//LISTS ALL unread notifications
        }
        //public IEnumerable<NotificationBase> NotificationsAll()
        // {
        //    return Mapper.Map<IEnumerable<NotificationBase>>(ds.Notification.Where(e => e.ToEmail.Contains(HttpContext.Current.User.Identity.Name)));
        //      //return Mapper.Map<IEnumerable<NotificationBase>>(ds.Notifications.Where(e => e.Read.Equals(false)));//LISTS ALL unread notifications
        // }
        public IEnumerable<NotificationBase> NotificationsPending()
        {
            var o =
             Mapper.Map<IEnumerable<NotificationBase>>(ds.Notification.Where(e => e.ToEmail.Contains(HttpContext.Current.User.Identity.Name)));
            o = o.Where(e => e.Read.Equals(false));
            return o;
            //return Mapper.Map<IEnumerable<NotificationBase>>(ds.Notifications.Where(e => e.Read.Equals(false)));//LISTS ALL unread notifications
        }

        public IEnumerable<EventBase> CalendarAll()
        {
            return Mapper.Map<IEnumerable<EventBase>>(ds.Events.OrderBy(evet => evet.EventName));
            //return Mapper.Map<IEnumerable<NotificationBase>>(ds.Notifications.Where(e => e.Read.Equals(false)));//LISTS ALL unread notifications
        }



        public ProfilePicture ProfilePictureGetById(string email)
        {

            var o = ds.UserProfiles.SingleOrDefault(user => user.Email == email);

            return (o == null) ? null : Mapper.Map<ProfilePicture>(o);
        }

        public EventPicture EventPhotoGetById(int id)
        {
            var o = ds.Events.Find(id);

            return (o == null) ? null : Mapper.Map<EventPicture>(o);
        }

        /*
        public bool UserProfileDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.UserProfiles.SingleOrDefault(user => user.Id == id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {

                // Remove the object
                var userMain = ds.Users.SingleOrDefault(user => user.Email == itemToDelete.Email);
            //    var useremail = userMain.Email;
                ds.UserProfiles.Remove(itemToDelete);
                ds.SaveChanges();
               // COMMENT THIS SECTION
                using (var db = new ApplicationDbContext())
                {

                    var user = db.Users.Find(userMain.Id);
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
               // ds.Users.Remove(userMain);
      //          ds.SaveChanges();
               // Membership.DeleteUser(userMain.Id, true); OLD 
               //tTILL HERE

                
                FormsAuthentication.SignOut();
 
               // FormsAuthentication.RedirectToLoginPage();
                return true;
            }
        }
    */

        public IEnumerable<CommentBase> CommentGetAll()
        {
            return Mapper.Map<IEnumerable<CommentBase>>(ds.Comments);
        }

        public CommentBase CommentGetById(int id)
        {
            var o = ds.Comments.Find(id);

            return (o == null) ? null : Mapper.Map<CommentBase>(o);
        }

        public CommentBase AddComment(CommentAdd newItem)
        {
            var addedItem = ds.Comments.Add(Mapper.Map<Comment>(newItem));

            ds.SaveChanges();

            return (addedItem == null) ? null : Mapper.Map<CommentBase>(addedItem);

        }

        //Is passed with comments
        public EventWithPlayers EventGetByIdWithPlayers(int id)
        {
            // Attempt to fetch the object
            var o = ds.Events.Include("UserProfiles").Include("Comments").SingleOrDefault(e => e.Id == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<EventWithPlayers>(o);
        }

        /*
        public EventWithComments EventGetByIdWithComments(int id)
        {
            // Attempt to fetch the object
            var o = ds.Events.Include("Comments").SingleOrDefault(t => t.Id == id);

            // Return the result, or null if not found
            return (o == null) ? null : Mapper.Map<EventWithComments>(o);
        }
        */

        public IEnumerable<CommentWithEventInfo> CommentGetAllWithEventInfo()
        {
            var c = ds.Comments.Include("Event").OrderBy(p => p.Id);

            return Mapper.Map<IEnumerable<CommentWithEventInfo>>(c);
        }

        public IEnumerable<CommentWithEventId> CommentGetAllWithEventId()
        {
            var c = ds.Comments.Include("Event").OrderBy(p => p.Id);

            return Mapper.Map<IEnumerable<CommentWithEventId>>(c);
        }

        public EventWithPlayers EventAddComments(EventAddComments newItem)
        {
            // Playlist edit existing

            // Attempt to fetch the object
            var o = ds.Events.Include("Comments").SingleOrDefault(p => p.Id == newItem.EventId);

            if (o == null)
            {
                return null;
            }
            else
            {
                ds.Comments.Add(new Comment { EventId = o.Id, Email = newItem.Email, Msg = newItem.Message, Event = o });
                ds.SaveChanges();
                o.Comments.Clear(); //clear comments connected to event
                var c = ds.Comments.Where(j => j.EventId.Equals(o.Id));                
                foreach (var item in c) //add all comments connected to event
                {
                    var a = ds.Comments.Find(item.Id);
                    o.Comments.Add(a);
                }
                //var addc = ds.Comments.SingleOrDefault(p => p.EventId == o.Id);
                //o.Comments.Add(addc);

                ds.SaveChanges();

                return Mapper.Map<EventWithPlayers>(o);
            }
        }



        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()






        // Add some programmatically-generated objects to the data store
        // Can write one method, or many methods - your decision
        // The important idea is that you check for existing data first
        // Call this method from a controller action/method

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // ############################################################
            // Genre

            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims

                //ds.SaveChanges();
                //done = true;
            }


            /*Adding
User Profile
*/
            /*if (ds.UserProfiles.Count() == 0)
            {
                ds.UserProfiles.Add(new UserProfile { FirstName = "Bob", LastName = "Jobs", Gender = "Male", DateOfBirth = new DateTime(1980, 01, 01), Description = "Hi My Name is Bob", City = "Toronto", Street = "70 The Pong Road", PostalCode = "M3J 3M6", Province = "Ontario", Country = "Canada", PhoneNumber = "4099999999", Email = "admin@example.com" });
                ds.UserProfiles.Add(new UserProfile { FirstName = "Mob", LastName = "Fobs", Gender = "Male", DateOfBirth = new DateTime(1980, 01, 01), Description = "Hi My Name is Mob", City = "Toronto", Street = "70 The Pong Road", PostalCode = "M3J 3M6", Province = "Ontario", Country = "Canada", PhoneNumber = "4069019099", Email = "xx@example.com" });

                ds.SaveChanges();
                done = true;

            }


            

            ds.SaveChanges();
            done = true;*/


            if (ds.Events.Count() <= 3)
            {
                // Add events
                // var bob = ds.UserProfiles.SingleOrDefault(a => a.FirstName == "Bob");
                //var mob = ds.UserProfiles.SingleOrDefault(a => a.FirstName == "Mob");

                ds.Events.Add(new Event
                {
                    EventName = "Men's Basketball at Seneca@York",
                    EventCreator = "Bobmob",
                    EventDate = new DateTime(2016, 12, 15, 10, 0, 0),
                    AgeGroup = "20-40",
                    Capacity = 30,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Male",
                    PostalCode = "M3J 3M6",
                    Province = "Ontario",
                    Street = "70 The Pond Road",
                    SkillLevel = "Any",
                    PublicationStatus = "Published"
                    //UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Come and play soccer!",
                    EventCreator = "JohnSmith",
                    EventDate = new DateTime(2017, 02, 18, 12, 30, 0),
                    AgeGroup = "20-30",
                    Capacity = 20,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M3J 3M6",
                    Province = "Ontario",
                    Street = "34 Yonge Street",
                    SkillLevel = "Any",
                    PublicationStatus = "Published"
                    //UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Etobicoke Golf (Kids)",
                    EventCreator = "GOLFFAN2343",
                    EventDate = new DateTime(2017, 06, 20, 15, 0, 0),
                    AgeGroup = "5-17",
                    Capacity = 50,
                    City = "Etobicoke",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M4W 3M3",
                    Province = "Ontario",
                    Street = "40 Beattie Ave",
                    SkillLevel = "Any",
                    PublicationStatus = "Published"
                    //UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Etobicoke Golf (Adults)",
                    EventCreator = "GOLFFAN2343",
                    EventDate = new DateTime(2017, 06, 20, 15, 0, 0),
                    AgeGroup = "18-65",
                    Capacity = 50,
                    City = "Etobicoke",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M4W 3M3",
                    Province = "Ontario",
                    Street = "40 Beattie Ave",
                    SkillLevel = "Any",
                    PublicationStatus = "Published"
                    //UserProfiles = new List<UserProfile> { mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "BEGINNERS CRICKET ALL AGES",
                    EventCreator = "some9",
                    EventDate = new DateTime(2017, 05, 05, 12, 0, 0),
                    AgeGroup = "Any",
                    Capacity = 20,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M3J 3M6",
                    Province = "Ontario",
                    Street = "70 The Pond Road",
                    SkillLevel = "Beginner",
                    PublicationStatus = "Published"
                    // UserProfiles = new List<UserProfile> { mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Tennis Lessons - Beginner",
                    EventCreator = "JackTennis",
                    EventDate = new DateTime(2017, 07, 10, 13, 0, 0),
                    AgeGroup = "18-30",
                    Capacity = 30,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M3M 5B5",
                    Province = "Ontario",
                    Street = "153 Danforth Avenue",
                    SkillLevel = "Beginner",
                    PublicationStatus = "Published"
                    //UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Tennis Lessons - Intermediate",
                    EventCreator = "JackTennis",
                    EventDate = new DateTime(2017, 07, 10, 16, 0, 0),
                    AgeGroup = "18-30",
                    Capacity = 30,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M3M 5B5",
                    Province = "Ontario",
                    Street = "153 Danforth Avenue",
                    SkillLevel = "Intermediate",
                    PublicationStatus = "Published"
                    //UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Toronto Women's Swimming (Any skill level)",
                    EventCreator = "Jane Smith",
                    EventDate = new DateTime(2017, 07, 25, 15, 0, 0),
                    AgeGroup = "18-60",
                    Capacity = 40,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Female",
                    PostalCode = "A8D 6F5",
                    Province = "Ontario",
                    Street = "1234 Yonge Street",
                    SkillLevel = "Any",
                    PublicationStatus = "Published"
                    // UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "ALL-DAY SWIMMING COMPETITION 2017",
                    EventCreator = "MarySwim",
                    EventDate = new DateTime(2017, 07, 30, 8, 0, 0),
                    AgeGroup = "18-50",
                    Capacity = 80,
                    City = "Mississauga",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M6S 8C8",
                    Province = "Ontario",
                    Street = "3134 Dundas Street West",
                    SkillLevel = "Any",
                    PublicationStatus = "Published"
                    // UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Come and play basketball!",
                    EventCreator = "JohnSmith",
                    EventDate = new DateTime(2017, 06, 18, 14, 0, 0),
                    AgeGroup = "20-30",
                    Capacity = 20,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M3J 3M6",
                    Province = "Ontario",
                    Street = "34 Yonge Street",
                    SkillLevel = "Any",
                    PublicationStatus = "Published"
                    // UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Kids Soccer Tournament!",
                    EventCreator = "Soccerfan23",
                    EventDate = new DateTime(2017, 07, 10, 14, 0, 0),
                    AgeGroup = "6-13",
                    Capacity = 30,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M7E 4S6",
                    Province = "Ontario",
                    Street = "22 Broadview Ave",
                    SkillLevel = "Intermediate",
                    PublicationStatus = "Published"
                    // UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Come Play Basketball (Seneca@York)",
                    EventCreator = "some9",
                    EventDate = new DateTime(2017, 05, 25, 12, 0, 0),
                    AgeGroup = "18-30",
                    Capacity = 25,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M3J 3M6",
                    Province = "Ontario",
                    Street = "70 The Pond Road",
                    SkillLevel = "Intermediate",
                    PublicationStatus = "Published"
                    // UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.Events.Add(new Event
                {
                    EventName = "Come Play Tennis (Seneca@York)",
                    EventCreator = "some9",
                    EventDate = new DateTime(2017, 05, 28, 12, 0, 0),
                    AgeGroup = "18-30",
                    Capacity = 25,
                    City = "Toronto",
                    Country = "Canada",
                    Gender = "Any",
                    PostalCode = "M3J 3M6",
                    Province = "Ontario",
                    Street = "70 The Pond Road",
                    SkillLevel = "Intermediate",
                    PublicationStatus = "Published"
                    // UserProfiles = new List<UserProfile> { bob, mob }
                });

                ds.SaveChanges();
                done = true;
            }


            /*
            if (ds.Events.Count() == 0)
            {
                // Add genres

               // ds.Events.Add(new Event { EventName = "BasketBall at Seneca", EventCreator = "John", EventDate = new DateTime(2016, 12, 15), Age = 20, Capacity = 10, City = "Toronto", Country = "Canada", Gender = "Any", PostalCode = "M3J 3M6", Province = "Ontario", Street = "70 The Pond Road", SkillLevel = "Any", PublicationStatus = "Under Review" });

                ds.SaveChanges();
                done = true;
            }
            */



            return done;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

    }

    // New "UserAccount" class for the authenticated user
    // Includes many convenient members to make it easier to render user account info
    // Study the properties and methods, and think about how you could use it
    public class UserAccount
    {
        // Constructor, pass in the security principal
        public UserAccount(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        // Add other role-checking properties here as needed
        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

}