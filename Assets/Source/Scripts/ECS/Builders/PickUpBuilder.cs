using ECS.Components;
using ECS.Data;
using ECS.EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace ECS.Builders
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

            var actor = _pool.Get(spawnPoint, Quaternion.identity);
            actor.Initialize(_ => _pool.ReturnToPool(actor));
            var entity = _world.NewEntity();
            actor.GetComponent<ColliderObserver>().Initialize(_world, entity);

            ref var destructionComponent = ref entity.Get<PoolDestructionComponent>();
            destructionComponent.poolable = actor;

            ref var scorePickUpComponent = ref entity.Get<ScorePickUpComponent>();
            scorePickUpComponent.pickUpScore = initConfig.PickUpScoreValue;

            entity.Get<PickUpTagComponent>();
        }
    }
}