using System;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class ZeroFriction: IBlockBehaviour
    {
        public float BlockPriority => 2f;
        public bool IsPlayerOnBlock
        {
            get
            {
                return isPlayerOnBlock;
            }
            set
            {
                isPlayerOnBlock = value;
            }
        }

        private bool isPlayerOnBlock = false;

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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.ZeroFriction>();
            }

            if (IsPlayerOnBlock && bodyComp.IsOnGround)
            {
                bodyComp.Velocity.X = bodyComp.Velocity.X - 0f;
            }

            return true;
        }
    }
}
