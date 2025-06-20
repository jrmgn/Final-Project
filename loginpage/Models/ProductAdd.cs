using Microsoft.EntityFrameworkCore;

namespace loginpage.Models
{
    public class ProductAdd
    { 
        public string Medicine_name { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;

        [Precision(16, 2)]
        public decimal Price { get; set; }
        public bool Prescription { get; set; }
        public IFormFile? ImgFile { get; set; }
        public Category Category { get; set; }
        public DosageForm DosageForm { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
