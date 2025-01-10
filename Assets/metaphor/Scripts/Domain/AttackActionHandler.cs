using System;
using System.Collections.Generic;
using metaphor.Scripts.Domain.Interface;

namespace metaphor.Scripts.Domain
{
    public class AttackActionHandler : IBattleActionResult
    {
        public List<TurnState> Amount => new() { TurnState.Full };
        public BattleActionResult Result { get; private set; }

        public IBattleActionResult ExecuteAttack(List<ICombatant> targets)
        {
            if (targets == null)
            {
                throw new InvalidOperationException("Nenhum inimigo selecionado para o ataque.");
            }

            // Aplica o dano no inimigo selecionado
            foreach (var target in targets)
            {
                target.Attack();
            }

            Result = BattleActionResult.Successful;
            return this;
        }

        
    }
}