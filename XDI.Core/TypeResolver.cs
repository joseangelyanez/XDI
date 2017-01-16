using System;
using System.Collections.Generic;
using System.Data.Common;

namespace XDI.Code
{
    public class TypeResolver
    {
        private readonly static Dictionary<string, Type> _typeMap = new Dictionary<string, Type>() {
            { "char", typeof(string) },
            { "varchar", typeof(string) },
            { "string", typeof(string) },
            { "text", typeof(string) },
            { "guid", typeof(Guid) },
            { "blob", typeof(byte[]) },
            { "boolean", typeof(bool) },
            { "bool", typeof(bool) },
            { "float", typeof(float) },
            { "double", typeof(double) },
            { "int", typeof(decimal) },
            { "decimal", typeof(decimal) },
            { "numeric", typeof(decimal) },
            { "money", typeof(decimal) },
            { "bit", typeof(bool) },
            { "varbinary", typeof(byte[]) },
            { "image", typeof(byte[]) },
            { "date", typeof(DateTime) },
            { "datetime", typeof(DateTime) },
        };
        
        public Type GetClrTypeFromParameter(DbParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            string type = parameter.DbType.ToString().ToLower();

            if (_typeMap.ContainsKey(type))
                return _typeMap[type];
            else
                return typeof(object);
        }

        public Type GetClrTypeFromFriendlyName(string friendlyName)
        {
            if (friendlyName == null)
                throw new ArgumentNullException(nameof(friendlyName));

            friendlyName = friendlyName.Trim().ToLower();

            if (!_typeMap.ContainsKey(friendlyName))
                throw new InvalidXdiTypeException($"The type {friendlyName} could not be resolved.");

            return _typeMap[friendlyName];
        }
    }
}
