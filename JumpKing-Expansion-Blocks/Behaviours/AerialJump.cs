using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    class AerialJump: IBlockBehaviour
    {
        private bool setting = false;
        private float setVelocity;
        private readonly PlayerEntity player;

        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }

        public AerialJump(PlayerEntity player)
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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.AerialJump>();
            }

            if (IsPlayerOnBlock && player.m_body.IsOnGround)
            {
                setting = true;
            }

            if (setting)
            {
                setVelocity = Math.Min(bodyComp.Velocity.Y, setVelocity);
                if (bodyComp.Velocity.Y > 0.3f)
                {
                    bodyComp.Velocity.Y = setVelocity;
                    setting = false;
                    setVelocity = 0f;
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
