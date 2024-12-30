using System.Data;
using System.Data.SqlClient;
namespace WebApi.Models;
public class SiteProvider
{
    IDbConnection? connection;
    IConfiguration configuration;
    public SiteProvider(IConfiguration configuration){
        this.configuration = configuration;
    }

    
    protected IDbConnection Connection => connection ??= new SqlConnection(configuration.GetConnectionString("Store"));

    UserRepository? user;
    public UserRepository User => user ??= new UserRepository(Connection);
    CategoryRepository? category;
    public CategoryRepository Category => category ??= new CategoryRepository(Connection);
    ProductRepository? product;
    public ProductRepository Product => product ??= new ProductRepository(Connection);
    RoleRepository? role;
    public RoleRepository Role => role ??= new RoleRepository(Connection);
    CartRepository? cart;
    public CartRepository Cart => cart ??= new CartRepository(Connection);
    InvoiceRepository? invoice;
    public InvoiceRepository Invoice => invoice ??= new InvoiceRepository(Connection);
    VnPaymentRepository? vnPayment;
    public VnPaymentRepository VnPayment => vnPayment ??= new VnPaymentRepository(Connection);
    AccessRepository? access;
    public AccessRepository Access =>access ??= new AccessRepository(Connection);
}