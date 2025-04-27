using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class AutoJumpCharge: IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        public Direction dirctionType { get; set; } = Direction.None;

        public AutoJumpCharge() { }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.AutoJumpCharge>();
            }

            if (IsPlayerOnBlock)
            {
                GetDirection(behaviourContext);
            }
            return true;
        }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;
        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext) => false;
        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext) => inputGravity;
        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext) => inputXVelocity;
        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext) => inputYVelocity;

        internal void GetDirection(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                Blocks.AutoJumpCharge autoJumpCharge = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.AutoJumpCharge>().FirstOrDefault() as Blocks.AutoJumpCharge;
                switch (autoJumpCharge.Direction)
                {
                    case 233:
                        dirctionType = Direction.Controllable;
                        return;
                    case 234:
                        dirctionType = Direction.Left;
                        return;
                    case 235:
                        dirctionType = Direction.Right;
                        return;
                    default:
                        dirctionType = Direction.None;
                        return;
                }
            }
        }
    }

    public enum Direction
    {
        Controllable,
        Left,
        Right,
        None
    }
}
