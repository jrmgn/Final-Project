using Microsoft.EntityFrameworkCore;

namespace loginpage.Models
{
    //attributes declared for database
    public class Product
    {
        public int Id { get; set; }
        public string Medicine_name { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;

        [Precision(16,2)]
        public decimal Price { get; set; } 
        public bool Prescription { get; set; }
        public string ImgFileName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Category Category { get; set; } 
        public DosageForm DosageForm { get; set; }

        public string Description { get; set; } = string.Empty;
    }

    public enum Category
    {
        PainRelief,
        FeverReducer,
        Antibiotic,
        CoughCold,
        Antacid,
        Laxative,
        Antihistamine
    }

    public enum DosageForm
    {
        Capsule,
        Tablet,
        Syrup
    }
}
