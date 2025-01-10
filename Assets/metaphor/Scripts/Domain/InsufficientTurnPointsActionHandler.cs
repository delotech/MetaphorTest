using System.Collections.Generic;
using metaphor.Scripts.Domain.Interface;

namespace metaphor.Scripts.Domain
{
    public class InsufficientTurnPointsActionHandler : IBattleActionResult
    {
        public List<TurnState> Amount => new() { TurnState.None };
        public BattleActionResult Result => BattleActionResult.Canceled;
    }
}