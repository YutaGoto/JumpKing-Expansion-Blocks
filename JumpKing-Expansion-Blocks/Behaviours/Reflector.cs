using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class Reflector: IBlockBehaviour
    {
        public float BlockPriority => 2f;
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

            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.Reflector>();
            }

            if (IsPlayerOnBlock)
            {
                if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
                {
                    Blocks.Reflector reflector = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.Reflector>().FirstOrDefault() as Blocks.Reflector;
                    ratio = 1.0f + (reflector.Ratio - 182.0f) * 0.1f;
                }
                else
                {
                    ratio = 1.0f;
                }
            }

            if (IsPlayerOnBlock && !bodyComp.IsOnGround)
            {
                bodyComp.Velocity.X *= ratio;
            }

            return true;
        }
    }
}
