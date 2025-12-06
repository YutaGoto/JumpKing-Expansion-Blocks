using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class CeilingShiftSolid : IBlockBehaviour
    {
        public float BlockPriority => 2f;

        /// <inheritdoc/>
        public bool IsPlayerOnBlock { get; set; }

        /// <inheritdoc/>
        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info.IsCollidingWith<Blocks.CeilingShiftSolid>())
            {
                return !IsPlayerOnBlock;
            }
            return false;
        }

        /// <inheritdoc/>
        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info != null && behaviourContext != null)
            {
                BodyComp bodyComp = behaviourContext.BodyComp;

                return info.IsCollidingWith<Blocks.CeilingShiftSolid>()
                       && !behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.CeilingShiftSolid>()
                       && bodyComp.Velocity.Y > 0f;
            }
            return false;
        }

        /// <inheritdoc/>
        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        /// <inheritdoc/>
        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        /// <inheritdoc/>
        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            if (IsPlayerOnBlock && inputYVelocity < 0)
            {
                return 0f;
            }
            return inputYVelocity;
        }

        /// <inheritdoc/>
        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.CeilingShiftSolid>();
            }

            return true;
        }
    }
}
