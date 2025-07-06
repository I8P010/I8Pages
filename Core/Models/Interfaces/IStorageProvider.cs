namespace I8Pages.Core;

/// <summary>
/// 
/// </summary>
public interface IStorageProvider
{
	Task<string> CreateAsync(string title);

	Task<GetData> GetAsync(Guid id);
	
	Task<IEnumerable<Guid>> GetIdsAsync();

	Task DeleteAsync(Guid id);

	Task SaveAsync(Post post);
}