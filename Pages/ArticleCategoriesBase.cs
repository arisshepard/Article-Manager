using System.Threading.Tasks;
using article_manager.Models;
using article_manager.Services;
using Microsoft.AspNetCore.Components;

namespace article_manager.Pages
{
    public class ArticleCategoriesBase : ComponentBase
    {
        [Inject]
        private ICRUDService<ArticleCategoryListItem, ArticleCategoryItem> service { get; set; }

        protected ArticleCategoryListItem[] articleCategoryListItems;
        protected ArticleCategoryItem currentCategory;
        protected string error;

        protected override async Task OnInitializedAsync()
        {
            await this.ShowList();
        }

        public async Task AddCategory()
        {
            this.currentCategory = await service.GetNew();
        }

        public async Task DeleteCategory(ArticleCategoryListItem item)
        {
            try
            {
                await service.Delete(item.ID);
                await ShowList();
            }
            catch (System.Exception ex)
            {

                this.error = ex.Message;
            }
        }

        public async Task EditCategory(ArticleCategoryListItem item)
        {
            this.currentCategory = await service.Get(item.ID);
        }

        public async Task SaveCategory()
        {
            try
            {
                if (currentCategory.Id == 0)
                {
                    await service.Create(currentCategory);
                }
                else
                {
                    await service.Update(currentCategory);
                }
            }
            catch (System.Exception ex)
            {
                this.error = ex.Message;
            }
        }

        public async Task ShowList()
        {
            articleCategoryListItems = await service.GetList();
            currentCategory = null;
        }
    }
}