namespace Shop.Common.Entities
{
    public class Product : BaseEntity<long>
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ProductDesc { get; set; }
        public string Photo { get; set; }
        public short ProductCategory { get; set; }
        public short? NumberOfSeat { get; set; }
    }
}
