using DataAccess.Repositories;
using Models.Dto;
using Models.Dto.ShowData;
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
        private readonly IImageService _imageService;

        public PostService(IPostRepository postRepository, IImageService imageService)
        {
            _postRepository = postRepository;
            _imageService = imageService;
        }

        public async Task<IEnumerable<PostShow>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return posts.Select(post => new PostShow
            {
                Id = post.Id,
                Title = post.Title,
                User = post.User,
                CreatedAt = post.CreatedAt,
                CategoryId = post.CategoryId,
                PostSections = (ICollection<PostSectionShow>)post.PostSections.Select(async section => new PostSectionShow
                {
                    Id = section.Id,
                    SectionText = section.SectionText,
                    ImageId = section.ImageId,
                    ImageFile = await _imageService.GetImageFile(section.ImageId ?? Guid.Empty),
                    SectionOrder = section.SectionOrder
                }).ToList(),
                Comments = (ICollection<CommentModel>)post.Comments.Select(comment => new CommentDtoShow
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                    UserId = comment.UserId,
                    UserName = comment.User.UserName,
                    CreatedAt = comment.CreatedAt
                }).ToList(),
                Reacts = (ICollection<ReactModel>)post.Reacts.Select(react => new ReactDtoShow
                {
                    Id = react.Id,
                    UserId = react.UserId,
                    UserName = react.User.UserName,
                    Liked = react.Liked
                }).ToList(),
                TagNames = post.Tags.Select(tag => tag.Name).ToList()
            });
        }

        public async Task<PostShow> GetPostByIdAsync(Guid id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return null;

            return new PostShow
            {
                Id = post.Id,
                Title = post.Title,
                User = post.User,
                CreatedAt = post.CreatedAt,
                CategoryId = post.CategoryId,
                PostSections = (ICollection<PostSectionShow>)post.PostSections.Select(async section => new PostSectionShow
                {
                    Id = section.Id,
                    SectionText = section.SectionText,
                    ImageId = section.ImageId,
                    ImageFile = await _imageService.GetImageFile(section.ImageId ?? Guid.Empty),
                    SectionOrder = section.SectionOrder
                }).ToList(),
                Comments = (ICollection<CommentModel>)post.Comments.Select(comment => new CommentDtoShow
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                    UserId = comment.UserId,
                    UserName = comment.User.UserName,
                    CreatedAt = comment.CreatedAt
                }).ToList(),
                Reacts = (ICollection<ReactModel>)post.Reacts.Select(react => new ReactDtoShow
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
                UserId = postDto.UserId,
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
            post.UserId = postDto.UserId;
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
