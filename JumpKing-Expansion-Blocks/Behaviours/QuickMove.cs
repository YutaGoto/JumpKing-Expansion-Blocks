using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class QuickMove: IBlockBehaviour
    {
        public float BlockPriority => 3f;

        public bool IsPlayerOnBlock { get; set; }
        private float ratio = 1.0f;

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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.QuickMove>();
            }

            return true;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            

            if (IsPlayerOnBlock)
            {
                if (behaviourContext.LastFrameCollisionInfo?.PreResolutionCollisionInfo != null)
                {
                    Blocks.QuickMove quickMove = behaviourContext.LastFrameCollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.QuickMove>().FirstOrDefault() as Blocks.QuickMove;
                    ratio = 1.0f + quickMove.Ratio * 0.1f;
                }
                else
                {
                    ratio = 1.0f;
                }
            }
            else
            {
                ratio = 1.0f;
            }

            return inputXVelocity * ratio;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity;
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

    }
}
