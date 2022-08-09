
using Avero.Core.Entities;

namespace Avero.Web.ViewModels.Admin
{
    public class viewProductsViewModel
    {
        public IEnumerable<Product>? products { get; set; }  
        public int productPerPage { get; set; }  
        public int currentPage { get; set; }

        public int PageCount()  
        {  
            return Convert.ToInt32(Math.Ceiling(products.Count() / (double)productPerPage));  
        }  
        public IEnumerable<Product> PaginatedProduct()  
        {  
            int start = (currentPage - 1) * productPerPage;  
            return products.OrderBy(b=>b.Id).Skip(start).Take(productPerPage);  
        }  
    }  
}
