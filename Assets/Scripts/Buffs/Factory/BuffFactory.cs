using Effects;
using Zenject;

namespace Buffs
{
    public class BuffFactory
    {
        [Inject] private DiContainer _diContainer;
        
        public Buff CreateBuff(BuffData data)
        {
            var buff = _diContainer.Instantiate<Buff>();
            buff.Initialized(data);
            return buff;
        }
    }
}