using System.Text.Json;
using System.Text.Json.Serialization;
using I8Pages.Core.Utilities;

namespace I8Pages.Core;

/// <summary>
/// The main class for working with posts.
/// </summary>
public static class PostService
{
	/// <summary>
	/// An object of the <c>IStorageProvider</c> type, necessary for working with post and metadata files.
	/// </summary>
	private static readonly IStorageProvider _storageProvider;

	/// <summary>
	/// Intializes <c>PostService</c> class.
	/// </summary>
	/// <exception cref="AppConfigurationException"></exception>
	static PostService()
	{
		string settingsPath = "..\\..\\appsettings.json";
		string jsonData = File.ReadAllText(settingsPath);
		JsonElement root = JsonDocument.Parse(jsonData).RootElement;

		if (root.TryGetProperty("storageProvider", out JsonElement providerNameProperty))
		{
			_storageProvider = providerNameProperty.GetString() switch
			{
				"local" => new LocalStorageProvider(),
				_ => throw new AppConfigurationException("Invalid value for \"storageProvider\" property.")
			};
		}

		else
		{
			throw new AppConfigurationException("Property \"storageProvider\" must be assigned a value.");
		}
	}

	/// <summary>
	/// Creates a new post.
	/// </summary>
	/// <param name="title">The title of the new post.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	public static async Task<Post> CreateAsync(string title)
	{
		string path = await _storageProvider.CreateAsync(title);
		Post post = new Post(
			Guid.NewGuid(),
			title,
			string.Empty,
			DateTime.UtcNow,
			DateTime.UtcNow,
			path
		);
		return post;
	}

	/// <summary>
	/// Gets a post by its ID.
	/// </summary>
	/// <param name="id">The post's ID.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	public static async Task<Post> GetAsync(Guid id)
	{
		GetData getData = await _storageProvider.GetAsync(id);
		return new Post(
			id,
			getData.Title,
			getData.Text,
			getData.UtcCreationTime,
			getData.UtcLastUpdateTime,
			getData.Path
		);
	}

	/// <summary>
	/// Gets all posts.
	/// </summary>
	/// <returns>An object representing an asynchronously running operation.</returns>
	public static async Task<List<Post>> GetAllAsync()
	{
		List<Post> result = new List<Post>();
		IEnumerable<Guid> ids = await _storageProvider.GetIdsAsync();
		foreach (Guid id in ids)
		{
			Post post = await GetAsync(id);
			result.Add(post);
		}
		return result;
	}

	/// <summary>
	/// Changes the text of an existing post.
	/// </summary>
	/// <param name="id">The ID of the post.</param>
	/// <param name="newText">The new text of the post.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	public static async Task UpdateAsync(Guid id, string newText)
	{
		Post oldPost = await GetAsync(id);
		Post newPost = new Post(
			id,
			oldPost.Title,
			newText,
			oldPost.UtcCreationTime,
			DateTime.UtcNow,
			oldPost.Path
		);
		await _storageProvider.SaveAsync(newPost);
	}

	/// <summary>
	/// Changes the title of an existing post.
	/// </summary>
	/// <param name="id">The ID of the post.</param>
	/// <param name="newTitle">The new title of the post.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	public static async Task ChangeTitleAsync(Guid id, string newTitle)
	{
		Post oldPost = await GetAsync(id);
		Post newPost = new Post(
			id,
			newTitle,
			oldPost.Text,
			oldPost.UtcCreationTime,
			DateTime.UtcNow,
			oldPost.Path
		);
		await _storageProvider.SaveAsync(newPost);
	}

	/// <summary>
	/// Deletes a post.
	/// </summary>
	/// <param name="id">The ID of the post to delete.</param>
	/// <returns>An object representing an asynchronously running operation.</returns>
	public static async Task DeleteAsync(Guid id) => await _storageProvider.DeleteAsync(id);
}