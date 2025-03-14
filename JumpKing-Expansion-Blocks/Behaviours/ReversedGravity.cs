﻿using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class ReversedGravity: IBlockBehaviour
    {
        public float BlockPriority => 2f;

        /// <inheritdoc/>
        public bool IsPlayerOnBlock { get; set; }

        /// <inheritdoc/>
        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        /// <inheritdoc/>
        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        /// <inheritdoc/>
        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            return IsPlayerOnBlock && !bodyComp.IsOnGround && Math.Abs(bodyComp.Velocity.X) > 0.1f ? -inputGravity : inputGravity;
        }

        /// <inheritdoc/>
        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        /// <inheritdoc/>
        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity;
        }

        /// <inheritdoc/>
        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.ReversedGravity>();
            }
            return true;
        }
    }
}
