using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "MissingName", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "MissingPrice", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        [RegularExpression(@"^-?\d+(\.\d{1,2})?$", ErrorMessageResourceName = "PriceNotANumber", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        [Range(0, double.MaxValue, ErrorMessageResourceName = "PriceNotGreaterThanZero", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        public string Price { get; set; }

        [Required(ErrorMessageResourceName = "MissingStock", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        [RegularExpression("^-?[0-9]+$", ErrorMessageResourceName = "StockNotAnInteger", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        [Range(0, int.MaxValue, ErrorMessageResourceName = "StockNotGreaterThanZero", ErrorMessageResourceType = typeof(Resources.Models.Services.ProductService))]
        public string Stock { get; set; }


        public string Description { get; set; }

        public string Details { get; set; }
    }
}
