using ErikMaths;
using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing_Expansion_Blocks.Patches;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class SlideOn : IBlockBehaviour
    {
        public float BlockPriority => 2f;

        /// <inheritdoc/>
        public bool IsPlayerOnBlock { get; set; }
        private bool isSliding { get; set; } = false;
        private bool OnSlideWall { get; set; } = true;
        private PlayerEntity Player = ModEntry.Player;

        /// <inheritdoc/>
        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info.IsCollidingWith<Blocks.SlideOn>())
            {
                OnSlideWall = true;
                return !IsPlayerOnBlock;
            }

            return false;
        }

        /// <inheritdoc/>
        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info.IsCollidingWith<Blocks.SlideOn>())
            {
                OnSlideWall = false;
                return !IsPlayerOnBlock;
            }
            return false;
        }

        /// <inheritdoc/>
        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            if (isSliding)
            {
                return 0f;
            }
            return inputGravity;
        }

        /// <inheritdoc/>
        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            if (IsPlayerOnBlock && !isSliding && !OnSlideWall)
            {
                isSliding = true;
                
                if (Math.Abs(Player.m_body.Velocity.X) <= PlayerValues.SPEED)
                {
                    SpriteEffects spriteEffect = Traverse.Create(Player).Field("m_flip").GetValue<SpriteEffects>();
                    if (spriteEffect == SpriteEffects.FlipHorizontally)
                    {
                         Player.m_body.Velocity.X = -PlayerValues.SPEED;
                    }
                    else
                    {
                         Player.m_body.Velocity.X = PlayerValues.SPEED;
                    }
                    
                }
                Player.m_body.Velocity.Y = 0f;
            }
            return inputXVelocity;
        }

        /// <inheritdoc/>
        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            if (IsPlayerOnBlock && !isSliding && OnSlideWall)
            {
                isSliding = true;
                if (Player.m_body.Velocity.Y > 0f)
                {
                    Player.m_body.Velocity.Y = PlayerValues.SPEED;
                }
                else
                {
                    Player.m_body.Velocity.Y = -PlayerValues.SPEED;
                }
                Player.m_body.Velocity.X = 0f;
            }
            return inputYVelocity;
        }

        /// <inheritdoc/>
        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.SlideOn>();
            }

            if (isSliding)
            {
                int baseFrame = PatchedJumpState.JumpFrames;
                if (OnSlideWall)
                {
                    Player.m_body.Velocity.Y = ErikMath.MoveTowards(Player.m_body.Velocity.Y, 0f, (PlayerValues.ICE_FRICTION / 2f) * (6.0f / baseFrame));
                }
                else
                {
                    Player.m_body.Velocity.X = ErikMath.MoveTowards(Player.m_body.Velocity.X, 0f, (PlayerValues.ICE_FRICTION / 2f) * (6.0f / baseFrame));
                }
            }

            if (isSliding && !IsPlayerOnBlock && ((!OnSlideWall && (Player.m_body.IsKnocked || Player.m_body.Velocity.X == 0f)) || (OnSlideWall && Player.m_body.Velocity.Y == 0f)))
            {
                isSliding = false;
            }

            return true;
        }
    }
}
