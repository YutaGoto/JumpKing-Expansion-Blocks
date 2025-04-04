using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System;
using System.Linq;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class TrapHopping : IBlockBehaviour
    {
        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        private readonly Random rng = new Random();
        private TrapHoppingType hoppingType = TrapHoppingType.None;

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
                IsPlayerOnBlock = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.IsCollidingWith<Blocks.TrapHopping>();
            }

            if (IsPlayerOnBlock)
            {
                GetHoppingType(behaviourContext);

                switch (hoppingType)
                {
                    case TrapHoppingType.Right:
                        bodyComp.Velocity.X = PlayerValues.WALK_SPEED;
                        break;
                    case TrapHoppingType.Left:
                        bodyComp.Velocity.X = -PlayerValues.WALK_SPEED;
                        break;
                    case TrapHoppingType.PlayerValue:
                        if(bodyComp.Velocity.X >= 0)
                        {
                            bodyComp.Velocity.X = PlayerValues.WALK_SPEED;
                        }
                        else
                        {
                            bodyComp.Velocity.X = -PlayerValues.WALK_SPEED;
                        }
                        break;
                    case TrapHoppingType.Random:
                        bodyComp.Velocity.X = (float)GetRandomNumber(-PlayerValues.SPEED, PlayerValues.SPEED);
                        break;
                    default:
                        break;
                }
                bodyComp.Velocity.Y = PlayerValues.JUMP * 0.2f;
                Traverse.Create(bodyComp).Field("_knocked").SetValue(true);
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

        private double GetRandomNumber(double minimum, double maximum)
        {
            return rng.NextDouble() * (maximum - minimum) + minimum;
        }

        private void GetHoppingType(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                Blocks.TrapHopping trapHopping= behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.TrapHopping>().FirstOrDefault() as Blocks.TrapHopping;
                switch (trapHopping.HoppingType)
                {
                    case 1:
                        hoppingType = TrapHoppingType.Right;
                        return;
                    case 2:
                        hoppingType = TrapHoppingType.Left;
                        return;
                    case 3:
                        hoppingType = TrapHoppingType.PlayerValue;
                        return;
                    case 4:
                        hoppingType = TrapHoppingType.Random;
                        return;
                    default:
                        hoppingType = TrapHoppingType.None;
                        return;
                }
            }
        }
    }

    internal enum TrapHoppingType
    {
        Right,
        Left,
        PlayerValue,
        Random,
        None,
    }
}
