using System;
using System.ComponentModel;

namespace Xamarin.Forms.Internals
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class DataTemplateExtensions
	{
		[Obsolete("Please use IDataTemplateSelector instead")]
		public static DataTemplate SelectDataTemplate(this DataTemplate self, object item, BindableObject container)
		{
			var selector = self as DataTemplateSelector;
			if (selector == null)
				return self;

			return selector.SelectTemplate(item, container);
		}

		public static DataTemplate SelectDataTemplate(DataTemplate dataTemplate, IDataTemplateSelector dataTemplateSelector, object item, BindableObject container)
		{
			if (dataTemplateSelector != null)
				return dataTemplateSelector.SelectTemplate(item, container) ?? dataTemplate;

			return dataTemplate;
		}

		[Obsolete("Please use IDataTemplateSelector instead")]
		public static object CreateContent(this DataTemplate self, object item, BindableObject container)
		{
			return self.SelectDataTemplate(item, container).CreateContent();
		}

		public static object CreateContent(DataTemplate dataTemplate, IDataTemplateSelector dataTemplateSelector, object item, BindableObject container)
		{
			return SelectDataTemplate(dataTemplate, dataTemplateSelector, item, container)?.CreateContent();
		}
	}
}