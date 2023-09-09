using Autofac;
using AutoMapper;
using CompanyTask.Application.AutoMapper;
using CompanyTask.Application.Services.AccountServices;
using CompanyTask.Application.Services.AddressService;
using CompanyTask.Application.Services.CompanyManagerService;
using CompanyTask.Application.Services.DepartmentService;
using CompanyTask.Application.Services.PersonelService;
using CompanyTask.Application.Services.TitleService;
using CompanyTask.Domain.Repositories;
using CompanyTask.Infrastructure.Repositories;

namespace CompanyTask.Application.IoC
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterType<AddressRepository>().As<IAddressRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentRepository>().As<IDepartmentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DistrictRepository>().As<IDistrictRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TitleRepository>().As<ITitleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CompanySectorRepository>().As<ICompanySectorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();

            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<PersonelService>().As<IPersonelService>().InstancePerLifetimeScope();
            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerLifetimeScope();
            builder.RegisterType<DepartmentService>().As<IDepartmentService>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyManagerService>().As<ICompanyManagerService>().InstancePerLifetimeScope();
            builder.RegisterType<TitleService>().As<ITitleService>().InstancePerLifetimeScope();


            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>(); /// AutoMapper klasörünün altına eklediğimiz Mapping classını bağlıyoruz.
            }
            )).AsSelf().SingleInstance();



            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            #endregion

            base.Load(builder);
        }
    }
}
