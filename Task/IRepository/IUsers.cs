using Task.DTO;

namespace Task.IRepository
{
    public interface IUsers
    {
        Task<DataResponse> Apply(UserDTO users);
    }
}
