using Backend_Toplearn.Model.BAL;

namespace Backend_Toplearn.Config.JWT_Service
{
    public interface IJwtService
    {
        string Generate(User.Fields user);
    }
}