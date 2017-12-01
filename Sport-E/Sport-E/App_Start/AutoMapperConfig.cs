using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sport_E
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // Add map creation statements here
            // Mapper.CreateMap< FROM , TO >();

            // Disable AutoMapper v4.2.x warnings
#pragma warning disable CS0618

            // AutoMapper create map statements

            Mapper.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

            // Add more below...
            Mapper.CreateMap<Controllers.UserProfileAdd, Models.UserProfile>();
            Mapper.CreateMap<Models.UserProfile, Controllers.UserProfileBase>();
            Mapper.CreateMap<Controllers.UserProfileBase, Controllers.UserProfileEditinfoForm>();
            Mapper.CreateMap<Models.UserProfile, Controllers.ProfilePicture>();
            //Mapper.CreateMap<IdentityUser, Controllers.UserProfileBase>();
            //Mapper.CreateMap<Models.Event, Controllers.EventsController>();
            Mapper.CreateMap<Models.Comment, Controllers.CommentBase>();
            Mapper.CreateMap<Controllers.CommentAdd, Models.Comment>(); //Create for comment
            Mapper.CreateMap<Models.Comment, Controllers.CommentWithEventInfo>();
            Mapper.CreateMap<Models.Comment, Controllers.CommentWithEventId>();


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Mapper.CreateMap<Models.Event, Controllers.EventBase>();
            Mapper.CreateMap<Controllers.EventAdd, Models.Event>();


            ////////////////////////////////////////////////////////////////////////////////
            Mapper.CreateMap<Models.Event, Controllers.EventBase>();
            Mapper.CreateMap<Models.Event, Controllers.EventWithPlayers>();
            Mapper.CreateMap<Controllers.EventBase, Controllers.EventEditContactInfoForm>();
            Mapper.CreateMap<Controllers.EventBase, Controllers.EventEditContactInfo>();
            Mapper.CreateMap<Models.Event, Controllers.EventPicture>();
            Mapper.CreateMap<Controllers.EventBase, Controllers.EventRateForm>();
            Mapper.CreateMap<Controllers.EventBase, Controllers.EventRate>();
            //hmmm...
            Mapper.CreateMap<Controllers.EventEditContactInfo, Controllers.EventEditContactInfoForm>();

            Mapper.CreateMap<Controllers.EventRate, Controllers.EventRateForm>();

            Mapper.CreateMap<Controllers.EventAdd, Models.Event>();
            Mapper.CreateMap<Controllers.EventAdd, Models.UserProfile>();

            Mapper.CreateMap<Models.Notification, Controllers.NotificationBase>();

            Mapper.CreateMap<Models.JoinRequest, Controllers.JoinRequestBase>();
            Mapper.CreateMap<Models.JoinRequest, Controllers.UserProfileBase>();
            Mapper.CreateMap<Models.JoinRequest, Controllers.EventBase>();
            Mapper.CreateMap<Models.UserProfile, Controllers.JoinRequestBase>();
            Mapper.CreateMap<Models.Event, Controllers.JoinRequestBase>();

#pragma warning restore CS0618
        }
    }
}