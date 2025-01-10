using metaphor.Scripts.Domain.Interface;

namespace metaphor.Scripts.Domain
{
    public class PlayerSlotOnBattle<T> : IPlayerSlotOnBattle<T>
    {
        public int SlotIndex { get; private set; }
        public T Entity { get; private set; }

        public PlayerSlotOnBattle(int slotIndex, T entity)
        {
            SlotIndex = slotIndex;
            Entity = entity;
        }
    }
}