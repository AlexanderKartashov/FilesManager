using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Processing
{
	public interface IItemProcessor
	{
		void ProcessItem(IFileSystemItem item, int level);
	}
}
