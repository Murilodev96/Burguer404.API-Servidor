using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Burguer404.Domain.Utils
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?
                        .GetName() ?? value.ToString();
        }

        public static string GetDisplayNamePorValorEnum<TEnum>(int value) where TEnum : Enum
        {
            var enumValue = (TEnum)Enum.ToObject(typeof(TEnum), value);
            var member = typeof(TEnum).GetMember(enumValue.ToString()).FirstOrDefault();

            if (member != null)
            {
                var displayAttr = member.GetCustomAttribute<DisplayAttribute>();
                if (displayAttr != null)
                    return displayAttr.Name ?? string.Empty;
            }

            return enumValue.ToString() ?? string.Empty; 
        }
    }
}
