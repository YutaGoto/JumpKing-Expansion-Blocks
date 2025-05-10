
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class JumpStepHop: IBlockBehaviour
    {
        private bool stepFlag;
        private bool hopFlag;

        private float jumpVelocity;
        private InputComponent m_input;
        private bool tapJumpButton = false;
        private readonly PlayerEntity player;

        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public JumpStepHop(PlayerEntity player)
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
            m_input = player.GetComponent<InputComponent>();

            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.JumpStepHop>();
            }
            if (IsPlayerOnBlock && player.m_body.IsOnGround)
            {
                stepFlag = true;
                hopFlag = true;
                jumpVelocity = 0f;
            }
            else if (!IsPlayerOnBlock && player.m_body.IsOnGround)
            {
                stepFlag = false;
                hopFlag = false;
            }

            if (!stepFlag && hopFlag)
            {
                if (bodyComp.Velocity.Y > -1.0f && m_input.TryConsumeJump() && !tapJumpButton)
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
                    bodyComp.Velocity.Y = jumpVelocity / 2.0f;
                    tapJumpButton = true;
                    hopFlag = false;
                }
            }

            if (stepFlag && hopFlag)
            {
                jumpVelocity = Math.Min(bodyComp.Velocity.Y, jumpVelocity);
                if (bodyComp.Velocity.Y > -1.0f && m_input.TryConsumeJump() && !tapJumpButton)
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
                    bodyComp.Velocity.Y = jumpVelocity / 3.0f * 2.0f;
                    tapJumpButton = true;
                    stepFlag = false;
                }
            }

            if (!m_input.GetState().jump)
            {
                tapJumpButton = false;
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
