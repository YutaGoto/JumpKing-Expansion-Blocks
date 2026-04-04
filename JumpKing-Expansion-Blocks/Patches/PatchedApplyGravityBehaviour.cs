using EntityComponent;
using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace JumpKing_Expansion_Blocks.Patches
{
    [HarmonyPatch(typeof(ApplyGravityBehaviour))]
    internal class PatchedApplyGravityBehaviour
    {
        public static bool isOnMoreFallSpeedBlock { get; set; }

        private static readonly FieldInfo blockBehavioursField = AccessTools.Field(typeof(ApplyGravityBehaviour), "m_blockBehaviours");

        public PatchedApplyGravityBehaviour(Harmony harmony)
        {
            harmony.Patch(
                AccessTools.Method(typeof(ApplyGravityBehaviour), "ExecuteBehaviour"),
                new HarmonyMethod(AccessTools.Method(GetType(), nameof(ExecuteBehaviourPrefix))),
                null
            );
        }

        private static bool ExecuteBehaviourPrefix(ApplyGravityBehaviour __instance, BehaviourContext behaviourContext, ref bool __result)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            ICollisionQuery collisionQuery = LevelManager.Instance;
            Rectangle hitbox = player.m_body.GetHitbox();
            collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);

            if (player == null)
            {
                return true;
            }

            
            LinkedList<IBlockBehaviour> blockBehaviours = (LinkedList<IBlockBehaviour>)blockBehavioursField.GetValue(__instance);

            foreach (IBlockBehaviour blockBehaviour in blockBehaviours)
            {
                if(blockBehaviour.GetType() == typeof(Behaviours.MoreFallSpeed))
                {
                    isOnMoreFallSpeedBlock = blockBehaviour.IsPlayerOnBlock;
                    break;
                }
                else
                {
                    isOnMoreFallSpeedBlock = false;
                }
            }


            if (!isOnMoreFallSpeedBlock)
            {
                return true;
            }

            float gravity = PlayerValues.GRAVITY;

            player.m_body.Velocity.Y += gravity;
            player.m_body.Velocity.Y = Math.Min(player.m_body.Velocity.Y, PlayerValues.MAX_FALL * 2f);
            __result = true;
            return false;
        }
    }
}
