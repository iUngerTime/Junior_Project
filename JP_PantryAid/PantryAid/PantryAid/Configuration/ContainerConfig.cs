using Autofac;
using Database_Helpers;
using PantryAid.Core.Interfaces;
using PantryAid.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Configuration
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SqlServerDataAccess>().As<iSqlServerDataAccess>();
            builder.RegisterType<IngredientData>().As<iIngredientData>();
            builder.RegisterType<UserData>().As<iUserDataRepo>();

            return builder.Build();
        }
    }
}
