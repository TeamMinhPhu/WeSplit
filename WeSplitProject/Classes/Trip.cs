﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeSplitProject.Classes
{
	class Trip
	{
		public string ID { get; set; }
		public string CoverImage { get; set; }
		public DateTime StartedDate { get; set; }
		public DateTime EndedDate { get; set; }
		public string Name { get; set; }
		public List<Member> Members {get; set; }
		public List<string> Photos { get; set; }
	}
}
