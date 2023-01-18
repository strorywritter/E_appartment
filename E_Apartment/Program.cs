using E_Apartment_DataAccess.EfCore;
using E_Apartment_Logic.ApartmentLogic;
using E_Apartment_Logic.BuildingLogic;
using E_Apartment_Logic.LeaseLogic;
using E_Apartment_Logic.Logic;
using E_Apartment_Logic.Occupier;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace E_Apartment
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);        

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        //ApplicationConfiguration.Initialize();
        //Application.Run(new Form1());

        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var host = CreateHostBuilder().Build();
        ServiceProvider = host.Services;

        Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }
        static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Handle the exception here
            // For example, you can show a message box to the user
            MessageBox.Show("An error occurred: " + e.Exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<Form1>();
                    services.AddDbContext<EApartmentDbContext>(options => options.UseSqlServer("Data Source=DESKTOP-KDT3DR0\\SQLEXPRESS; Initial Catalog=e_apartment;User ID=SQL-ADMIN;Password=admin@123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
                    services.AddScoped<IApartmentLogic,ApartmentLogic>();
                    services.AddScoped<IBuildingLogic, BuildingLogic>();
                    services.AddScoped<IOccupierLogic, OccupierLogic>();
                    services.AddScoped<ILoginLogic, LoginLogic>();
                    services.AddScoped<ILeaseLogic, LeaseLogic>();
                });
        }
    }
}