using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class Trampoline: IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        private bool xCollisionCheck;
        private float yVelocity = 0f;
        private float xVelocity = 0f;

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if(IsPlayerOnBlock)
            {
                xCollisionCheck = true;
            }
            else
            {
                xCollisionCheck = false;
            }

            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.Trampoline>() && yVelocity >= 1.0f;
            }

            yVelocity = Math.Max(yVelocity, bodyComp.Velocity.Y);
            xVelocity = bodyComp.Velocity.X;

            if (yVelocity < 1.0f) Reset();

            if (IsPlayerOnBlock && !xCollisionCheck)
            {
                bodyComp.Velocity.Y = (yVelocity * -0.8f);
                bodyComp.Velocity.X = xVelocity;
                Reset();
            }

            if (!IsPlayerOnBlock && bodyComp.IsOnGround) Reset();

            return true;
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

        private void Reset()
        {
            yVelocity = 0f;
            xVelocity = 0f;
        }
    }
}
