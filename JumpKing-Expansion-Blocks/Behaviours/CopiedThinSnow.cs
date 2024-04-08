using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.API;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class CopiedThinSnow: IBlockBehaviour
    {
        public float BlockPriority => 1f;
        public bool IsPlayerOnBlock { get; set;  }

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
