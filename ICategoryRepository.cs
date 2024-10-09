namespace Groceries
{
    public interface ICategoryRepository
    {
        void DeleteCategoryById(int id);
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        Category UpdateCategoryById(int id, Category updatedCategory);
    }
}