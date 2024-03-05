using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;
using ErikMaths;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class SlipperyIce: IBlockBehaviour 
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
        private readonly BodyComp bodyComp;

        public SlipperyIce(BodyComp bodyComp)
        {
            this.bodyComp = bodyComp ?? throw new ArgumentNullException(nameof(bodyComp));
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
            // Check if the player is on the block or not
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.SlipperyIce>();
            }

            if (IsPlayerOnBlock)
            {
                bodyComp.Velocity.X = ErikMath.MoveTowards(bodyComp.Velocity.X, 0f, PlayerValues.ICE_FRICTION / 3f);
            }

            return true;
        }
    }
}
