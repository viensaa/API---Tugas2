using Tugas2FE.Interfaces;
using Tugas2FE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//membuat session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Mysession.frontend";
    options.IdleTimeout = TimeSpan.FromMinutes(2);
    options.Cookie.IsEssential = true;
});




//injek dependensices
builder.Services.AddScoped<IStudent, StudentServices>();
builder.Services.AddScoped<ICourse, CourseServices>();
builder.Services.AddScoped<IEnrollment, EnrollmentServices>();
builder.Services.AddScoped<IAccount, AccountServices>();

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
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
