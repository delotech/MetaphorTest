using UnityEngine;
using metaphor.Scripts.Domain;
using metaphor.Scripts.Domain.Interface;

namespace metaphor.Scripts.Controller.Gameplay
{
    public class BattleHandlerScene : MonoBehaviour
    {
        public IBattleHandler BattleHandler { get; private set; }

        private void Start()
        { 
            // Caso a scene de batalha fosse chamada os valores seriam criados aqui
          //  BattleHandler = new BattleHandler();
        }
    }
}