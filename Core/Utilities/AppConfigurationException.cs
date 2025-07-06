namespace I8Pages.Core.Utilities;

class AppConfigurationException : ArgumentException
{
	public AppConfigurationException() { }

	public AppConfigurationException(string text) : base(text) { }
	
	public AppConfigurationException(string text, Exception inner) : base(text, inner) { }
}