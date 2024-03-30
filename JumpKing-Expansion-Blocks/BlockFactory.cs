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
        private static readonly Color CODE_HIGH_GRAVITY = new Color(128, 0, 0);
        private static readonly Color CODE_DEEP_WATER = new Color(0, 172, 172);
        private static readonly Color CODE_ACCELERATE = new Color(180, 255, 0);
        private static readonly Color CODE_INFINITY_JUMP = new Color(64, 255, 255);
        private static readonly Color CODE_WALL_JUMP = new Color(1, 1, 1);

        private static readonly Color CODE_SLIPPERY_ICE = new Color(0, 192, 255);
        private static readonly Color CODE_ZERO_FRICTION = new Color(0, 193, 255);
        private static readonly Color CODE_REFLECTOR = new Color(192, 0, 192);
        private static readonly Color CODE_QUICKSAND = new Color(255, 108, 0);
        private static readonly Color CODE_SIDESAND = new Color(255, 109, 0);
        private static readonly Color CODE_MAGIC_SAND = new Color(255, 110, 0);
        private static readonly Color CODE_CURSED_ICE = new Color(128, 128, 0);
        private static readonly Color CODE_COPIED_THIN_SNOW = new Color(255, 255, 129);

        private static readonly int CONVEYOR_R_MIN = 1;
        private static readonly int CONVEYOR_R_MAX = 30;
        private static readonly int CONVEYOR_G = 30;
        private static readonly int CONVEYOR_B_RIGHT = 30;
        private static readonly int CONVEYOR_B_LEFT = 31;

        private readonly HashSet<Color> supportedBlockCodes = new HashSet<Color>
        {
            CODE_HIGH_GRAVITY,
            CODE_DEEP_WATER,
            CODE_ACCELERATE,
            CODE_INFINITY_JUMP,
            CODE_WALL_JUMP,
            CODE_SLIPPERY_ICE,
            CODE_ZERO_FRICTION,
            CODE_REFLECTOR,
            CODE_QUICKSAND,
            CODE_SIDESAND,
            CODE_MAGIC_SAND,
            CODE_CURSED_ICE,
            CODE_COPIED_THIN_SNOW
        };

        private readonly ArrayList solidBlocksCode = new ArrayList
        {
            CODE_SLIPPERY_ICE,
            CODE_ZERO_FRICTION,
            CODE_REFLECTOR,
            CODE_QUICKSAND,
            CODE_SIDESAND,
            CODE_MAGIC_SAND,
            CODE_INFINITY_JUMP,
            CODE_WALL_JUMP,
            CODE_CURSED_ICE,
            CODE_COPIED_THIN_SNOW
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

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, JumpKing.Workshop.Level level, LevelTexture textureSrc, int currentScreem, int x, int y)
        {
            if (blockCode == CODE_HIGH_GRAVITY)
            {
                return new HighGravity(blockRect);
            }
            else if (blockCode == CODE_DEEP_WATER)
            {
                return new DeepWater(blockRect);
            }
            else if (blockCode == CODE_ACCELERATE)
            {
                return new Accelerate(blockRect);
            }
            else if (blockCode == CODE_INFINITY_JUMP)
            {
                return new InfinityJump(blockRect);
            }
            else if (blockCode == CODE_WALL_JUMP)
            {
                return new WallJump(blockRect);
            }
            else if (blockCode == CODE_SLIPPERY_ICE)
            {
                return new SlipperyIce(blockRect);
            }
            else if (blockCode == CODE_ZERO_FRICTION)
            {
                return new ZeroFriction(blockRect);
            }
            else if (blockCode == CODE_REFLECTOR)
            {
                return new Reflector(blockRect);
            }
            else if (blockCode == CODE_QUICKSAND)
            {
                return new Quicksand(blockRect);
            }
            else if (blockCode == CODE_SIDESAND)
            {
                return new SideSand(blockRect);
            }
            else if (blockCode == CODE_MAGIC_SAND)
            {
                return new MagicSand(blockRect);
            }
            else if (blockCode == CODE_CURSED_ICE)
            {
                return new CursedIce(blockRect);
            }
            else if (blockCode == CODE_COPIED_THIN_SNOW)
            {
                return new CopiedThinSnow(blockRect);
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

        private bool IsConveyorBlock(Color blockCode)
        {
            return blockCode.G == CONVEYOR_G && blockCode.R <= CONVEYOR_R_MAX && blockCode.R >= CONVEYOR_R_MIN  && (blockCode.B == CONVEYOR_B_RIGHT || blockCode.B == CONVEYOR_B_LEFT);
        }
    }
}
