using System.Collections;
using System.Reflection;
using System.Text;

namespace MedPedia.Common.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static string ObjectToQueryString<T>(this T obj) where T : class
        {
            StringBuilder sb = new StringBuilder();

            IEnumerable data = obj as IEnumerable ?? new[] { obj };

            foreach (var datum in data)
            {
                Type t = datum.GetType();
                var properties = t.GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    if (p.CanRead)
                    {
                        var indexes = p.GetIndexParameters();
                        if (indexes.Count() > 0)
                        {
                            var pp = p.GetValue(datum, new object[] { 1 });
                            sb.Append(ObjectToQueryString(pp));
                        }
                        else if (typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType != typeof(string))
                        {
                            sb.Append(ObjectToQueryString(p.GetValue(datum)));
                        }
                        else
                        {

                            //I dont think this is a good way to do it
                            if (p.PropertyType.FullName != p.GetValue(datum, null).ToString())
                            {
                                //sb.Append(String.Format("{0}={1}&", p.Name, HttpUtility.UrlEncode(p.GetValue(datum, null).ToString())));
                                sb.Append(String.Format("{0}={1}&", p.Name, p.GetValue(datum, null).ToString()));
                            }
                            else
                            {
                                sb.Append(ObjectToQueryString(p.GetValue(datum, null)));
                            }
                        }
                    }
                }
            }
            return sb.ToString().TrimEnd('&');
        }
    }
}
