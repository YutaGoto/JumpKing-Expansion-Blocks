﻿using EntityComponent;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Patches
{
    internal class BodyComp
    {
        public static void GetMultipliersPostfix(ref float __result)
        {
            ICollisionQuery collisionQuery = LevelManager.Instance;
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            Rectangle hitbox = player.m_body.GetHitbox();
            collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            if (info.IsCollidingWith<Blocks.DeepWater>())
            {
                __result *= 0.25f;
            }
            else if (info.IsCollidingWith<Blocks.RainGravity>())
            {
                __result *= 1.28f;
            }
            else if (info.IsCollidingWith<Blocks.Accelerate>())
            {
                __result *= 2f;
            }
        }
    }
}
