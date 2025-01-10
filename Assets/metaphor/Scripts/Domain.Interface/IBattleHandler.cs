using System.Collections.Generic;

namespace metaphor.Scripts.Domain.Interface
{
    public interface IBattleHandler
    {
        public List<IPlayerSlotOnBattle<ICharacter>> Team { get; }
        public List<IPlayerSlotOnBattle<IEnemy>> Enemies { get; }
        
        void InitBattle(List<IPlayerSlotOnBattle<ICharacter>> characters, List<IPlayerSlotOnBattle<IEnemy>> enemies);
        void SpawnCharacter();
        
        // turnos controller
        bool IsPlayerTurn { get; }
        int CurrentPlayerIndex { get; }
        List<TurnState> TurnStates { get; }
        bool HaveTurnsToPlay { get; }
        void PlayNewTurn();
        void PlayNextTurn();
        void CheckNextTurn();
        void ResetTurnStates();

        void ExpendTurn(List<TurnState> amount);
        bool HaveTurnPoints(List<TurnState> amount);
        
        // Actions controller
        BattleAction BattleAction { get; }
        void SelectAction(BattleAction battleAction);
        IBattleActionResult PlayAction();
        void ResetPlayAction();
        
        // Battle
        void SelectTargets(List<ICombatant> combatant);
        List<ICombatant> TargetsSelected { get; }
        ICombatant CurrentPlayer { get; }

        ICombatant GetRandomTarget(bool getEnemy = false);
    }

    public enum BattleActionResult
    {
        Canceled = 0,
        Failed = 1,
        Successful = 2
    }
    public enum TurnState
    {
        None = 0,
        Half = 1, // Meio turno
        Full = 2, // Turno completo
    }
    
    public enum BattleAction
    {
        Idle,
        Attack,
        Defense,
    }
}

