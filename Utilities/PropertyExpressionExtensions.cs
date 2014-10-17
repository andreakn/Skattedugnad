using System;
using System.Linq.Expressions;

namespace Skattedugnad.Utilities
{
    public static class PropertyExpressionExtensions
    {
        public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                return string.Empty;
            }
            var parameter = expression.Parameters[0].Name;
            var pattern = string.Format("{0}.", parameter);
            var body = GetMemberExpressionFrom(expression.Body).ToString();
            return body.RemoveFirstMatch(pattern);
        }

        private static MemberExpression GetMemberExpressionFrom(Expression expression)
        {
            return DoGetMemberExpressionFrom((dynamic)expression);
        }

        private static MemberExpression DoGetMemberExpressionFrom(UnaryExpression expression)
        {
            return GetMemberExpressionFrom((dynamic)expression.Operand);
        }

        private static MemberExpression DoGetMemberExpressionFrom(MemberExpression expression)
        {
            return expression;
        }

        private static MemberExpression DoGetMemberExpressionFrom(object invalid)
        {
            throw new InvalidOperationException(string.Format("Don't know how to get a MemberExpression from {0}", invalid.GetType().Name));
        } 
    }
}