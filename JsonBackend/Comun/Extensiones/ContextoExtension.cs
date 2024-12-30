using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Extensiones
{
    public static class ContextoExtension
    {
        public static TProperty GetValue<TProperty>(this object entity, string propertyName)
        {
            try
            {
                propertyName = propertyName.Trim('{', '}');
                object value = entity.GetType().GetProperty(propertyName).GetValue(entity);
                return (TProperty)value;
            }
            catch (Exception)
            {
                return default(TProperty);
            }
        }
    }
}
