using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class RainGravity: IBlockBehaviour
    {
        public float BlockPriority => 2f;
        public const float HIGH_GRAV_Y_MOVE_MULTIPLIER = 1.5f;
        public const float HIGH_GRAV_X_MOVE_MULTIPLIER = 0.9f;

        public bool IsPlayerOnBlock { get; set; }

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
            return inputXVelocity * (IsPlayerOnBlock ? HIGH_GRAV_X_MOVE_MULTIPLIER : 1f);
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            // JumpState jumpState;
            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.RainGravity>();
            }

            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Max(bodyComp.Velocity.Y, -7f);
            }
            if (bodyComp.Velocity.Y > 0 && IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Min(bodyComp.Velocity.Y + 0.15f, 10);
            }

            return inputYVelocity;
        }


        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            // if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            // {
            //     IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.HighGravity>();
            // }
            return true;
        }
    }
}
