using Data;
using ECS.Components;
using ECS.MonoBehaviours;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Systems
{
    public class PickUpBuilder : EcsBuilder
    {
        public PickUpBuilder(EcsWorld world) : base(world)
        {
        }

        public void Build(PickUpsInitConfig initConfig, Vector3 spawnPoint)
        {
            var pickUpActor =
                Object.Instantiate(initConfig.PickUpActor, spawnPoint, Quaternion.identity);
            var pickUp = _world.NewEntity();
            pickUpActor.GetComponent<ColliderObserver>().Initialize(_world, pickUp);

            ref var destructionComponent = ref pickUp.Get<DestructionComponent>();
            destructionComponent.destroyObject = pickUpActor.gameObject;

            ref var scorePickUpComponent = ref pickUp.Get<ScorePickUpComponent>();
            scorePickUpComponent.pickUpScore = initConfig.PickUpScoreValue;

            pickUp.Get<PickUpTagComponent>();
        }
    }
}