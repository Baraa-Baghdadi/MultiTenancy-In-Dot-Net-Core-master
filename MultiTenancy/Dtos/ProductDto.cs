namespace MultiTenancy.Dtos
{
    public class ProductDto
    {
        public long totalItem { get; set; }
        public IReadOnlyList<Product> Items { get; set; }
    }
}
