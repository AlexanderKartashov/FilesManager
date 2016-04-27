using Model.Core;

namespace Model.Processing
{
	public interface IItemProcessor
	{
		void ProcessItem(IFileSystemItem item, int level);
	}
}
