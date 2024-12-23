using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class InfinityJump: IBlockBehaviour
    {
        private readonly ICollisionQuery m_collisionQuery;
        public float BlockPriority => 2f;
        public bool IsPlayerOnBlock { get; set; }

        public InfinityJump(ICollisionQuery collisionQuery) => m_collisionQuery = collisionQuery ?? throw new System.ArgumentNullException("collisionQuery");

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            if (IsPlayerOnBlock && bodyComp.Velocity.Y > 0f)
            {
                return Math.Min(inputYVelocity * 0.4f, PlayerValues.MAX_FALL - 1f);
            }

            return inputYVelocity;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (IsPlayerOnBlock && bodyComp.Velocity.Y > PlayerValues.MAX_FALL - 1f)
            {
                return 0f;
            }
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            Rectangle hitbox = bodyComp.GetHitbox();
            m_collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            IsPlayerOnBlock = info.IsCollidingWith<Blocks.InfinityJump>();
            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.X -= 0f;
                Traverse.Create(bodyComp).Field("_knocked").SetValue(false);
            }
            return true;
        }
    }
}
