using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitProject.Classes
{
	class TripDAO
	{
		public static BindingList<Trip> GetAll()
		{
			Member fakeMember = new Member() { Name = "Phu", Phone="123456789"};
			List<Member> fakeMemberList = new List<Member>()
			{
				fakeMember,
				fakeMember
			};

			BindingList<Trip> fakeData = new BindingList<Trip>
			{
				new Trip() { ID="1", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), EndedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="2", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="3", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="4", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), EndedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="5", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), EndedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="6", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="7", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="8", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now.ToString(), CoverImage = "Resources/Images/sora.jpg" },
			};

			return fakeData;
		}
	}
}
