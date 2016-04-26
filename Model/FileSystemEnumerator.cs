using System.Linq;

namespace Model
{
	public class FileSystemEnumerator
	{
		public delegate void ProcessItemEventHandler(IFileSystemItem item, int level);

		public event ProcessItemEventHandler ProcessItemEvent;

		public void Enumerate(IFileSystemItem root)
		{
			EnumerateRecursive(root, 0);
		}

		private void EnumerateRecursive(IFileSystemItem root, int level)
		{
			ProcessItemEvent?.Invoke(root, level);

			root.Objects?.ToList().ForEach((IFileSystemItem item) => EnumerateRecursive(item, level + 1));
		}
	}
}
