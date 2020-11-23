namespace Seller.Admin.Services.Identity
{
    using System.Threading.Tasks;
    using Refit;
    using Seller.Admin.Models.Identity;
    public interface IIdentityService
    {
        [Post("/Identity/Login")]
        Task<UserOutputModel> Login([Body] UserInputModel loginInput);
    }
}
