namespace Seller.Shared.Services
{
    using System.Threading.Tasks;
    using Data.Models;
    public interface IDataService<in TEntity>
        where TEntity : class
    {
        Task MarkMessageAsPublished(string id);

        Task Save(TEntity entity, params Message[] messages);
    }
}
