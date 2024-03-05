using JumpKing;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.MiscEntities.WorldItems.Inventory;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.Player;
using System;
using ErikMaths;
using BehaviorTree;
using System.Diagnostics;
using JumpKing.API;
using JumpKing_Expansion_Blocks.Blocks;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class CopiedThinSnow: IBlockBehaviour
    {
        public float BlockPriority => 1f;
        public bool IsPlayerOnBlock
        {
            get
            {
                return m_isPlayerOnBlock;
            }
            set
            {
                m_isPlayerOnBlock = value;
            }
        }

        private bool m_isPlayerOnBlock;
        private readonly PlayerEntity player;

        public CopiedThinSnow(PlayerEntity player)
        {
            this.player = player ?? throw new ArgumentNullException(nameof(player));
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
            if (behaviourContext?.LastFrameCollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.LastFrameCollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.CopiedThinSnow>();
                if (IsPlayerOnBlock)
                {
                    return 0f;
                }
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
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.CopiedThinSnow>();
            }
            return true;
        }
    }
}
