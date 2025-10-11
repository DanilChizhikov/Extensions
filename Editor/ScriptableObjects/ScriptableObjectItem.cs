using System;

namespace DTech.Extensions.Editor
{
	internal sealed class ScriptableObjectItem
	{
		public Type Type { get; }
		public bool IsSelected { get; set; }

		public ScriptableObjectItem(Type type)
		{
			Type = type;
			IsSelected = false;
		}
	}
}