using Model.Core;

namespace Model
{
	namespace Processing
	{
		public interface IEnumerationStrategy
		{
			void Init(FileSystemEnumerator.ProcessItemEventHandler handler);

			void Enumerate(IFileSystemItem root);
		}
	}
}