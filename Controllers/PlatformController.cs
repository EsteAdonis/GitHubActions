namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformController(IPlatformRepo platformRepo, IMapper mapper) : ControllerBase
{
	private readonly IPlatformRepo _platformRepo = platformRepo;
	private readonly IMapper _mapper = mapper;

	[HttpGet]
	public async Task<IActionResult> GetPlatforms()
	{
		var platforms = await _platformRepo.GetAllPlatformsAsync();
		return Ok(platforms);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetPlatformById(int id)
	{
		try
		{
			var platform = await _platformRepo.GetPlatformById(id);
			return Ok(platform);
		}
		catch (KeyNotFoundException ex)
		{
			return NotFound(ex.Message);
		}
	}

	[HttpPost]
	public async Task<IActionResult> CreatePlatform([FromBody] PlatformCreateDto platformCreateDto)
	{
		if (platformCreateDto == null)
		{
			return BadRequest("Platform data is null");
		}

		var platform = _mapper.Map<Platform>(platformCreateDto);
		if (platform == null)
		{
			return BadRequest("Invalid platform data");
		}

		await _platformRepo.CreatePlatformAsync(platform);
		await _platformRepo.SaveChangesAsync();

		return CreatedAtAction(nameof(GetPlatformById), new { id = platform.Id }, platform);
	}
	
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdatePlatform(int id, [FromBody] PlatformCreateDto platformUpdateDto)
	{
		if (platformUpdateDto == null)
		{
			return BadRequest("Platform data is null");
		}

		var platform = await _platformRepo.GetPlatformById(id);
		if (platform == null)
		{
			return NotFound("Platform not found");
		}

		_mapper.Map(platformUpdateDto, platform);
		await _platformRepo.SaveChangesAsync();

		return NoContent();
	}
}

