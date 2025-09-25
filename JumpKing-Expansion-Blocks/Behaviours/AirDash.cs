using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;


namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class AirDash : IBlockBehaviour
    {
        private InputComponent m_input;
        private readonly PlayerEntity m_player;
        private readonly ICollisionQuery m_collisionQuery;
        private bool doAirDash = false;
        private bool releasedJumpKey = true;


        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public AirDash(PlayerEntity player, ICollisionQuery collisionQuery)
        {
            m_player = player ?? throw new ArgumentNullException("player");
            m_collisionQuery = collisionQuery ?? throw new ArgumentNullException("collisionQuery");

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
            m_input = m_player.GetComponent<InputComponent>();

            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.AirDash>();
            }

            if (IsPlayerOnBlock && !m_player.m_body.IsOnGround && !m_player.m_body.IsKnocked)
            {
                InputComponent.State m_state = m_input.GetState();

                if (!doAirDash && m_state.jump)
                {
                    releasedJumpKey = false;
                }

                if (!releasedJumpKey && !doAirDash && bodyComp.Velocity.Y > -1.0f)
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

            if (m_player.m_body.IsKnocked || m_player.m_body.IsOnGround)
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

        private bool CollidingSlopBlock()
        {
            int num = ((m_player.m_body.Velocity.X > 0f) ? 1 : (-1));
            m_player.m_body.Position.X -= num;
            Rectangle hitbox2 = m_player.m_body.GetHitbox();
            m_collisionQuery.CheckCollision(hitbox2, out Rectangle _, out AdvCollisionInfo advCollisionInfo2);

            IReadOnlyList<IBlock> collidedBlocks = advCollisionInfo2.GetCollidedBlocks();
            m_player.m_body.Position.X += num;
            foreach (IBlock block in collidedBlocks)
            {
                if (block is SlopeBlock)
                {
                    return true;
                }
            }
            return false;
        }

        
    }
}
