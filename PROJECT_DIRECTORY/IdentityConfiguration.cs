using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PROJECT_DIRECTORY;

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
    public static IConfigureOptions<IdentityOptions> For(WebApplicationBuilder? builder) =>
        new IdentityConfiguration(builder?.Environment.IsDevelopment() == true
            ? ConfigureDevelopment
            : ConfigureNonDevelopment
            );
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
