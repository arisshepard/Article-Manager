using System.Threading.Tasks;
using article_manager.Models;
using article_manager.Services;
using frontendlib.Models;
using Microsoft.AspNetCore.Components;

namespace article_manager.Pages
{
    public class ArticleCategoriesBase : ComponentBase
    {
        [Inject]
        private ICRUDService<ArticleCategoryListItem, ArticleCategoryItem> service { get; set; }

        protected ItemListModel categoriesModel = new ItemListModel()
        {
            ItemName = "Category",
            Headers = new string[] { "Id", "Name" },
            Items = new ArticleCategoryListItem[0]
        };

        protected ItemDetailsModel<ArticleCategoryItem> categoryModel = new ItemDetailsModel<ArticleCategoryItem>()
        {
            ItemName = "Category"
        };

        protected string error;

        protected override async Task OnInitializedAsync()
        {
            await this.ShowList();
        }

        public async Task AddCategory()
        {
            this.categoryModel.Item = await service.GetNew();
        }

        public async Task DeleteCategory(object item)
        {
            try
            {
                await service.Delete(((ArticleCategoryListItem)item).ID);
                await ShowList();
            }
            catch (System.Exception ex)
            {

                this.error = ex.Message;
            }
        }

        public async Task EditCategory(object item)
        {
            this.categoryModel.Item = await service.Get(((ArticleCategoryListItem)item).ID);
        }

        public async Task SaveCategory()
        {
            try
            {
                if (categoryModel.Item.Id == 0)
                {
                    await service.Create(categoryModel.Item);
                }
                else
                {
                    await service.Update(categoryModel.Item);
                }
            }
            catch (System.Exception ex)
            {
                this.error = ex.Message;
            }
        }

        public async Task ShowList()
        {
            categoriesModel.Items = await service.GetList();
            categoryModel.Item = null;
        }
    }
}