using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;
using Microsoft.Xna.Framework;
using HarmonyLib;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public  class Quicksand: IBlockBehaviour
    {
        public float BlockPriority => 1f;
        public bool IsPlayerOnBlock { set; get; }

        private readonly ICollisionQuery m_collisionQuery;

        public Quicksand(ICollisionQuery collisionQuery)
        {
            m_collisionQuery = collisionQuery ?? throw new ArgumentNullException(nameof(collisionQuery));
        }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info.IsCollidingWith<Blocks.Quicksand>())
            {
                return !IsPlayerOnBlock;
            }
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info.IsCollidingWith<Blocks.Quicksand>() && !IsPlayerOnBlock)
            {
                return behaviourContext.BodyComp.Velocity.Y < 0f;
            }
            return false;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            float num = ((IsPlayerOnBlock && bodyComp.Velocity.Y <= 0f) ? 0.75f : 1f);
            float result = inputYVelocity * num;
            if (!IsPlayerOnBlock && bodyComp.IsOnGround && bodyComp.Velocity.Y > 0f)
            {
                bodyComp.Position.Y += 1.5f;
            }
            return result;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            Rectangle hitbox = bodyComp.GetHitbox();
            m_collisionQuery.CheckCollision(hitbox, out Rectangle _, out AdvCollisionInfo info);
            IsPlayerOnBlock = info.IsCollidingWith<Blocks.Quicksand>();
            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Min(1.0f, bodyComp.Velocity.Y);
                Traverse.Create(bodyComp).Field("_knocked").SetValue(false);
            }
            
            return true;
        }
    }
}
