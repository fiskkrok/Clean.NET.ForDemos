# Development Goals

* ### Model the domain
* ### Design a UI
	* Razor Pages Application
* ### Persis the model
	* Start off with EF Core
	* Other technologies later, if needed
* ### Authenticate users
	* ASP.NET Core Identity
-------------------
# Sticking to Principles
* Take least obligations!
* Start small and grow - don't over-engineer
## Let the Project evolve
* Don't try to predict the future
* Don't try to solve problems that don't exist

> Apply progressively as the project forms
---

```csharp
public class PROJECT_DIRECTORYIdentityDbContext : IdentityDbContext<IdentityUser>
{
	public PROJECT_DIRECTORYIdentityDbContext(DbContextOptions<PROJECT_DIRECTORYIdentityDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		// Added to change the default schema from dbo to Authentication
		builder.HasDefaultSchema("Authentication"); // <--- Add this line
	}
}
```

Then in the Program.cs file, add the following line to the ConfigureServices method:

```csharp
var builder = WebApplication.CreateBuilder(args);

//other code

// Added options.Conventions.AuthorizePage("/Index") to require authorization for the Index page
builder.Services.AddRazorPages(options => options.Conventions.AuthorizePage("/Index")); // <--- Add this line

//other code
app.MapRazorPages();

app.Run();
```

Then for development purposes, you can add Configuration so you don't have to follow all the rules
for passwords:

```csharp
public class IdentityConfiguration : IConfigureOptions<IdentityOptions>
{
	//You can add this too your Project as part of development but make sure to remove it before deploying to production
	private Action<IdentityOptions> ActualConfigure { get; }

	public IdentityConfiguration(Action<IdentityOptions> actualConfigure)
	{
		this.ActualConfigure = actualConfigure;
	}

	public void Configure(IdentityOptions options)
	{
		this.ActualConfigure(options);
	}

	// This is the method that runs either options based on the environment
	public static IConfigureOptions<IdentityOptions> For(WebApplicationBuilder? builder) =>
		new IdentityConfiguration(builder?.Environment.IsDevelopment() == true
			? ConfigureDevelopment
			: ConfigureNonDevelopment
			);


			//Here you can choose whatever options you want for development and non-development
	private static void ConfigureDevelopment(IdentityOptions options)
	{
		options.Password.RequireDigit = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Password.RequiredLength = 6;
		options.Password.RequiredUniqueChars = 0;
		options.SignIn.RequireConfirmedAccount = false;
		options.SignIn.RequireConfirmedEmail = false;
		options.SignIn.RequireConfirmedPhoneNumber = false;
		options.User.RequireUniqueEmail = true;
	}

	private static void ConfigureNonDevelopment(IdentityOptions options)
	{
		options.SignIn.RequireConfirmedAccount = true;
	}
}
```
