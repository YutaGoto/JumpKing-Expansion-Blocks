﻿using ErikMaths;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class CursedIce: IBlockBehaviour
    {
        public float BlockPriority => 2f;
        public bool IsPlayerOnBlock { get; set; }
        private bool IsCursed = false;

        public CursedIce()
        {
        }

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
            BodyComp bodyComp = behaviourContext.BodyComp;

            IsCursed = false;
            if (IsPlayerOnBlock)
            {
                if (bodyComp.IsOnBlock<Blocks.CursedIce>() && bodyComp.Velocity.Y < 0.0f) IsCursed = true;

                if (bodyComp.IsOnBlock<Blocks.CursedIce>() && IsCursed) bodyComp.Velocity.X *= -1;
            }

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
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.CursedIce>();
            }

            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.X = ErikMath.MoveTowards(bodyComp.Velocity.X, 0f, PlayerValues.ICE_FRICTION);
            }

            return true;
        }
    }
}
