using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class SideLock: IBlockBehaviour
    {
        public float BlockPriority => 3f;

        public bool IsPlayerOnBlock { get; set; }
        public Blocks.SideLock _sideLockBlock;

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
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.SideLock>();
            }

            if (IsPlayerOnBlock && behaviourContext?.LastFrameCollisionInfo?.PreResolutionCollisionInfo != null)
            {
                _sideLockBlock = (Blocks.SideLock)behaviourContext.LastFrameCollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.SideLock>().FirstOrDefault();

                if (_sideLockBlock != null)
                {
                    if (_sideLockBlock.Direction == 98 && bodyComp.Velocity.X > 0) // stop RIGHT
                    {
                        bodyComp.Velocity.X = 0.0f;
                    }
                    else if (_sideLockBlock.Direction == 99 && bodyComp.Velocity.X < 0) // stop LEFT
                    {
                        bodyComp.Velocity.X = 0.0f;
                    }
                }
            }

            return true;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (!IsPlayerOnBlock || _sideLockBlock == null) return inputXVelocity;

            if (_sideLockBlock.Direction == 98 && bodyComp.Velocity.X > 0 || _sideLockBlock.Direction == 99 && bodyComp.Velocity.X < 0)
            {
                return 0.0f;
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
    }
}
