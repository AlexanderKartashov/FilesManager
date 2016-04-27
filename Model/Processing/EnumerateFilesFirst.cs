using System.Linq;
using Model.Core;

namespace Model
{
	namespace Processing
	{
		public class EnumerateFilesFirst : BaseEnumerationStrategy
		{
			protected override void EnumerateImpl(IFileSystemItem item, int level)
			{
				_handler?.Invoke(item, level);

				var objects = item.Objects;
				objects?.OfType<File>().ToList().ForEach((File file) => EnumerateImpl(file, level + 1));
				objects?.OfType<Folder>().ToList().ForEach((Folder folder) => EnumerateImpl(folder, level + 1));
			}
		}
	}
}