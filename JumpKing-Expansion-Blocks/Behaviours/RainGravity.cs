using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing_Expansion_Blocks.Patches;
using System;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    public class RainGravity : IBlockBehaviour
    {
        public float BlockPriority => 2f;
        private float yBaseVelocity;

        public bool IsPlayerOnBlock { get; set; }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            return false;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;
            if (behaviourContext?.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.RainGravity>();
            }

            if (IsPlayerOnBlock)
            {
                yBaseVelocity = AdjustYVelocity();
                bodyComp.Velocity.Y = Math.Max(bodyComp.Velocity.Y, yBaseVelocity);
            }

            if (bodyComp.Velocity.Y > 0 && IsPlayerOnBlock)
            {
                bodyComp.Velocity.Y = Math.Min(bodyComp.Velocity.Y + 0.15f, 10);
            }

            return inputYVelocity;
        }


        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {    
            return true;
        }

        private float AdjustYVelocity()
        {
            int baseFrame = PatchedJumpState.JumpFrames;
            return baseFrame switch
            {
                0 => 0.000f,
                1 => -0.700f,
                2 => -0.950f,
                3 => -1.200f,
                4 => -1.450f,
                5 => -1.700f,
                6 => -1.950f,
                7 => -2.200f,
                8 => -2.450f,
                9 => -2.700f,
                10 => -2.950f,
                11 => -3.200f,
                12 => -3.450f,
                13 => -3.700f,
                14 => -3.950f,
                15 => -4.200f,
                16 => -4.450f,
                17 => -4.698f,
                18 => -4.950f,
                19 => -5.200f,
                20 => -5.450f,
                21 => -5.655f,
                22 => -5.950f,
                23 => -6.203f,
                24 => -6.462f,
                25 => -6.715f,
                26 => -6.970f,
                27 => -7.228f,
                _ => -7.435f,
            };
        }

    }
}
