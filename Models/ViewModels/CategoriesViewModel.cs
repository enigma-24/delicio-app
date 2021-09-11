using System.Collections.Generic;

namespace delicio_app.Models.ViewModels
{
    public class CategoriesViewModel{
        public IEnumerable<Category> CategoryList { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<string> SubCategoryList { get; set; }
        public string StatusMessage { get; set; }
    }
}