using DataAccess.Repositories;
using Models.Dto;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Imp
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(post => new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Author = post.Author,
                CreatedAt = post.CreatedAt,
                CategoryId = post.CategoryId,
                CategoryName = post.Category.Name,
                PostSections = post.PostSections.Select(section => new PostSectionDTO
                {
                    Id = section.Id,
                    SectionText = section.SectionText,
                    ImageId = section.ImageId,
                    ImageUrl = section.Image != null ? $"/images/{section.ImageId}.jpg" : null,
                    SectionOrder = section.SectionOrder
                }).ToList(),
                Comments = post.Comments.Select(comment => new CommentDTO
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                    UserId = comment.UserId,
                    UserName = comment.User.UserName,
                    CreatedAt = comment.CreatedAt
                }).ToList(),
                Reacts = post.Reacts.Select(react => new ReactDTO
                {
                    Id = react.Id,
                    UserId = react.UserId,
                    UserName = react.User.UserName,
                    Liked = react.Liked
                }).ToList(),
                TagNames = post.Tags.Select(tag => tag.Name).ToList()
            });
        }

        public async Task<PostDTO> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return null;

            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                Author = post.Author,
                CreatedAt = post.CreatedAt,
                CategoryId = post.CategoryId,
                CategoryName = post.Category.Name,
                PostSections = post.PostSections.Select(section => new PostSectionDTO
                {
                    Id = section.Id,
                    SectionText = section.SectionText,
                    ImageId = section.ImageId,
                    ImageUrl = section.Image != null ? $"/images/{section.ImageId}.jpg" : null,
                    SectionOrder = section.SectionOrder
                }).ToList(),
                Comments = post.Comments.Select(comment => new CommentDTO
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                    UserId = comment.UserId,
                    UserName = comment.User.UserName,
                    CreatedAt = comment.CreatedAt
                }).ToList(),
                Reacts = post.Reacts.Select(react => new ReactDTO
                {
                    Id = react.Id,
                    UserId = react.UserId,
                    UserName = react.User.UserName,
                    Liked = react.Liked
                }).ToList(),
                TagNames = post.Tags.Select(tag => tag.Name).ToList()
            };
        }

        public async Task CreatePostAsync(PostDTO postDto)
        {
            var post = new PostModel
            {
                Id = Guid.NewGuid(),
                Title = postDto.Title,
                Author = postDto.Author,
                CreatedAt = DateTime.UtcNow,
                CategoryId = postDto.CategoryId,
                PostSections = postDto.PostSections.Select(section => new PostSectionModel
                {
                    SectionText = section.SectionText,
                    SectionOrder = section.SectionOrder,
                    ImageId = section.ImageId
                }).ToList()
            };

            await _postRepository.AddAsync(post);
        }

        public async Task UpdatePostAsync(PostDTO postDto)
        {
            var post = await _postRepository.GetByIdAsync(postDto.Id);
            if (post == null) return;

            post.Title = postDto.Title;
            post.Author = postDto.Author;
            post.CategoryId = postDto.CategoryId;
            post.PostSections = postDto.PostSections.Select(section => new PostSectionModel
            {
                Id = section.Id,
                SectionText = section.SectionText,
                SectionOrder = section.SectionOrder,
                ImageId = section.ImageId
            }).ToList();

            await _postRepository.UpdateAsync(post);
        }

        public async Task DeletePostAsync(Guid id)
        {
            await _postRepository.DeleteAsync(id);
        }
    }
}
