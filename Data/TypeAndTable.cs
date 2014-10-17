using System;

namespace Skattedugnad.Data
{
    public class TypeAndTable
    {
        public Type Type { get; private set; }
        public string TableName { get; private set; }

        public TypeAndTable(Type type, string tableName)
        {
            Type = type;
            TableName = tableName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TypeAndTable);
        }

        public bool Equals(TypeAndTable other)
        {
            return other != null &&
                   other.Type == Type &&
                   other.TableName == TableName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Type != null ? Type.GetHashCode() : 0) * 397) ^ (TableName != null ? TableName.GetHashCode() : 0);
            }
        }
    }
}