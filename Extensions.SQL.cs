using System;
using System.Data;
using System.Text;

namespace CSharpUtils
{
    public static partial class Extensions
    {
        public static string SelectFrom(this string fields, string from)
        {
            return "SELECT " + fields.AddSlashes() + " FROM " + from.AddSlashes();
        }

        public static string Select(this string from, string fields)
        {
            return SelectFrom(fields, from);
        }

        public static string Where(this string sql, string where)
        {
            return sql + " WHERE " + where;
        }

        /// <summary>
        /// Returns a string with backslashes before characters that need to be quoted
        /// </summary>
        /// <param name="InputTxt">Text string need to be escape with slashes</param>
        public static string AddSlashes(this string InputTxt)
        {
            // List of characters handled:
            // \000 null
            // \010 backspace
            // \011 horizontal tab
            // \012 new line
            // \015 carriage return
            // \032 substitute
            // \042 double quote
            // \047 single quote
            // \134 backslash
            // \140 grave accent

            string Result = InputTxt;

            try
            {
                Result = System.Text.RegularExpressions.Regex.Replace(InputTxt, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
            }
            catch (Exception Ex)
            {
                // handle any exception here
                Console.WriteLine(Ex.Message);
            }

            return Result;
        }

        public static string BuildInsertSQL(this DataTable table)
        {
            StringBuilder sql = new StringBuilder("INSERT INTO " + table.TableName + " (");
            StringBuilder values = new StringBuilder(" VALUES (");
            bool bFirst = true;

            foreach (DataColumn column in table.Columns)
            {
                if (bFirst)
                    bFirst = false;
                else
                {
                    sql.Append(", ");
                    values.Append(", ");
                }

                sql.Append(column.ColumnName);
                values.Append("@");
                values.Append(column.ColumnName);
            }

            sql.Append(")");
            sql.Append(values.ToString());
            sql.Append(")");

            return sql.ToString(); ;
        }
    }
}
