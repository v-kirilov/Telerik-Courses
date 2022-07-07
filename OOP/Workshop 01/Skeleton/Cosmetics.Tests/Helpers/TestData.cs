using Cosmetics.Models;

namespace Cosmetics.Tests.Helpers
{
    public class TestData
    {
        public static class CategoryData
        {
            public static string ValidName = new string('x', Category.NameMinLength + 1);
        }

        public static class ProductData
        {
            public static string ValidName = new string('x', Product.NameMinLength + 1);
            public static string ValidBrand = new string('x', Product.BrandMinLength + 1);
        }
    }
}
