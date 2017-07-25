﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Blog.Infrastructure;
using BlogCore.Core;
using MediatR;

namespace BlogCore.Blog.UseCases.ListOutBlog
{
    public class ListOutBlogInteractor : IAsyncRequestHandler<ListOutBlogRequest, IEnumerable<ListOutBlogResponse>>
    {
        private readonly IRepository<BlogDbContext, Domain.Blog> _blogRepo;

        public ListOutBlogInteractor(IRepository<BlogDbContext, Domain.Blog> blogRepo)
        {
            _blogRepo = blogRepo;
        }

        public async Task<IEnumerable<ListOutBlogResponse>> Handle(ListOutBlogRequest request)
        {
            var blogs = await _blogRepo.ListAsync(new PageInfo(request.Page, 10));
            var responses = blogs
                .Select(x => new ListOutBlogResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.ImageFilePath,
                    (int)x.Theme
                ));

            return responses;
        }
    }
}