using Model.Core;

namespace Model
{
	namespace Processing
	{
		public interface IItemFilter
		{
			bool FilterItem(IFileSystemItem item, int level);
		}
	}
}
