using ECS.Builders;
using ECS.Components;
using ECS.Data;
using Leopotam.Ecs;
using Random = UnityEngine.Random;

namespace ECS.Systems
{
    public class DestructionPickUpSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PickUpSpawnerTagComponent, DestructionEventComponent> _filter;
        private const float MinPercentProbability = 0f;
        private const float MaxPercentProbability = 100f;
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
                if (Random.Range(MinPercentProbability, MaxPercentProbability) > _probability)
                    continue;

                ref var pickUpSpawnTagComponent = ref _filter.Get1(index);

                var position = pickUpSpawnTagComponent.spawnPoint.position;
                _pickUpBuilder.Build(_config, position + _config.SpawnOffset);
            }
        }
    }
}