using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class SuperLowGravity: IBlockBehaviour
    {
        public float BlockPriority => 2f;

        /// <inheritdoc/>
        public bool IsPlayerOnBlock { get; set; }

        public const float LOW_GRAV_GRAVITY_MULTIPLIER = 0.5f;
        public const float LOW_GRAV_X_MOVE_MULTIPLIER = 1.2f;
        public const float LOW_GRAV_Y_MOVE_MULTIPLIER = 1.1f;

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

            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.SuperLowGravity>();
            }

            return inputGravity * (IsPlayerOnBlock ? LOW_GRAV_GRAVITY_MULTIPLIER : 1f);
        }

        /// <inheritdoc/>
        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.SuperLowGravity>();
            }

            // Apply the modifier to the velocity
            return inputXVelocity * (IsPlayerOnBlock ? LOW_GRAV_X_MOVE_MULTIPLIER : 1f);
        }

        /// <inheritdoc/>
        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.SuperLowGravity>();
            }

            float modifier = IsPlayerOnBlock ? LOW_GRAV_Y_MOVE_MULTIPLIER : 1f;

            float newYVelocity = inputYVelocity * modifier;

            if (IsPlayerOnBlock && bodyComp.IsOnGround && bodyComp.Velocity.Y > 0)
            {
                bodyComp.Position.Y += 1;
            }

            return newYVelocity;
        }

        /// <inheritdoc/>
        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.SuperLowGravity>();
            }
            return true;
        }
    }
}
