using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing_Expansion_Blocks.Patches;
using Microsoft.Xna.Framework;
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
        private Vector2 TargetPosition { get; set; } = new Vector2(0, 0);

        private float UpdateAxisVelocity(float currentVelocity, float currentPosition, float targetPosition, float deceleration, float slowDownDistance, float snapDistance, float minNearSpeed)
        {
            float distanceToTarget = targetPosition - currentPosition;
            float absDistance = Math.Abs(distanceToTarget);

            if (absDistance <= 1f)
            {
                return 0f;
            }

            float direction = Math.Sign(distanceToTarget);
            float targetSpeed = (float)Math.Sqrt(2f * deceleration * absDistance);
            float nearFactor = Math.Min(absDistance / slowDownDistance, 1f);
            targetSpeed *= nearFactor;
            targetSpeed = Math.Max(targetSpeed, minNearSpeed);
            targetSpeed = Math.Min(targetSpeed, PlayerValues.SPEED);
            float desiredVelocity = direction * targetSpeed;

            if (absDistance <= snapDistance)
            {
                return desiredVelocity;
            }

            if (currentVelocity < desiredVelocity)
            {
                return Math.Min(currentVelocity + deceleration, desiredVelocity);
            }

            return Math.Max(currentVelocity - deceleration, desiredVelocity);
        }

        /// <inheritdoc/>
        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info.IsCollidingWith<Blocks.SlideOn>())
            {
                Vector2 pos = TargetPosition;
                int baseFrame = PatchedJumpState.JumpFrames;
                if (Player.m_body.Velocity.Y < 0f)
                {
                    pos.Y = Player.m_body.Position.Y - (baseFrame * 8f);
                }
                else
                {
                    pos.Y = Player.m_body.Position.Y + (baseFrame * 8f);
                }
                TargetPosition = pos;
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
                Vector2 pos = TargetPosition;
                int baseFrame = PatchedJumpState.JumpFrames;
                SpriteEffects spriteEffect = Traverse.Create(Player).Field("m_flip").GetValue<SpriteEffects>();
                if (spriteEffect == SpriteEffects.FlipHorizontally)
                {
                    pos.X = Player.m_body.Position.X - (baseFrame * 8f);
                }
                else
                {
                    pos.X = Player.m_body.Position.X + (baseFrame * 8f);
                }
                TargetPosition = pos;
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
                float deceleration = PlayerValues.ICE_FRICTION / 4f;
                float slowDownDistance = 32f;
                float snapDistance = 4f;
                float minNearSpeed = PlayerValues.SPEED * 0.2f;

                if (OnSlideWall)
                {
                    Player.m_body.Velocity.Y = UpdateAxisVelocity(
                        Player.m_body.Velocity.Y,
                        Player.m_body.Position.Y,
                        TargetPosition.Y,
                        deceleration,
                        slowDownDistance,
                        snapDistance,
                        minNearSpeed);
                }
                else
                {
                    Player.m_body.Velocity.X = UpdateAxisVelocity(
                        Player.m_body.Velocity.X,
                        Player.m_body.Position.X,
                        TargetPosition.X,
                        deceleration,
                        slowDownDistance,
                        snapDistance,
                        minNearSpeed);
                }
            }

            if (isSliding && !IsPlayerOnBlock && ((!OnSlideWall && (Player.m_body.IsKnocked || Player.m_body.Velocity.X == 0f)) || (OnSlideWall && Player.m_body.Velocity.Y == 0f)))
            {
                isSliding = false;
            }

            if (isSliding)
            {
                if (OnSlideWall && (Player.m_body.Velocity.Y > 0 && Player.m_body.Position.Y >= TargetPosition.Y || Player.m_body.Velocity.Y < 0 && Player.m_body.Position.Y <= TargetPosition.Y))
                {
                    isSliding = false;
                    TargetPosition = new Vector2(0, 0);
                }

                if (!OnSlideWall && (Player.m_body.Velocity.X > 0 && Player.m_body.Position.X >= TargetPosition.X || Player.m_body.Velocity.X < 0 && Player.m_body.Position.X <= TargetPosition.X))
                {
                    isSliding = false;
                    TargetPosition = new Vector2(0, 0);
                }

                if (!OnSlideWall && (Player.m_body.Position.X < 0 || Player.m_body.Position.X > 462))
                {
                    isSliding = false;
                    TargetPosition = new Vector2(0, 0);
                }

                if (OnSlideWall && (Player.m_body.Position.Y > TargetPosition.Y && Player.m_body.Velocity.Y >= -0.1f))
                {
                    isSliding = false;
                    TargetPosition = new Vector2(0, 0);
                }
            }

            return true;
        }
    }
}
