using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class SuperCharge : IBlockBehaviour
    {
        public float BlockPriority => 2f;
        public bool IsPlayerOnBlock { get; set; }
        public int SuperChargeRatio { get; set; }
        public SuperCharge() { }

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
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.SuperCharge>();
            }

            if (IsPlayerOnBlock)
            {
                if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null && behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.SuperCharge>() != null)
                {
                    Blocks.SuperCharge superChargeBlock = (Blocks.SuperCharge)behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.SuperCharge>().FirstOrDefault();
                    SuperChargeRatio = superChargeBlock.Ratio;
                }
                else
                {
                    SuperChargeRatio = 1;
                }
            }

            return true;
        }
    }
}
