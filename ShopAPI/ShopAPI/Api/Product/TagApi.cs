using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ShopApi.Model.Entities.DTO.Tag;
using ShopApi.Model.Interfaces.Repository;
using ShopDb.Entities;

namespace ShopApi.Api.Product
{
    [Route("api/Product/Tag")]
    [ApiController]
    public class TagApi : ControllerBase
    {
        private readonly ITagRepository _tagRepository;

        public TagApi(ITagRepository tagRepository) => _tagRepository= tagRepository;

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
}
