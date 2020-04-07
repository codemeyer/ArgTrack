using System.Reflection;

namespace ArgTrack.Mappers
{
    public static class PropertyCopier
    {
        public static void Copy(object from, object to)
        {
            var targetType = to.GetType();
            var sourceType = from.GetType();

            PropertyInfo[] sourceProps = sourceType.GetProperties();

            foreach (var propInfo in sourceProps)
            {
                PropertyInfo toProp = targetType == sourceType
                    ? propInfo
                    : targetType.GetProperty(propInfo.Name);

                if (toProp != null && toProp.CanWrite)
                {
                    var value = propInfo.GetValue(from, null);
                    toProp.SetValue(to, value, null);
                }
            }
        }
    }
}
