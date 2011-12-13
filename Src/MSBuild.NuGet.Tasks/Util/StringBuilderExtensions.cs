using System.Text;

namespace MSBuild.NuGet.Tasks.Util
{
    public static class StringBuilderExtensions
    {
        public static void AppendPathArg(this StringBuilder sb, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                sb.AppendFormat(" -{0} \"{1}\"", name, value.TrimEnd('\\'));
        }

        public static void AppendStringArg(this StringBuilder sb, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                sb.AppendFormat(" -{0} {1}", name, value);
        }

        public static void AppendBoolArg(this StringBuilder sb, string name, bool value)
        {
            if (value)
                sb.AppendFormat(" -{0}", name);
        }
    }
}
