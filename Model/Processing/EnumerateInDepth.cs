using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Core;

namespace Model
{
	namespace Processing
	{
		public class EnumerateInDepth : BaseEnumerationStrategy
		{
			protected override void EnumerateImpl(IFileSystemItem root, int level)
			{
				_handler?.Invoke(root, level);
				root.Objects?.ToList().ForEach((IFileSystemItem item) => EnumerateImpl(item, level + 1));
			}
		}
	}
}