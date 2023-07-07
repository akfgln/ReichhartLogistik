using ReichhartLogistik.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Models.Extensions
{
    public static class MappingExtensions
    {       
        private static TDestination Map<TDestination>(this object source)
        {
            return AutoMapper.Mapper.Map<TDestination>(source);
        }
        private static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapper.Mapper.Map(source, destination);
        }

        public static TModel ToModel<TModel>(this BaseEntity entity) where TModel : BaseEntityModel
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return entity.Map<TModel>();
        }

        public static TEntity ToEntity<TEntity>(this BaseEntityModel model) where TEntity : BaseEntity
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return model.Map<TEntity>();
        }
    }
}
