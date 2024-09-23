using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class DoubleJump: IBlockBehaviour
    {
        private bool doubleJumpFlag;
        private float doubleJumpVelocity;
        private InputComponent m_input;
        private readonly PlayerEntity player;

        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public DoubleJump(PlayerEntity player)
        {
            this.player = player ?? throw new ArgumentNullException("player");
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
            BodyComp bodyComp = behaviourContext.BodyComp;
            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.DoubleJump>();
            }
            if (IsPlayerOnBlock && player.m_body.IsOnGround)
            {
                doubleJumpFlag = true;
                doubleJumpVelocity = 0f;
            }
            else if (!IsPlayerOnBlock && player.m_body.IsOnGround)
            {
                doubleJumpFlag = false;
            }
            if (doubleJumpFlag)
            {
                doubleJumpVelocity = Math.Min(bodyComp.Velocity.Y, doubleJumpVelocity);
                m_input = player.GetComponent<InputComponent>();
                if (bodyComp.Velocity.Y > -1.0f && m_input.GetState().jump && !player.m_body.IsOnGround)
                {
                    if (m_input.GetState().right)
                    {
                        bodyComp.Velocity.X = PlayerValues.SPEED;
                    }
                    else if (m_input.GetState().left)
                    {
                        bodyComp.Velocity.X = -PlayerValues.SPEED;
                    }
                    else
                    {
                        bodyComp.Velocity.X = 0f;
                    }
                    bodyComp.Velocity.Y = doubleJumpVelocity;
                    doubleJumpFlag = false;
                    doubleJumpVelocity = 0f;
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
