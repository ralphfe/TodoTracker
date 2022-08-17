using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get conenction string from configuration
var todoListConnectionString = builder.Configuration.GetConnectionString("TodoItemsConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Todo: This will throw exception if connection string is not set up properly
builder.Services.AddDbContext<TodoListMvc.Models.TodoDbContext>(m => m.UseSqlServer(todoListConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
