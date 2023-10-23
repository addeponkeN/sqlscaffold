using EntityFrameworkCore.Scaffolding.Handlebars;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace Ponko.DBModelsGen.DesignTime
{
    public class ScaffoldingDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHandlebarsScaffolding(options =>
            {
                options.LanguageOptions = LanguageOptions.CSharp;
                options.EnableSchemaFolders = false;
            });

            serviceCollection.AddHandlebarsTransformers(
                entityTypeNameTransformer: this.ReplaceUnwantedStrings,
                entityFileNameTransformer: this.ReplaceUnwantedStrings,
                propertyTransformer: propertyInfo => new EntityPropertyInfo(this.ReplaceUnwantedStrings(propertyInfo.PropertyType), propertyInfo.PropertyName, propertyInfo.PropertyIsNullable),
                navPropertyTransformer: propertyInfo => new EntityPropertyInfo(this.ReplaceUnwantedStrings(propertyInfo.PropertyType), propertyInfo.PropertyName, propertyInfo.PropertyIsNullable),
                constructorTransformer: propertyInfo => new EntityPropertyInfo(this.ReplaceUnwantedStrings(propertyInfo.PropertyType), propertyInfo.PropertyName, propertyInfo.PropertyIsNullable)
            );
        }

        private string? ReplaceUnwantedStrings(string? s)
        {
            return s?
                .Replace("Datum", "Data")
                .Replace("datum", "data");
        }
    }
}
