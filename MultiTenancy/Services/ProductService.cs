using Microsoft.EntityFrameworkCore;

namespace MultiTenancy.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    public string TenantId { get; set; }
    private readonly ITenantService _tenantService;
    public ProductService(ApplicationDbContext context, ITenantService tenantService)
    {
        _context = context;
        _tenantService = tenantService;
        TenantId = _tenantService.GetCurrentTenant()?.TId;
    }

    public async Task<Product> CreatedAsync(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync()
    {
        // For disable MultiTenancy:

        if (TenantId == "Guest" )
        {
            return await _context.Products.IgnoreQueryFilters().ToListAsync();
        }
            return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<long> GetCountAsync()
    {
        var items = TenantId == "Guest" ? await _context.Products.IgnoreQueryFilters().ToListAsync()
            : await _context.Products.ToListAsync();
        return items.Count;
    }
}