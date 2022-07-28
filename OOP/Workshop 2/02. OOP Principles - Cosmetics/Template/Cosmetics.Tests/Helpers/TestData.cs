using Cosmetics.Models;

namespace Cosmetics.Tests.Helpers
{
    public class TestData
    {
        public static class ShampooData
        {
            public static string ValidName = new string('x', Shampoo.NameMinLength + 1);
            public static string InvalidName = new string('x', Shampoo.NameMinLength - 1);

            public static string ValidBrand = new string('x', Shampoo.BrandMinLength + 1);
            public static string InvalidBrand = new string('x', Shampoo.BrandMinLength - 1);
        }

        public static class ToothpasteData
        {
            public static string ValidName = new string('x', Toothpaste.NameMinLength + 1);
            public static string InvalidName = new string('x', Toothpaste.NameMinLength - 1);

            public static string ValidBrand = new string('x', Toothpaste.BrandMinLength + 1);
            public static string InvalidBrand = new string('x', Toothpaste.BrandMinLength - 1);
        }
    }
}
