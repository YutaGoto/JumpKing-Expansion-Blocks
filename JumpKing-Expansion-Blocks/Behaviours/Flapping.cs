using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;
using System.Collections.Generic;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class Flapping: IBlockBehaviour
    {
        private bool tapJumpButton = false;
        private int tapCount = 0;
        private List<float> powers = new List<float>() { 2f / 3f, 1f / 2f, 1f / 3f, 1f / 4f, 1f / 8f, 0f, -1f / 8f };

        private InputComponent m_input;
        private readonly PlayerEntity player;

        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public Flapping(PlayerEntity player)
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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.Flapping>();
            }
            
            if (IsPlayerOnBlock)
            {
                if (m_input.GetState().jump && !tapJumpButton && !player.m_body.IsOnGround && (tapCount >= 1 || (tapCount == 0 && bodyComp.Velocity.Y > 0)))
                {
                    tapJumpButton = true;
                    if (tapCount >= powers.Count - 1)
                    {
                        tapCount = powers.Count - 1;
                    }

                    bodyComp.Velocity.Y = PlayerValues.JUMP * powers[tapCount];
                    
                    tapCount++;
                }
            }

            if (!m_input.GetState().jump)
            {
                tapJumpButton = false;
            }

            if(bodyComp.IsOnGround)
            {
                tapCount = 0;
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
