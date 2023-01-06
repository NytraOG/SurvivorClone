using CoOrga.Survivors.Frontend.Blazor.Server.Services;
using CoOrga.Survivors.Frontend.Module;
using CoOrga.Survivors.Frontend.Module.BusinessObjects;
using DevExpress.ExpressApp.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.ApplicationBuilder;
using DevExpress.ExpressApp.Blazor.Services;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;

namespace CoOrga.Survivors.Frontend.Blazor.Server;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(typeof(HubConnectionHandler<>), typeof(ProxyHubConnectionHandler<>));

        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddHttpContextAccessor();
        services.AddScoped<CircuitHandler, CircuitHandlerProxy>();

        services.AddXaf(Configuration, builder =>
        {
            builder.UseApplication<The_GUIBlazorApplication>();

            builder.Modules
                   .AddAuditTrailXpo(options =>
                    {
                        options.AuditDataItemPersistentType = typeof(AuditDataItemPersistent);
                    })
                   .AddConditionalAppearance()
                   .AddValidation(options =>
                    {
                        options.AllowValidationDetailsAccess = false;
                    })
                   .Add<FrontendModule>()
                   .Add<FrontendBlazorModule>();

            builder.ObjectSpaceProviders
                   .AddSecuredXpo((serviceProvider, options) =>
                    {
                        string connectionString = null;

                        if (Configuration.GetConnectionString("ConnectionString") != null)
                            connectionString = Configuration.GetConnectionString("ConnectionString");
#if EASYTEST
                    if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                        connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                    }
#endif
                        ArgumentNullException.ThrowIfNull(connectionString);
                        options.ConnectionString           = connectionString;
                        options.ThreadSafe                 = true;
                        options.UseSharedDataStoreProvider = true;
                    })
                   .AddNonPersistent();

            builder.Security
                   .UseIntegratedMode(options =>
                    {
                        options.RoleType = typeof(PermissionPolicyRole);
                        // ApplicationUser descends from PermissionPolicyUser and supports the OAuth authentication. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
                        // If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
                        options.UserType = typeof(ApplicationUser);
                        // ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
                        // If you use PermissionPolicyUser or a custom user type, comment out the following line:
                        options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);
                        options.UseXpoPermissionsCaching();
                    })
                   .AddPasswordAuthentication(options =>
                    {
                        options.IsSupportChangePassword = true;
                    });
        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                 {
                     options.LoginPath = "/LoginPage";
                 });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRequestLocalization(); // Add This
        var provider = new FileExtensionContentTypeProvider();
        provider.Mappings.Remove(".data");
        provider.Mappings[".data"] = "application/octet-stream";
        provider.Mappings.Remove(".wasm");
        provider.Mappings[".wasm"] = "application/wasm";
        provider.Mappings.Remove(".symbols.json");
        provider.Mappings[".symbols.json"] = "application/octet-stream";
        app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });
        app.UseStaticFiles();
        //--------------
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseXaf();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapXafEndpoints();
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
            endpoints.MapControllers();
        });
        ElectronBootstrap();
    }

    public async void ElectronBootstrap()
    {
        var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
        {
            Width  = 1152,
            Height = 940,
            Show   = false
        });
        await browserWindow.WebContents.Session.ClearCacheAsync();
        browserWindow.OnReadyToShow += () => browserWindow.Show();
        browserWindow.SetTitle("JetBrains!");
    }
}