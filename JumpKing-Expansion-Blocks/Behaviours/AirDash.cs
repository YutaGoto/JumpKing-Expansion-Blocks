using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class AirDash : IBlockBehaviour
    {
        private InputComponent m_input;
        private readonly PlayerEntity player;
        private bool doAirDash = false;
        private bool releasedJumpKey = true;


        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public AirDash(PlayerEntity player)
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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.AirDash>();
            }

            if (IsPlayerOnBlock && !player.m_body.IsOnGround && !player.m_body.IsKnocked)
            {
                InputComponent.State m_state = m_input.GetState();

                if (!doAirDash && m_state.jump)
                {
                    releasedJumpKey = false;
                }

                if (!releasedJumpKey && !doAirDash)
                {
                    doAirDash = true;

                    if (m_state.right)
                    {
                        bodyComp.Velocity.X = PlayerValues.SPEED * 1.05f;
                    }
                    else if (m_state.left)
                    {
                        bodyComp.Velocity.X = -PlayerValues.SPEED * 1.05f;
                    }
                    else
                    {
                        bodyComp.Velocity.X = 0;
                    }
                }

                if ((doAirDash && Math.Abs(bodyComp.Velocity.X) > 0.1f) && !releasedJumpKey)
                {
                    bodyComp.Velocity.Y = 0;
                }

                if (!m_state.jump)
                {
                    releasedJumpKey = true;
                }
            }

            if (player.m_body.IsKnocked || player.m_body.IsOnGround)
            {
                doAirDash = false;
                releasedJumpKey = true;
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
