namespace Model
{
	public interface IItemsProcessor
	{
		void Process(IFileSystemItem item, int level);
	}
}