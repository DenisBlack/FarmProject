using System;
using System.Collections.Generic;

namespace Effects
{
    public class BuffSystem 
    {
        private HashSet<Buff> _buffs = new HashSet<Buff>();
    
        public Action<Buff> OnBuffStarted;
        public Action<Buff> OnBuffCompleted;

        public void AddEffect(Buff buff)
        {
            _buffs.Add(buff);
            OnBuffStarted?.Invoke(buff);
        }

        public void RemoveBuff(Buff buff)
        {
            _buffs.Remove(buff);
            OnBuffCompleted?.Invoke(buff);
        }
    }
}