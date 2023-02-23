using CodeBase.Data;
using CodeBase.Enemy;
using CodeBase.Hero;
using CodeBase.Infrastructure;
using CodeBase.Static_Data.Enums;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Logic.Enemy_Spawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public MonsterTypeId MonsterTypeId;
        public string Id { get; set; }
        
        public bool _slain;
        private IGameFactory _factory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory factory) => 
            _factory = factory;


        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.KillData.ClearedSpawners.Contains(Id))
            {
                _slain = true;
            }
            else
            {
                Spawn();
            }
        }

        private async void Spawn()
        {
            GameObject monster = await _factory.CreateMonster(MonsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.Happened += Slay;
        }

        private void Slay()
        {
            if(_enemyDeath != null) _enemyDeath.Happened -= Slay;
            _slain = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_slain)
                progress.KillData.ClearedSpawners.Add(Id);
        }
    }
}
