﻿
using ErikMaths;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class HeavyIce: IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; } = false;

        public HeavyIce() { }

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
            return inputYVelocity;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.HeavyIce>();
            }

            BodyComp bodyComp = behaviourContext.BodyComp;

            if (IsPlayerOnBlock && bodyComp.IsOnGround && !behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<IceBlock>())
            {
                bodyComp.Velocity.X = ErikMath.MoveTowards(bodyComp.Velocity.X, 0f, PlayerValues.ICE_FRICTION);
            }

            return true;
        }

    }
}
