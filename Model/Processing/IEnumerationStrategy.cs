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
		public interface IEnumerationStrategy
		{
			void Init(FileSystemEnumerator.ProcessItemEventHandler handler);

			void Enumerate(IFileSystemItem root);
		}
	}
}