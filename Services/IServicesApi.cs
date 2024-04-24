using pruebaConecta.Models;

namespace pruebaConecta.Services
{
    public interface IServicesApi
    {
        Task<List<Property>> GetAllPropertiesByPage(int pageNumber);
        Task<ApiResult> GetAllResult(PropertySearch propertySearch);
        Task<PropertyDetails> GetPropertyById(int idProperty);

        Task<List<Region>> getAllRegions();
        Task<List<Comuna>> getAllComuna();
        Task<List<PropertyType>> getAllPropertyType();
    }
}
