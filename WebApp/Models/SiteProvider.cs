using Microsoft.Extensions.Caching.Memory;

namespace WebApp.Models;
public class SiteProvider
{
    IConfiguration configuration;
    IMemoryCache cache;
    public SiteProvider(IConfiguration configuration, IMemoryCache cache)
    {
        this.configuration = configuration;
        this.cache = cache;
    }
    ProductRepository? product;
    public ProductRepository Product => product ??= new ProductRepository(configuration);
    CategoryRepository? category;
    public CategoryRepository Category => category ??= new CategoryRepository(configuration,cache);
    UploadRepository? upload;
    public UploadRepository Upload => upload ??= new UploadRepository(configuration);
    UserRepository? user;
    public UserRepository User=> user ??= new UserRepository(configuration);
    RoleRepository? role;
    public RoleRepository Role => role ??= new RoleRepository(configuration);
    CartRepository? cart;
    public CartRepository Cart => cart ??= new CartRepository(configuration);
    InvoiceRepository? invoice;
    public InvoiceRepository Invoice => invoice ??= new InvoiceRepository(configuration);
}