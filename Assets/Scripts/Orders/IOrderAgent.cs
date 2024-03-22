using Cysharp.Threading.Tasks;

namespace Orders
{
    public interface IOrderAgent
    {
        UniTask CompleteOrderAndReturn();
    }
}