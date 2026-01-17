using System.Collections.Generic;
using Data;
using ECS.Components;
using ECS.Components.Detection;
using ECS.Components.Movement;
using EntityActors;
using Leopotam.Ecs;
using UnityEngine;
using Utilitiy;

namespace Systems
{
    public class TurretBuilder : EcsBuilder
    {
        public TurretBuilder(EcsWorld world) : base(world)
        {
        }

        public TurretActor CreateTurret(TurretInitConfig turretInitConfig, Transform placeholder)
        {
            var turretActor = Object.Instantiate(
                turretInitConfig.TurretPrefab,
                placeholder.transform);

            var turret = _world.NewEntity();
            var selfTransform = turretActor.transform;

            ref var destructionComponent = ref turret.Get<DestructionComponent>();
            destructionComponent.destroyObject = turretActor.gameObject;

            ref var trackerComponent = ref turret.Get<TrackerComponent>();
            trackerComponent.searchRadius = turretInitConfig.TrackerRange;
            trackerComponent.selfTeam = Teams.Player;
            trackerComponent.selfTransform = selfTransform;

            ref var rotatableComponent = ref turret.Get<RotatableComponent>();
            rotatableComponent.transform = selfTransform;

            ref var detectionComponent = ref turret.Get<DetectionComponent>();
            detectionComponent.angle = turretInitConfig.DetectAngle;
            detectionComponent.radius = turretInitConfig.DetectRadius;

            turret.Get<FollowComponent>();

            ref var turretComponent = ref turret.Get<TurretComponent>();
            turretComponent.weapons = new List<EcsEntity>();

            var weaponBuilder = new WeaponBuilder(_world);

            foreach (var position in turretActor.WeaponPositions)
            {
                var weapon = weaponBuilder.Build(turretInitConfig.WeaponInitConfig, selfTransform,
                    position);
                turretComponent.weapons.Add(weapon);
            }

            return turretActor;
        }
    }
}