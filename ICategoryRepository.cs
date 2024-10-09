namespace Groceries
{
    public interface ICategoryRepository
    {
        void DeleteCategoryById(int id);
        Category GetCategoryById(int id);
        Category GetCategoryByName(Category category);
        Category UpdateCategoryById(int id, Category updatedCategory);
    }
}