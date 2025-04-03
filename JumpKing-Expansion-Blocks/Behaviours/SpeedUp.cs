using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class SpeedUp : IBlockBehaviour
    {
        public float BlockPriority => 3f;

        public bool IsPlayerOnBlock { get; set; }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {

            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.SpeedUp>();
            }

            return true;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity * GetAccelerateMultiplier();
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity * GetAccelerateMultiplier();
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity * GetAccelerateMultiplier();
        }

        public float GetAccelerateMultiplier()
        {
            return IsPlayerOnBlock ? 2f : 1f;
        }
    }
}
