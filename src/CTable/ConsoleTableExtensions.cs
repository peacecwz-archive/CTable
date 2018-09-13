using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace CTable
{
    public static class ConsoleTableExtensions
    {
        public static string ToStringTable<T>(this IEnumerable<T> values, string[] columnHeaders,
            params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        public static string ToStringTable<T>(this T[] values, string[] columnHeaders,
            params Func<T, object>[] valueSelectors)
        {
            var tableValues = new string[values.Length + 1, valueSelectors.Length];

            for (int colIndex = 0; colIndex < tableValues.GetLength(1); colIndex++)
            {
                tableValues[0, colIndex] = columnHeaders[colIndex];
            }

            for (int rowIndex = 1; rowIndex < tableValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < tableValues.GetLength(1); colIndex++)
                {
                    object value = valueSelectors[colIndex].Invoke(values[rowIndex - 1]);

                    tableValues[rowIndex, colIndex] = value != null ? value.ToString() : "null";
                }
            }

            return ToStringTable(tableValues);
        }

        public static string ToStringTable(this string[,] tableValues)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(tableValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            var builder = new StringBuilder();
            for (int rowIndex = 0; rowIndex < tableValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < tableValues.GetLength(1); colIndex++)
                {
                    string cell = tableValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    builder.Append(" | ");
                    builder.Append(cell);
                }

                builder.Append(" | ");
                builder.AppendLine();

                if (rowIndex == 0)
                {
                    builder.Append($" |{headerSpliter}| ");
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }

        public static string ToStringTable<T>(this IEnumerable<T> values,
            params Expression<Func<T, object>>[] valueSelectors)
        {
            var headers = valueSelectors.Select(func => GetProperty(func).Name).ToArray();
            var selectors = valueSelectors.Select(exp => exp.Compile()).ToArray();
            return ToStringTable(values, headers, selectors);
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];

                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }

            return maxColumnsWidth;
        }

        private static PropertyInfo GetProperty<T>(Expression<Func<T, object>> expresstion)
        {
            if (expresstion.Body is UnaryExpression)
            {
                if ((expresstion.Body as UnaryExpression).Operand is MemberExpression)
                {
                    return ((expresstion.Body as UnaryExpression).Operand as MemberExpression).Member as PropertyInfo;
                }
            }

            if ((expresstion.Body is MemberExpression))
            {
                return (expresstion.Body as MemberExpression).Member as PropertyInfo;
            }

            return null;
        }
    }
}