using System.Linq;
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