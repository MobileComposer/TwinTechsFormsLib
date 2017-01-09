using System;
using Xamarin.Forms;

namespace TwinTechs.Extensions
{
	public static class ViewExtensions
	{
		/// <summary>
		/// Gets the page to which an element belongs
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="element">Element.</param>
		public static Page GetParentPage(this VisualElement element)
		{
			if (element != null)
			{
				var parent = element.Parent;
				while (parent != null)
				{
					// keep moving up the visual tree until a Page is found
					if (parent is Page)
						return parent as Page;
					parent = parent.Parent;
				}
			}

			return null;
		}
	}
}

