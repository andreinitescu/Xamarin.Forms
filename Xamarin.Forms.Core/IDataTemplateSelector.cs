namespace Xamarin.Forms
{
	public interface IDataTemplateSelector
	{
		DataTemplate SelectTemplate(object item, BindableObject container);
	}
}
