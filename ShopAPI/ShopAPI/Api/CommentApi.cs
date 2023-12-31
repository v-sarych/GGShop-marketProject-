﻿using IdentityServer.Model.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApiServer.Extentions;
using ShopApiCore.Entities.DTO.Comment;
using ShopApiCore.Interfaces.Repository;
using ShopDb.Enums;

namespace ShopApiServer.Api
{
    [Route("api/Product/Comment")]
    [ApiController]
    public class CommentApi : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApi(ICommentRepository commentRepository) => _commentRepository = commentRepository;

        [HttpPost("Get")]
        public async Task<ICollection<CommentDTO>> Get(GetCommentsDTO gettingSettings)
            => await _commentRepository.Get(gettingSettings);

        [Authorize]
        [HttpPut("Edit")]
        public async Task Edit(EditCommentDTO edittingSettings)
            => await _commentRepository.Edit(edittingSettings, 
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        /// <response code="200">Sucess</response>
        /// <response code="400">AlreadyExist</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        [HttpPost("Create")]
        public async Task Create(CreateCommentDTO createtingSettings)
            => await _commentRepository.Create(createtingSettings,
                Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));

        [Authorize]
        [HttpDelete("Delete")]
        public async Task Delete(int productId, long userId = 0)
        {
            if(userId == 0)
            {
                await _commentRepository.Delete(productId,
                    Convert.ToInt64(Request.HttpContext.User.FindFirst(ClaimTypes.UserId).Value));
                return;
            }
            else if(Request.HttpContext.User.FindFirst(ClaimTypes.Role).Value == Roles.Admin)
            {
                await _commentRepository.Delete(productId, userId);
                return;
            }

            throw new NoPermissionsException();
        }
    }
}
