using XPOS_FE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
//Add Menu service to call API
builder.Services.AddScoped<MenuService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
