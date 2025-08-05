namespace PlatformService.Data;

public interface IPlatformRepo
{
	Task<bool> SaveChangesAsync();
	Task<Platform> GetPlatformById(int id);
	Task<IEnumerable<Platform>> GetAllPlatformsAsync();	
	Task CreatePlatformAsync(Platform platform);
	void UpdatePlatformAsync(Platform platform);
	Task DeletePlatformAsync(int id);
}
