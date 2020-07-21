using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using DigitalHealth.GlobalInterfaces;
using DigitalHealth.Services;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<DiagnosticService>().As<IDiagnosticService>();
            builder.RegisterType<DiseaseCRUDService>().As<IDiseaseCRUDService>();
            builder.RegisterType<ICDCRUDService>().As<IICDCRUDService>();
            builder.RegisterType<MarkCRUDService>().As<IMarkCRUDService>();
            builder.RegisterType<MethodOfTreatmentCRUDService>().As<IMethodOfTreatmentCRUDService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<SymptomCRUDService>().As<ISymptomCRUDService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<Logger>().As<ILogger>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}