using System.Collections.Generic;
using metaphor.Scripts.Domain.Interface;

namespace metaphor.Scripts.Domain
{
    public class DefenseActionHandler : IBattleActionResult
    {
        public List<TurnState> Amount => new() { TurnState.Half };
        public BattleActionResult Result { get; private set; }

        public IBattleActionResult ExecuteDefense(ICombatant character)
        {
            character.Defend();
            
            Result = BattleActionResult.Successful;
            
            return this;
        }
    }
}