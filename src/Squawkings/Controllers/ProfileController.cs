using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GravatarHelper;
using NPoco;
using Squawkings.Models;

namespace Squawkings.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ISquawksDb _squawksDb;
        private readonly IDatabase _db;

        public ProfileController() 
            : this(new SquawksDb(), new Database("Squawkings"))
        {
            
        }
        public ProfileController(ISquawksDb squawksDb, IDatabase db)
        {
            _squawksDb = squawksDb;
            _db = db;
        }

        public ActionResult Index(string userName)
        {
            var vm = new ProfileViewModel()
                         {
                             SquawkDisps = _squawksDb.GetProfileSquawks(userName),
                             ProfileDetails = _squawksDb.UserDispGetProfileByUserName(userName, User.Identity.Id())
                         };


            if(vm.ProfileDetails.Userid != User.Identity.Id())
            {
                vm.ProfileDetails.UnfollowButton = vm.ProfileDetails.IsFollowing;
                vm.ProfileDetails.FollowButton = !vm.ProfileDetails.IsFollowing;
            }

            vm.ProfileDetails.DisplayUrl = vm.ProfileDetails.IsGravatar ? BuildGravatar(vm.ProfileDetails) : vm.ProfileDetails.AvatarUrl;

            return View(vm);
        }

        public string BuildGravatar(UserDisp user)
        {
            // Build Gravatar
            return GravatarHelper.GravatarHelper.CreateGravatarUrl(user.Email, 57,
                                                            Url.Content(
                                                                @".~/Content/Images/placeholder-profile-img2.jpg"),
                                                            GravatarRating.G, false, false);
        }

        [Authorize]
        public ActionResult Follow(ProfileInputModel im)
        {
            _db.Execute(@"insert into Followers (UserId, FollowingUserId) select @0, (select UserId from Users u where u.UserName = @1) ", User.Identity.Id(), im.UserName);

            return RedirectToAction("index","Profile", new {userName = im.UserName});
        }

        [Authorize]
        public ActionResult UnFollow(ProfileInputModel im)
        {
            var userId = User.Identity.Id();
            _db.Execute("delete from Followers where FollowingUserId = (select UserId from Users u where u.UserName = @0) and userid = @1", im.UserName, userId);

            return RedirectToAction("index","Profile", new {userName = im.UserName});
        }
    }
    
    public class ProfileViewModel
    {
        public ProfileViewModel()
        {
            SquawkDisps = new List<SquawkDisp>();
        }
        public List<SquawkDisp> SquawkDisps { get; set; }
        public UserDisp ProfileDetails { get; set; }
    }

    public class ProfileInputModel
    {
        public string UserName { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }

}
