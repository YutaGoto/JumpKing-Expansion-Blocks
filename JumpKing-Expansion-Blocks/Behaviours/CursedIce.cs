﻿using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using ErikMaths;
using JumpKing.MiscEntities.WorldItems.Inventory;
using JumpKing.MiscEntities.WorldItems;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class CursedIce: IBlockBehaviour
    {
        public float BlockPriority => 2f;
        public bool IsPlayerOnBlock
        {
            get
            {
                if (InventoryManager.HasItemEnabled(Items.SnakeRing))
                {
                    return m_bodyComp.IsOnGround;
                }
                return isPlayerOnBlock;
            }
            set
            {
                isPlayerOnBlock = value;
            }
        }

        private bool isPlayerOnBlock = false;
        private readonly BodyComp m_bodyComp;

        public CursedIce(BodyComp bodyComp)
        {
            m_bodyComp = bodyComp ?? throw new ArgumentNullException("bodyComp");
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
