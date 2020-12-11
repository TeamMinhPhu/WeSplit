using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeSplitProject.Classes
{
	class PagingViewModel
	{
		public static BindingList<string> UpdatePage(int totalPages)
		{
			BindingList<string> result = new BindingList<string>();
			for (int i = 1; i <= totalPages; i++)
			{
				result.Add(i.ToString());
			}
			return result;
		}
	}

	class Paging
	{
		public static int GetTotalPages(int totalItems, int itemsPerPage)
		{
			int result;
			result = totalItems / itemsPerPage + ((totalItems % itemsPerPage) == 0 ? 0 : 1);
			return result;
		}

		public static int GetItemsPerPage(double width, double height)
		{
			int result, row, column;
			row = (int)height / 220;
			column = (int)width / 330;
			result = row * column;
			if (result == 0)
			{
				result = 1;
			}
			return result;
		}
	}

}
