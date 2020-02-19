using System;
using System.Collections.Generic;
using System.Text;

namespace PantryAid.Core.Utilities
{
    public interface iSqlServerDataAccess
    {
        int ExecuteQuery_NoReturnType(string sql);
        List<T> ExecuteQuery_ExpectedListReturn<T>(string sql);
        T ExecuteQuery_SingleReturnItem<T>(string sql);
    }
}
