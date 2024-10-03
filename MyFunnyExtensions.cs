using Microsoft.OpenApi.Models;

namespace Groceries.Extensions
{
    public static class MyFunnyExtensions
    {
        public static int? FakeAssMethod(this Product p) 
        { 
            
            return p.id; 
        }
    }
}
