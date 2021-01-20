using System;
using System.Linq;
using System.Threading.Tasks;
using article_manager.Data;
using article_manager.Data.Entities;
using article_manager.Models;
using Microsoft.EntityFrameworkCore;

namespace article_manager.Services
{
    public class ArticleCategoriesService : ICRUDService<ArticleCategoryListItem, ArticleCategoryItem>
    {
        private readonly BDArticleManagementContext _context;

        public ArticleCategoriesService(BDArticleManagementContext context)
        {
            _context = context;
        }


        public async Task Create(ArticleCategoryItem item)
        {
            var entity = new ArticleCategory()
            {
                Name = item.Name,
                Description = item.Description
            };

            _context.Add(entity);
            await _context.SaveChangesAsync();
            item.Id = entity.Id;
        }

        public Task Delete(int id)
        {
            var entity = _context.ArticleCategories.SingleOrDefault(categoryDb => categoryDb.Id == id);
            if (entity == null) throw new ArgumentException("Item not found", "item");
            _context.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task<ArticleCategoryItem> Get(int id)
        {
            return _context.ArticleCategories
            .Where(categoryDb => categoryDb.Id == id)
            .Select(categoryDb => new ArticleCategoryItem()
            {
                Description = categoryDb.Description,
                Id = categoryDb.Id,
                Name = categoryDb.Name
            }).SingleOrDefaultAsync();
        }

        public Task<ArticleCategoryListItem[]> GetList()
        {
            return _context.ArticleCategories
            .Select(categoryDb => new ArticleCategoryListItem()
            {
                ID = categoryDb.Id,
                Name = categoryDb.Name

            }).ToArrayAsync();
        }

        public Task<ArticleCategoryItem> GetNew()
        {
            return Task.FromResult(
                new ArticleCategoryItem()
            );
        }

        public Task Update(ArticleCategoryItem item)
        {
            var entity = _context.ArticleCategories.SingleOrDefault(categoryDb => categoryDb.Id == item.Id);
            if (entity == null) throw new ArgumentException("Item not found", "item");
            entity.Name = item.Name;
            entity.Description = item.Description;
            return _context.SaveChangesAsync();
        }
    }
}