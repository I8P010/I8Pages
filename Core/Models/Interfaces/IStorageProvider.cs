namespace I8Pages.Core;

/// <summary>
/// 
/// </summary>
public interface IStorageProvider
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	Task<Post> CreateAsync(string title);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	/// <param name="newText"></param>
	/// <returns></returns>
	Task UpdateAsync(Guid id, string newText);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	/// <param name="newName"></param>
	/// <returns></returns>
	Task ChangeTitleAsync(Guid id, string newTitle);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task<Post> GetAsync(Guid id);

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	Task<IEnumerable<Post>> GetAllAsync();
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	Task DeleteAsync(Guid id);
}