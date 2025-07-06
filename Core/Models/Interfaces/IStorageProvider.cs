using I8Pages.Core.Utilities;

namespace I8Pages.Core;

/// <summary>
/// An interface that represents the provider for storing blog posts files.
/// </summary>
public interface IStorageProvider
{
	/// <summary>
	/// Creates a new post. Returns the path of the new post's file.
	/// </summary>
	/// <param name="title">The title of the post.</param>
	/// <param name="id">The ID of the new post.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	Task<string> CreateAsync(Guid id, string title);

	/// <summary>
	/// Gets the data of a post by its ID.
	/// </summary>
	/// <param name="id">The ID of the post.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	Task<GetData> GetAsync(Guid id);

	/// <summary>
	/// Gets IDs of all posts.
	/// </summary>
	/// <returns>An object representing an asynchronously running operation.</returns>
	Task<IEnumerable<Guid>> GetIdsAsync();

	/// <summary>
	/// Deletes a post by its ID.
	/// </summary>
	/// <param name="id">The ID of the post.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	Task DeleteAsync(Guid id);

	/// <summary>
	/// Writes information about a post represented as an object of type <c>Post</c> to a file corresponding to this post.
	/// </summary>
	/// <param name="post">A post represented with the <c>Post</c> type.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	Task SaveAsync(Post post);
}