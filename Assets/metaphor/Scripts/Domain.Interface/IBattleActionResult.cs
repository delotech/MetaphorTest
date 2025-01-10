using System.Collections.Generic;

namespace metaphor.Scripts.Domain.Interface
{
    public interface IBattleActionResult
    {
        List<TurnState> Amount { get; }
        BattleActionResult Result { get; }
    }
}