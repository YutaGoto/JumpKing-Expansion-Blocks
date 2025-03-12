using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class JkqPlatform : IBlockBehaviour
    {

        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        internal static bool isThroughPlatform { get; set; } = false;
        internal static float jumpYPosition { get; set; }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info != null && behaviourContext != null)
            {
                BodyComp bodyComp = behaviourContext.BodyComp;

                return info.IsCollidingWith<Blocks.JkqPlatform>()
                    && !behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>()
                    && !isThroughPlatform
                    && bodyComp.Velocity.Y >= -0.01f;
            }

            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>();
            }

            if (bodyComp.IsOnGround)
            {
                jumpYPosition = bodyComp.Position.Y;
                isThroughPlatform = false;
            }

            if (bodyComp.Position.Y < jumpYPosition && bodyComp.Velocity.Y <= 0.0f)
            {
                isThroughPlatform = isThroughPlatform || IsPlayerOnBlock;
            }
            

            return true;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity;
        }
    }
}
