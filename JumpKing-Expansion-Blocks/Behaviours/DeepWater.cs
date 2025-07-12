using HarmonyLib;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class DeepWater: IBlockBehaviour
    {
        public float BlockPriority => 3f;

        public bool IsPlayerOnBlock { get; set; }
        public bool PrevIsPlayerOnBlock { get; set; }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            Rectangle hitbox = bodyComp.GetHitbox();

            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                PrevIsPlayerOnBlock = IsPlayerOnBlock;
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.DeepWater>();
            }

            if (PrevIsPlayerOnBlock != IsPlayerOnBlock)
            {
                Point center = hitbox.Center;

                if (!IsPlayerOnBlock && bodyComp.Velocity.Y < 0f || IsPlayerOnBlock && bodyComp.Velocity.Y >= 0f)
                {
                    center.Y += hitbox.Height / 2;
                }
                else
                {
                    center.Y -= hitbox.Height / 2;
                }

                if (!ModEntry.Tags.Contains("DisableSplashParticle"))
                {
                    var spawner = Traverse.Create(bodyComp).Field("m_splashParticleSpawner").GetValue();
                    MethodInfo createWaterSplashParticle = AccessTools.TypeByName("ISplashParticleSpawner").GetMethod("CreateWaterSplashParticle");
                    createWaterSplashParticle.Invoke(spawner, new object[] { center, IsPlayerOnBlock });
                }
            }

            return true;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity * GetDeepWaterMultiplier();
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity * GetDeepWaterMultiplier();
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity * GetDeepWaterMultiplier();
        }

        public float GetDeepWaterMultiplier()
        {
            return IsPlayerOnBlock ? 0.25f : 1f;
        }
    }
}
    