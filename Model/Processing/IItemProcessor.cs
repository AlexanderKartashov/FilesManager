using Model.Core;

namespace Model
{
	namespace Processing
	{
		public interface IItemProcessor
		{
			void ProcessItem(IFileSystemItem item, int level);
		}
	}
}