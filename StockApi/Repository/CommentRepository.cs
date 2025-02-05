using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApi.Data;
using StockApi.Interfaces;
using StockApi.Models;

namespace StockApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _ctx;
        public CommentRepository(ApplicationDbContext dbContext) //DI
        {
            _ctx = dbContext;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _ctx.Comments.AddAsync(commentModel);
            await _ctx.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _ctx.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null) {
                return null;
            }

            _ctx.Comments.Remove(commentModel);
            await _ctx.SaveChangesAsync();

            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _ctx.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _ctx.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateAsync(int id, Comment commentModel)
        {
            var existingComment = await _ctx.Comments.FindAsync(id);
            if (existingComment == null) {
                return null;
            }
            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _ctx.SaveChangesAsync();

            return existingComment;
        }
    }
}
