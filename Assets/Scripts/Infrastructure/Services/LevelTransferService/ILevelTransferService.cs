
namespace Infrastructure.Services.LevelTransferService
{
    public interface ILevelTransferService : IService
    {
        void Transfer(string levelToTransfer);
    }
}