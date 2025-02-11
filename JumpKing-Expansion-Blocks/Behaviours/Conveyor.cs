using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class Conveyor: IBlockBehaviour
    {
        public float BlockPriority => 1f;

        public bool IsPlayerOnBlock { get; set; }

        public bool IsPlayerOnBlockLastFrame { get; set; }
        public Vector2 IsPlayerOnLastFramePosition { get; set; }

        public Blocks.Conveyor _conveyorBlock;

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
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (IsPlayerOnBlock && bodyComp.IsOnGround)
            {
                if (behaviourContext.LastFrameCollisionInfo?.PreResolutionCollisionInfo != null)
                {
                    _conveyorBlock = (Blocks.Conveyor)behaviourContext.LastFrameCollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.Conveyor>().FirstOrDefault();
                    IsPlayerOnBlockLastFrame = IsPlayerOnBlock;
                    IsPlayerOnLastFramePosition = bodyComp.Position;

                    if (_conveyorBlock.Direction == 30) // LEFT
                    {
                        return inputXVelocity + (float)_conveyorBlock.Speed * -0.2f;
                    }
                    else // RIGHT
                    {
                        return inputXVelocity + (float)_conveyorBlock.Speed * 0.2f;
                    }
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
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.Conveyor>();
            }

            return true;
        }
    }
}
