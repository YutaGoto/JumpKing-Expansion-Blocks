using JumpKing.API;
using JumpKing.Level;
using JumpKing.Level.Sampler;
using JumpKing.Workshop;
using JumpKing_Expansion_Blocks.Blocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JumpKing_Expansion_Blocks
{
    class BlockFactory: IBlockFactory
    {
        private readonly HashSet<Color> supportedBlockCodes = new HashSet<Color>
        {
            Constants.ColorCodes.CODE_HIGH_GRAVITY,
            Constants.ColorCodes.CODE_RAIN_GRAVITY,
            Constants.ColorCodes.CODE_SPECIAL_HIGH_GRAVITY,
            Constants.ColorCodes.CODE_DEEP_WATER,
            Constants.ColorCodes.CODE_ACCELERATE,
            Constants.ColorCodes.CODE_INFINITY_JUMP,
            Constants.ColorCodes.CODE_WALL_JUMP,
            Constants.ColorCodes.CODE_SLIPPERY_ICE,
            Constants.ColorCodes.CODE_ZERO_FRICTION,
            Constants.ColorCodes.CODE_REFLECTOR,
            Constants.ColorCodes.CODE_TRAMPOLINE,
            Constants.ColorCodes.CODE_QUICKSAND,
            Constants.ColorCodes.CODE_SIDE_SAND,
            Constants.ColorCodes.CODE_MAGIC_SAND,
            Constants.ColorCodes.CODE_UP_SAND,
            Constants.ColorCodes.CODE_RESTRAINED_ICE,
            Constants.ColorCodes.CODE_CURSED_ICE,
            Constants.ColorCodes.CODE_REVERSED_WALK,
            Constants.ColorCodes.CODE_REVERSED_CHARGE,
            Constants.ColorCodes.CODE_DOUBLE_JUMP,
            Constants.ColorCodes.CODE_CLOUD_JUMP,
            Constants.ColorCodes.CODE_DISABLED_JUMP,
        };

        private readonly ArrayList solidBlocksCode = new ArrayList
        {
            Constants.ColorCodes.CODE_SLIPPERY_ICE,
            Constants.ColorCodes.CODE_ZERO_FRICTION,
            Constants.ColorCodes.CODE_REFLECTOR,
            Constants.ColorCodes.CODE_TRAMPOLINE,
            Constants.ColorCodes.CODE_QUICKSAND,
            Constants.ColorCodes.CODE_SIDE_SAND,
            Constants.ColorCodes.CODE_MAGIC_SAND,
            Constants.ColorCodes.CODE_UP_SAND,
            Constants.ColorCodes.CODE_INFINITY_JUMP,
            Constants.ColorCodes.CODE_WALL_JUMP,
            Constants.ColorCodes.CODE_RESTRAINED_ICE,
            Constants.ColorCodes.CODE_CURSED_ICE,
            Constants.ColorCodes.CODE_REVERSED_WALK
        };

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            if (!supportedBlockCodes.Contains(blockCode))
            {
                if (IsConveyorBlock(blockCode))
                {
                    return true;
                }
                return false;
            }

            return true;
        }

        public bool IsSolidBlock(Color blockCode)
        {
            if (solidBlocksCode.Contains(blockCode) )
            {
                return true;
            }
            else if (IsConveyorBlock(blockCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, JumpKing.Workshop.Level level, LevelTexture textureSrc, int currentScreen, int x, int y)
        {
            if (blockCode == Constants.ColorCodes.CODE_HIGH_GRAVITY)
            {
                return new HighGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_RAIN_GRAVITY)
            {
                return new RainGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SPECIAL_HIGH_GRAVITY)
            {
                return new SpecialHighGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_DEEP_WATER)
            {
                return new DeepWater(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_ACCELERATE)
            {
                return new Accelerate(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_INFINITY_JUMP)
            {
                return new InfinityJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_WALL_JUMP)
            {
                return new WallJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SLIPPERY_ICE)
            {
                return new SlipperyIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_ZERO_FRICTION)
            {
                return new ZeroFriction(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REFLECTOR)
            {
                return new Reflector(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_TRAMPOLINE)
            {
                return new Trampoline(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_QUICKSAND)
            {
                return new Quicksand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SIDE_SAND)
            {
                return new SideSand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_MAGIC_SAND)
            {
                return new MagicSand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_UP_SAND)
            {
                return new UpSand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_RESTRAINED_ICE)
            {
                return new RestrainedIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_CURSED_ICE)
            {
                return new CursedIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REVERSED_WALK)
            {
                return new ReversedWalk(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REVERSED_CHARGE)
            {
                return new ReversedCharge(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_DOUBLE_JUMP)
            {
                return new DoubleJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_CLOUD_JUMP)
            {
                return new CloudJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_DISABLED_JUMP)
            {
                return new DisabledJump(blockRect);
            }
            else if (IsConveyorBlock(blockCode))
            {
                return new Conveyor(blockRect, blockCode.R, blockCode.B);
            }
            else
            {
                throw new InvalidOperationException($"{typeof(BlockFactory).Name} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }

        private static bool IsConveyorBlock(Color blockCode)
        {
            return blockCode.G == Constants.ConveyorSpeedCodes.CONVEYOR_G && blockCode.R <= Constants.ConveyorSpeedCodes.CONVEYOR_R_MAX && blockCode.R >= Constants.ConveyorSpeedCodes.CONVEYOR_R_MIN && (blockCode.B == Constants.ConveyorSpeedCodes.CONVEYOR_B_RIGHT || blockCode.B == Constants.ConveyorSpeedCodes.CONVEYOR_B_LEFT);
        }
    }
}
