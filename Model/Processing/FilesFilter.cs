using Model.Core;

namespace Model
{
	namespace Processing
	{
		public class FilesFilter : IItemFilter
		{
			public bool FilterItem(IFileSystemItem item, int level)
			{
				return item is File;
			}
		}
	}
}