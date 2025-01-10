using System;
using System.Collections.Generic;
using metaphor.Scripts.Domain.Interface;
using System.Linq;

namespace metaphor.Scripts.Domain
{
    public class BattleHandler : IBattleHandler
    {
        public List<IPlayerSlotOnBattle<ICharacter>> Team { get; private set;  }
        public List<IPlayerSlotOnBattle<IEnemy>> Enemies  { get; private set;  }
        public bool IsPlayerTurn { get; private set;  }
        
        // Actions
        public BattleAction BattleAction { get; private set;  }
        
        // Turn
        public int CurrentPlayerIndex  { get; private set;  }
        public List<TurnState> TurnStates { get; set; }
        public bool HaveTurnsToPlay => TurnStates.Count > 0;
        
        // Battle
        public List<ICombatant> TargetsSelected { get; private set; }
        public ICombatant CurrentPlayer { get;  private set; }
      

        public BattleHandler()
        {
            Team = new List<IPlayerSlotOnBattle<ICharacter>>();
            Enemies = new List<IPlayerSlotOnBattle<IEnemy>>();
            TurnStates = new List<TurnState>();
            TargetsSelected = new List<ICombatant>();
            CurrentPlayerIndex = 0;
            IsPlayerTurn = true;
            BattleAction = BattleAction.Idle;
        }

      
        public void InitBattle(List<IPlayerSlotOnBattle<ICharacter>> team, List<IPlayerSlotOnBattle<IEnemy>> enemies)
        {
            Team = team;
            Enemies = enemies;
            
            // TODO
             SpawnCharacter();
            
             PlayNewTurn();
        }

        public void SpawnCharacter()
        {
            // Lógica para gerar ou adicionar um novo personagem ao time
        }

   
    // Turns
    public void ResetTurnStates()
        {
            TurnStates.Clear();

            if (IsPlayerTurn)
            {
                foreach (var _ in Team)
                {
                    TurnStates.Add(TurnState.Full);
                }
            }
            else
            {
                foreach (var _ in Enemies)
                {
                    TurnStates.Add(TurnState.Full);
                }
            }
        }

        public void PlayNewTurn()
        {
            CurrentPlayerIndex = 0;
            BattleAction = BattleAction.Idle;
            
            ResetTurnStates();
            PlayNextTurn();
        }

        public void CheckNextTurn()
        {
            ResetPlayAction();

            if (HaveTurnsToPlay)
            {
                PlayNextTurn();
            }
            else
            {
                // Alterna o time
                IsPlayerTurn = !IsPlayerTurn;

                ResetTurnStates();
                PlayNextTurn();
            }
        }


        public void PlayNextTurn()
        {
            if (IsPlayerTurn)
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Team.Count;
                CurrentPlayer = Team[CurrentPlayerIndex].Entity;
            }
            else
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Enemies.Count;
                CurrentPlayer = Enemies[CurrentPlayerIndex].Entity;
            }
        }

        public void ExpendTurn(List<TurnState> amount)
        {
            if (!HaveTurnPoints(amount) || amount.Contains(TurnState.None))
            {
                return; // Se não estiverem disponíveis, não faz nada
            }

            var fullTurnsRequested = amount.Count(state => state == TurnState.Full);
            var halfTurnsRequested = amount.Count(state => state == TurnState.Half);
            
            for (var i = 0; i < fullTurnsRequested; i++)
            {
                TurnStates.Remove(TurnState.Full); // Remove 1 turno completo
            }
            for (var i = 0; i < halfTurnsRequested; i++)
            {
                TurnStates.Remove(TurnState.Half); // Remove 1 meio turno
            }
        }
        
        public bool HaveTurnPoints(List<TurnState> amount)
        {
            if (amount == null || amount.Count == 0)
            {
                return false;
            }

            var fullTurnsAvailable = TurnStates.Count(state => state == TurnState.Full);
            var halfTurnsAvailable = TurnStates.Count(state => state == TurnState.Half);

            var fullTurnsRequested = amount.Count(state => state == TurnState.Full);
            var halfTurnsRequested = amount.Count(state => state == TurnState.Half);

            // Verifica se a quantidade de turnos completos solicitados é maior que os turnos disponíveis
            if (fullTurnsRequested > fullTurnsAvailable)
            {
                return false;
            }

            // Verifica se a quantidade de meios turnos solicitados é maior que os disponíveis
            if (halfTurnsRequested > halfTurnsAvailable)
            {
                return false;
            }

            return true;
        }

        // Actions
        public void SelectAction(BattleAction battleAction)
        {
            BattleAction = battleAction;
        }

        public IBattleActionResult PlayAction()
        {
            IBattleActionResult battleActionResult; // Variável para armazenar o resultado genérico

            switch (BattleAction)
            {
                case BattleAction.Idle:
                    battleActionResult = new IdleActionHandler();
                    break;
            
                case BattleAction.Attack:
                    var attackAction = new AttackActionHandler();
                    
                    if (HaveTurnPoints(attackAction.Amount))
                    {
                        battleActionResult = attackAction.ExecuteAttack(TargetsSelected);
                    }
                    else
                    {
                        battleActionResult = new InsufficientTurnPointsActionHandler();
                    }
                    break;
            
                case BattleAction.Defense:
                    var defenseAction = new DefenseActionHandler();
                    
                    battleActionResult = HaveTurnPoints(defenseAction.Amount) ?
                        defenseAction.ExecuteDefense(CurrentPlayer) : new InsufficientTurnPointsActionHandler();
                    break;
            
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            // Consome os turnos necessários
            ExpendTurn(battleActionResult.Amount);
      
            return battleActionResult;
        }

        public void ResetPlayAction()
        {
            BattleAction = BattleAction.Idle;
            TargetsSelected = new List<ICombatant>();
        }

        public void SelectTargets(List<ICombatant> targets)
        {
            TargetsSelected = targets;
        }
        
        public ICombatant GetRandomTarget(bool getEnemy = false)
        {
            if (getEnemy)
            {
                return Enemies[0].Entity;
            }
            
            return Team[0].Entity;
            
        }
    }
}