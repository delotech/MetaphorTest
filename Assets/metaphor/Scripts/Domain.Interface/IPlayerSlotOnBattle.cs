namespace metaphor.Scripts.Domain.Interface
{
    public interface  IPlayerSlotOnBattle<T>
    {
        int SlotIndex { get; }
        
        public T Entity { get; }
    }
}