using HarmonyLib;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class SideSand : IBlockBehaviour
    {
        private readonly ICollisionQuery m_collisionQuery;

        /// <inheritdoc />
        public float BlockPriority => 1f;

        /// <inheritdoc />
        public bool IsPlayerOnBlock { get; set; }

        public SideSand(ICollisionQuery collisionQuery) => m_collisionQuery = collisionQuery ?? throw new ArgumentNullException("collisionQuery");

        /// <inheritdoc />
        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            float num = (IsPlayerOnBlock ? 0.25f : 1f);
            return inputXVelocity * num;
        }

        /// <inheritdoc />
        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            float num = ((IsPlayerOnBlock && bodyComp.Velocity.Y <= 0f) ? 0.5f : 1f);
            float result = inputYVelocity * num;
            if (!IsPlayerOnBlock && bodyComp.IsOnGround && bodyComp.Velocity.Y > 0f)
            {
                bodyComp.Position.Y += 1f;
            }
            return result;
        }

        /// <inheritdoc />
        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        /// <inheritdoc />
        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        /// <inheritdoc />
        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            Rectangle hitbox = bodyComp.GetHitbox();
            m_collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            IsPlayerOnBlock = info.IsCollidingWith<Blocks.SideSand>();
            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Min(0.75f, bodyComp.Velocity.Y);
                Traverse.Create(bodyComp).Field("_knocked").SetValue(false);
            }
            return true;
        }

        /// <inheritdoc />
        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

    }
}
