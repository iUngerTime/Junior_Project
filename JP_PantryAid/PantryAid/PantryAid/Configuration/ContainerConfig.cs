using Autofac;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Utilities;
using PantryAid.ViewModels;

namespace PantryAid.Configuration
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            //Interface Registration
            builder.RegisterType<SqlServerDataAccess>().As<iSqlServerDataAccess>();
            builder.RegisterType<IngredientData>().As<iIngredientData>();
            builder.RegisterType<UserData>().As<iUserDataRepo>();

            //Build and return
            return builder.Build();
        }
    }
}
