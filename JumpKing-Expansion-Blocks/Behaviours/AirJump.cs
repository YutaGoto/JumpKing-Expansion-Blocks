using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class AirJump : IBlockBehaviour
    {
        private bool airJumpFlag;
        private bool doAirJump = false;
        private bool tapJumpButton = false;
        private InputComponent m_input;
        private readonly PlayerEntity player;

        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public AirJump(PlayerEntity player)
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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.AirJump>();
            }
            if (IsPlayerOnBlock)
            {
                airJumpFlag = true;
            }
            else if (!IsPlayerOnBlock)
            {
                airJumpFlag = false;
                doAirJump = false;
            }

            if (airJumpFlag && IsPlayerOnBlock && !doAirJump && bodyComp.Velocity.Y > -1.0f && m_input.TryConsumeJump() && !tapJumpButton)
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
                tapJumpButton = true;
                bodyComp.Velocity.Y = PlayerValues.JUMP * (2f / 3f);
                airJumpFlag = false;
                doAirJump = true;
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
