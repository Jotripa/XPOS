using XPOS_FE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add service to call API
builder.Services.AddScoped<CategoryService>();
//Add Variant Service to call API
builder.Services.AddScoped<VariantService>();
//Add Product service to call API
builder.Services.AddScoped<ProductService>();
//Add Order service to call API
builder.Services.AddScoped<OrderService>();
//Add Auth service to call API
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
