using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System.Diagnostics;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class JkqPlatform : IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        internal static bool isThroughPlatform { get; set; } = false;
        private static Form formType { get; set; }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info != null && behaviourContext != null)
            {
                BodyComp bodyComp = behaviourContext.BodyComp;

                if (
                    info.IsCollidingWith<Blocks.JkqPlatform>()
                    && !behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>()
                    && !isThroughPlatform
                )
                {
                    GetForm(behaviourContext);
                    switch (formType)
                    {
                        case Form.Platform:
                            return false;
                        case Form.RightWall:
                            return bodyComp.Velocity.X <= 0f;
                        case Form.LeftWall:
                            return bodyComp.Velocity.X >= 0f;
                        default:
                            return true;
                    }
                }
            }

            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info != null && behaviourContext != null)
            {
                BodyComp bodyComp = behaviourContext.BodyComp;

                if (
                    info.IsCollidingWith<Blocks.JkqPlatform>()
                    && !behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>()
                    && !isThroughPlatform
                )
                {
                    GetForm(behaviourContext);
                    switch (formType)
                    {
                        case Form.Ceil:
                            return bodyComp.Velocity.Y <= 0.0f;
                        default:
                            if (bodyComp.IsOnGround) return true;

                            return bodyComp.Velocity.Y >= 0.0f;
                    }
                }
            }

            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>();
            }

            if (bodyComp.IsOnGround)
            {
                isThroughPlatform = false;
            }

            isThroughPlatform = isThroughPlatform || IsPlayerOnBlock;
            
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

        private void GetForm(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                Blocks.JkqPlatform jkqPlatform = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.JkqPlatform>().FirstOrDefault() as Blocks.JkqPlatform;
                switch (jkqPlatform.Form)
                {
                    case 21:
                        formType = Form.Platform;
                        return;
                    case 22:
                        formType = Form.RightWall;
                        return;
                    case 23:
                        formType = Form.LeftWall;
                        return;
                    case 24:
                        formType = Form.BothWall;
                        return;
                    case 25:
                        formType = Form.Ceil;
                        return;
                    default:
                        formType = Form.None;
                        return;
                }
            }
        }
    }

    internal enum Form
    {
        Platform,
        RightWall,
        LeftWall,
        BothWall,
        Ceil,
        None
    }
}
