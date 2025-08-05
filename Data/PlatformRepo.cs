namespace PlatformService.Data;

public class PlatformRepo(AppDbContext context): IPlatformRepo
{
	private readonly AppDbContext _context = context;

	public async Task<bool> SaveChangesAsync()
	{
		return await _context.SaveChangesAsync() >= 0;
	}

	public async Task<Platform> GetPlatformById(int id)
	{
		return await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id)
									?? throw new KeyNotFoundException($"Platform with id {id} not found");
	}

	public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
	{
		return await _context.Platforms.ToListAsync();
	}

	public async Task CreatePlatformAsync(Platform platform)
	{
		await _context.Platforms.AddAsync(platform);
	}

	public void UpdatePlatformAsync(Platform platform)
	{
		_context.Entry(platform).State = EntityState.Modified;
	}

	public async Task DeletePlatformAsync(int id)
	{
		var platform = await GetPlatformById(id);
		if (platform != null)
		{
			_context.Platforms.Remove(platform);
		}
	}
}
