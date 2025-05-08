using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class ForceFramesJump: IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        public int ForceFrames { get; set; } = 0;

        public ForceFramesJump()
        {
        }

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
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.ForceFramesJump>();
            }

            if (IsPlayerOnBlock)
            {
                if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null && behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.ForceFramesJump>() != null)
                {
                    Blocks.ForceFramesJump superChargeBlock = (Blocks.ForceFramesJump)behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.ForceFramesJump>().FirstOrDefault();
                    ForceFrames= superChargeBlock.Frame;
                }
                else
                {
                    ForceFrames = 0;
                }
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
