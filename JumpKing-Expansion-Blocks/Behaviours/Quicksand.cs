﻿using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.MiscEntities.WorldItems.Inventory;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.Player;
using System;
using ErikMaths;
using BehaviorTree;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public  class Quicksand: IBlockBehaviour
    {
        public float BlockPriority => 1f;
        public bool IsPlayerOnBlock
        {
            set;
            get;
        }

        private readonly ICollisionQuery m_collisionQuery;

        public Quicksand(ICollisionQuery collisionQuery)
        {
            m_collisionQuery = collisionQuery ?? throw new ArgumentNullException(nameof(collisionQuery));
        }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info.Sand)
            {
                return !IsPlayerOnBlock;
            }
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {

            if (info.Sand && !IsPlayerOnBlock)
            {
                return behaviourContext.BodyComp.Velocity.Y < 0f;
            }
            return false;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            float num = (IsPlayerOnBlock ? 0.5f : 1f);
            return inputXVelocity * num;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            float num = ((IsPlayerOnBlock && bodyComp.Velocity.Y <= 0f) ? 0.75f : 1f);
            float result = inputYVelocity * num;
            if (!IsPlayerOnBlock && bodyComp.IsOnGround && bodyComp.Velocity.Y > 0f)
            {
                bodyComp.Position.Y += 2f;
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
            IsPlayerOnBlock = info.Sand;
            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Min(1f, bodyComp.Velocity.Y);
                
            }
            return true;
        }
    }
}
