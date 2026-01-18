using System.Collections.Generic;
using ECS.Components;
using ECS.Data;
using ECS.EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace ECS.Builders
{
    public class TurretBuilder : EcsBuilder
    {
        public TurretBuilder(EcsWorld world) : base(world)
        {
        }

        public TurretActor CreateTurret(TurretInitConfig turretInitConfig, Transform placeholder)
        {
            var actor = Object.Instantiate(
                turretInitConfig.TurretPrefab,
                placeholder.transform);

            var entity = _world.NewEntity();
            var selfTransform = actor.transform;

            ref var destructionComponent = ref entity.Get<DestructionComponent>();
            destructionComponent.destroyObject = actor.gameObject;

            ref var trackerComponent = ref entity.Get<TrackerComponent>();
            trackerComponent.searchRadius = turretInitConfig.TrackerRange;
            trackerComponent.selfTeam = Teams.Player;
            trackerComponent.selfTransform = selfTransform;

            ref var rotatableComponent = ref entity.Get<RotatableComponent>();
            rotatableComponent.transform = selfTransform;

            ref var detectionComponent = ref entity.Get<DetectionComponent>();
            detectionComponent.angle = turretInitConfig.DetectAngle;
            detectionComponent.radius = turretInitConfig.DetectRadius;

            entity.Get<FollowComponent>();

            ref var turretComponent = ref entity.Get<TurretComponent>();
            turretComponent.weapons = new List<EcsEntity>();

            var weaponBuilder = new WeaponBuilder(_world);

            foreach (var position in actor.WeaponPositions)
            {
                var weapon = weaponBuilder.Build(turretInitConfig.WeaponInitConfig, selfTransform,
                    position);
                turretComponent.weapons.Add(weapon);
            }

            return actor;
        }
    }
}