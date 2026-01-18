using Data;
using ECS.Components;
using ECS.MonoBehaviours;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace Systems
{
    public class PickUpBuilder : EcsBuilder
    {
        private ObjectPool<PickUpActor> _pool;

        public PickUpBuilder(EcsWorld world) : base(world)
        {
        }

        public void Build(PickUpsInitConfig initConfig, Vector3 spawnPoint)
        {
            if (_pool == null)
                _pool = new ObjectPool<PickUpActor>(initConfig.PickUpActor);

            var pickUpActor = _pool.Get(spawnPoint, Quaternion.identity);
            pickUpActor.Initialize(_ => _pool.ReturnToPool(pickUpActor));
            var pickUp = _world.NewEntity();
            pickUpActor.GetComponent<ColliderObserver>().Initialize(_world, pickUp);

            ref var destructionComponent = ref pickUp.Get<PoolDestructionComponent>();
            destructionComponent.poolable = pickUpActor;

            ref var scorePickUpComponent = ref pickUp.Get<ScorePickUpComponent>();
            scorePickUpComponent.pickUpScore = initConfig.PickUpScoreValue;

            pickUp.Get<PickUpTagComponent>();
        }
    }
}