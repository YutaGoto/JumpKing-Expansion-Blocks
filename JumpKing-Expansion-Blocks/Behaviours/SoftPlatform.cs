using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Controller;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class SoftPlatform: IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info != null && behaviourContext != null)
            {
                BodyComp bodyComp = behaviourContext.BodyComp;
                PadState m_padState = ControllerManager.instance.GetPadState();

                return info.IsCollidingWith<Blocks.SoftPlatform>()
                    && !behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.SoftPlatform>()
                    && bodyComp.Velocity.Y > 0.0f 
                    && !m_padState.down;
            }

            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.SoftPlatform>();
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
