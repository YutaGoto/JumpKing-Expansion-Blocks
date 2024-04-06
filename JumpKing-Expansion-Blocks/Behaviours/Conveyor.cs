﻿using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class Conveyor: IBlockBehaviour
    {
        public float BlockPriority => 1f;
        private bool m_isPlayerOnBlock;

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

            if (IsPlayerOnBlock && bodyComp.IsOnGround)
            {
                if (behaviourContext.LastFrameCollisionInfo?.PreResolutionCollisionInfo != null)
                {
                    Blocks.Conveyor conveyor = behaviourContext.LastFrameCollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.Conveyor>().FirstOrDefault() as Blocks.Conveyor;

                    if (conveyor.Direction == 30) // LEFT
                    {
                        return inputXVelocity + (float)conveyor.Speed * -0.2f;
                    }
                    else // RIGHT
                    {
                        return inputXVelocity + (float)conveyor.Speed * 0.2f;
                    }
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
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.Conveyor>();
            }

            //BodyComp bodyComp = behaviourContext.BodyComp;

            //if (IsPlayerOnBlock && bodyComp.IsOnGround)
            //{
            //    if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            //    {
            //        Blocks.Conveyor conveyor = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.Conveyor>().FirstOrDefault() as Blocks.Conveyor;

            //        // TODO: Wall and slope Collision check

            //        if (conveyor.Direction == 30) // LEFT
            //        {
            //            bodyComp.Position.X += (float)conveyor.Speed * -0.2f;
            //        }
            //        else // RIGHT
            //        {
            //            bodyComp.Position.X += (float)conveyor.Speed * 0.2f;
            //        }
            //    }
            //}
            return true;
        }
    }
}
