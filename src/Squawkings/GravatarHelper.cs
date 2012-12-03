using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GravatarHelper;
using Squawkings.Models;

namespace Squawkings
{
	public interface IGravatarsHelper
	{
		string BuildGravatar(string email);
		List<SquawkDisp> SetUrls(List<SquawkDisp> squawkList);
	}

	public class GravatarsHelper : IGravatarsHelper
	{
		public string BuildGravatar(string email)
		{
			// Build Gravatar
			const string defaultImgUrl = @"~/Content/Images/placeholder-profile-img.jpg";
			return GravatarHelper.GravatarHelper.CreateGravatarUrl(email, 57, defaultImgUrl, GravatarRating.G, false, false);
		}

		public List<SquawkDisp> SetUrls(List<SquawkDisp> squawkList)
		{
			// TODO: Only do BuildGravatar once per user instead of every squawk
			foreach (var squawkDisp in squawkList)
			{
				if (squawkDisp.IsGravatar)
					squawkDisp.AvatarUrl = BuildGravatar(squawkDisp.Email);
				else
					squawkDisp.AvatarUrl = "~/Content/" + squawkDisp.AvatarUrl;
			}
			return squawkList;
		}
	}
}