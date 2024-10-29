using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPICore.Entities.DTO.Tag;
using ShopAPICore.Interfaces.Repository;
using ShopDb.Enums;

namespace ShopApiServer.Api.Product;

[Route("api/Product/Tag")]
[ApiController]
public class TagApi : ControllerBase
{
    private readonly ITagRepository _tagRepository;

    public TagApi(ITagRepository tagRepository) => _tagRepository= tagRepository;

    /// <returns>tag id that was created</returns>
    /// <response code="200">Sucess</response>
    /// <response code="400">AlreadyExist</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Roles = Roles.Admin)]
    [HttpPost("Create")]
    public async Task<int> Create(string name)
        => await _tagRepository.Create(name);

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("Delete")]
    public async Task Delete(int id)
        => await _tagRepository.Delete(id);

    [HttpGet("GetAll")]
    public async Task<ICollection<TagDTO>> GetAll() 
        => await _tagRepository.GetAll();   
}