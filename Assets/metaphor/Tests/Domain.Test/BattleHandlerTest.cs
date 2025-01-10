using System.Collections.Generic;
using metaphor.Scripts.Domain;
using metaphor.Scripts.Domain.Interface;
using NUnit.Framework;

namespace warrealms.Tests.Domain.Test
{
    public class BattleHandlerTest
    {
        private static IBattleHandler BattleHandler { get; set; }
        
      private static IPlayerSlotOnBattle<ICharacter> PlayerSlotOnBattle01 => new PlayerSlotOnBattle<ICharacter>(0,Character01);
      private static IPlayerSlotOnBattle<ICharacter> PlayerSlotOnBattle02 => new PlayerSlotOnBattle<ICharacter>(1,Character02);

       private static IPlayerSlotOnBattle<IEnemy> EnemiesSlotOnBattle01 => new PlayerSlotOnBattle<IEnemy>(0, Enemy01);
       private static ICharacter Character01 => new Character
       (
           1,
           "Marcelo",
           6,
           10,
           5,
           100,
           100,
           5,
           10
       );
       
       private static ICharacter Character02 => new Character
       (
           2,
           "Henrique",
           3,
           7,
           3,
           150,
           150,
          10,
           1
       );
        
        private static IEnemy Enemy01 => new Enemy
        (
            1,
            "Skeleton",
            1,
            50,
            50,
            5,
            10
        );

        private static List<IPlayerSlotOnBattle<ICharacter>> CharactersBattleHandlers =>
            new()
            {
                PlayerSlotOnBattle01,
                PlayerSlotOnBattle02
            };

        private static List<IPlayerSlotOnBattle<IEnemy>> EnemiesBattleHandlers =>
            new()
            {
                EnemiesSlotOnBattle01
            };
        
        #region Setup

        [SetUp]
        public void Init()
        {
            BattleHandler = GetBattleHandlerTest();
        }

        private IBattleHandler GetBattleHandlerTest()
        {
            var battleHandlerTest = new BattleHandler();

            battleHandlerTest.InitBattle(CharactersBattleHandlers, EnemiesBattleHandlers);

            return battleHandlerTest;
        }


        #endregion

        #region Turn Validations 


        [Test]
        public void CheckStamina()
        {
            Assert.True(BattleHandler.IsPlayerTurn, 
                $"Expected BattleHandler.IsPlayerTurn to be True, but found {BattleHandler.IsPlayerTurn}.");
        
            Assert.True(BattleHandler.TurnStates.Count == CharactersBattleHandlers.Count);
        }

        #endregion
        
        #region Turn Battle


        [Test]
        public void TestFullGamePlay()
        {
           //Turno do Jogador 1
           var enemies = new List<ICombatant>();
       

           var enemy = BattleHandler.GetRandomTarget(true);
           enemies.Add(enemy);
           
           Assert.True(enemy.HealthPoints == 50, 
               $"Expected HealthPoints to be 50, but found {enemy.HealthPoints}.");
           
           BattleHandler.SelectAction(BattleAction.Attack);
           BattleHandler.SelectTargets(enemies);
           
           var actionResult01 = BattleHandler.PlayAction();

           Assert.True(actionResult01.Result == BattleActionResult.Successful, 
               $"Expected actionResult.Result to be True, but found {actionResult01.Result}.");
           
           Assert.True(enemy.HealthPoints == 40, 
               $"Expected HealthPoints to be 40, but found {enemy.HealthPoints}.");
           
           // Turno do jogador 2
           BattleHandler.SelectAction(BattleAction.Attack);
           BattleHandler.SelectTargets(enemies);
           
           var actionResult02 = BattleHandler.PlayAction();

           Assert.True(actionResult02.Result == BattleActionResult.Successful, 
               $"Expected actionResult.Result to be True, but found {actionResult02.Result}.");
           
           Assert.True(enemy.HealthPoints == 30, 
               $"Expected HealthPoints to be 30, but found {enemy.HealthPoints}.");
           
           
           // Turno do inimigo
           if (actionResult02.Result == BattleActionResult.Successful)
           {
               BattleHandler.CheckNextTurn();
           }
           
           Assert.False(BattleHandler.IsPlayerTurn, 
               $"Expected BattleHandler.IsPlayerTurn to be False, but found {BattleHandler.IsPlayerTurn}.");
           
           Assert.True(BattleHandler.TurnStates.Count == 1, 
               $"Expected BattleHandler.TurnStates.Count to be 1, but found {BattleHandler.TurnStates.Count}.");
           
           var enemyTargets = new List<ICombatant>();
           var enemyTarget = BattleHandler.GetRandomTarget(); // por enquanto so pega o [0]
           enemyTargets.Add(enemyTarget);
           
           Assert.True(enemyTarget.HealthPoints == 100, 
               $"Expected HealthPoints to be 100, but found {enemyTarget.HealthPoints}.");
           
           BattleHandler.SelectAction(BattleAction.Attack);
           BattleHandler.SelectTargets(enemyTargets);
           
           var actionResult03 = BattleHandler.PlayAction();

           Assert.True(actionResult03.Result == BattleActionResult.Successful, 
               $"Expected actionResult.Result to be True, but found {actionResult03.Result}.");
           
           Assert.True(enemyTarget.HealthPoints == 85, 
               $"Expected HealthPoints to be 85, but found {enemyTarget.HealthPoints}.");
           
        }
        
  
        
        

        #endregion
    }

}