using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitProject.Classes
{
	class ViewModel
	{
		public string ID { get; set; }
		public string StartedDate { get; set; }
		public string EndedDate { get; set; }
		public string Name { get; set; }
		public string CoverImage { get; set; }
	}


	class MEMVER_VIEW
	{
		public string MemberName { get; set; }
		public string MemberId { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Avatar { get; set; }
		public long Expend { get; set; }
		public long ExpendTotal { get; set; }
		public long Paid { get; set; }
		public string Charge { get; set; }
		public ICollection<TRIP_SPLIT> expends { get; set; }
		public void setExpend()
		{
			long result = 0;
			foreach (var cost in expends)
			{
				result = result + (long)cost.PAID_COST;
			}
			Expend = (long)result;
		}
	}
}
