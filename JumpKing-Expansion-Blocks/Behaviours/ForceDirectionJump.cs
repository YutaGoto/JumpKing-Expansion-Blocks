using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class ForceDirectionJump : IBlockBehaviour
    {
        public float BlockPriority => 3f;

        public bool IsPlayerOnBlock { get; set; }
        internal Direction direction;

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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.ForceDirectionJump>();
            }

            if (IsPlayerOnBlock)
            {
                GetDirection(behaviourContext);
            }

            return true;
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

        internal void GetDirection(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                Blocks.ForceDirectionJump forceDirectionJump = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.ForceDirectionJump>().FirstOrDefault() as Blocks.ForceDirectionJump;

                switch (forceDirectionJump.Dir)
                {
                    case Constants.ForceDirectionJumpCodes.CODE_FORCE_DIRECTION_JUMP_G_RIGHT:
                        direction = Direction.Right;
                        break;
                    case Constants.ForceDirectionJumpCodes.CODE_FORCE_DIRECTION_JUMP_G_LEFT:
                        direction = Direction.Left;
                        break;
                    case Constants.ForceDirectionJumpCodes.CODE_FORCE_DIRECTION_JUMP_G_NEUTRAL:
                        direction = Direction.Neutral;
                        break;
                    default:
                        direction = Direction.None;
                        break;
                }
            }

            
        }

        internal enum Direction
        {
            Right,
            Left,
            Neutral,
            None
        }
    }
}
