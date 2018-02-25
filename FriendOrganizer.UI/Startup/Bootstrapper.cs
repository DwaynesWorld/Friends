using Autofac;
using FriendOrganizer.DataAccess;
using FriendOrganizer.UI.Services;
using FriendOrganizer.UI.ViewModels;
using Prism.Events;

namespace FriendOrganizer.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            // Handle all Application Events
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            //Database Context Creation
            builder.RegisterType<FriendOrganizerDbContext>().AsSelf();

            // Models
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();

            // Services
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<FriendDataService>().As<IFriendDataService>();

            return builder.Build();
        }
    }
}
