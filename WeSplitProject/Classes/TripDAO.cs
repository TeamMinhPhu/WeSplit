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
				new Trip() { ID="1", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, EndedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="2", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="3", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="4", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, EndedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="5", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, EndedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="6", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="7", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="8", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="9", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="10", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, EndedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="11", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, EndedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="12", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="13", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="14", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, EndedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="15", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, EndedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="16", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="17", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="18", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },
				new Trip() { ID="19", Name= "Trip", Members = fakeMemberList, StartedDate = DateTime.Now, CoverImage = "Resources/Images/sora.jpg" },

			};

			return fakeData;
		}
	}
}
