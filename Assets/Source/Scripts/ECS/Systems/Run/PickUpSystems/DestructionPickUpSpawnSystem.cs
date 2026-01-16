using System;
using Data;
using ECS.Components;
using Leopotam.Ecs;
using Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ECS.Systems
{
    public class DestructionPickUpSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PickUpSpawnerTagComponent, DestructionComponent> _filter;
        private readonly float _minPercentProbability = 0f;
        private readonly float _maxPercentProbability = 101f;
        private readonly PickUpBuilder _pickUpBuilder;
        private readonly PickUpsInitConfig _config;
        private readonly float _probability;

        public DestructionPickUpSpawnSystem(EcsWorld ecsWorld, PickUpsInitConfig config)
        {
            _config = config;
            _probability = config.SpawnProbability;
            _pickUpBuilder = new PickUpBuilder(ecsWorld);
        }

        public void Run()
        {
            foreach (var index in _filter)
            {
                if (Random.Range(_minPercentProbability, _maxPercentProbability) <= _probability)
                     return;

                ref var pickUpSpawnTagComponent = ref _filter.Get1(index);

                var position = pickUpSpawnTagComponent.spawnPoint.position;
                _pickUpBuilder.Build(_config, position + _config.SpawnOffset);
            }
        }
    }
}