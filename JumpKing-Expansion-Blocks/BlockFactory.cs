using JumpKing.API;
using JumpKing.Level;
using JumpKing.Level.Sampler;
using JumpKing.Workshop;
using JumpKing_Expansion_Blocks.Blocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace JumpKing_Expansion_Blocks
{
    class BlockFactory: IBlockFactory
    {
        private static readonly Color CODE_HIGH_GRAVITY = new Color(128, 0, 0);
        private static readonly Color CODE_SLIPPERY_ICE = new Color(0, 192, 255);
        private static readonly Color CODE_QUICKSAND = new Color(255, 120, 0);
        private static readonly Color CODE_REFLECTOR = new Color(192, 0, 192);
        private static readonly Color CODE_COPPIED_THIN_SNOW = new Color(255, 255, 129);
        
        private readonly HashSet<Color> supportedBlockCodes = new HashSet<Color>
        {
            CODE_HIGH_GRAVITY,
            CODE_SLIPPERY_ICE,
            CODE_SLIPPERY_ICE,
            CODE_QUICKSAND,
            CODE_REFLECTOR,
            CODE_COPPIED_THIN_SNOW
        };

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            return supportedBlockCodes.Contains(blockCode);
        }

        public bool IsSolidBlock(Color blockCode)
        {
            if (blockCode == CODE_HIGH_GRAVITY)
            {
                return false;
            }
            else if (blockCode == CODE_SLIPPERY_ICE)
            {
                return true;
            }
            else if (blockCode == CODE_QUICKSAND)
            {
                return true;
            }
            else if (blockCode == CODE_REFLECTOR)
            {
                return true;
            }
            else if (blockCode == CODE_COPPIED_THIN_SNOW)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, JumpKing.Workshop.Level level, LevelTexture textureSrc, int currentScreem, int x, int y)
        {
            if (blockCode == CODE_HIGH_GRAVITY)
            {
                return new HighGravity(blockRect);
            }
            else if (blockCode == CODE_SLIPPERY_ICE)
            {
                return new SlipperyIce(blockRect);
            }
            else if (blockCode == CODE_QUICKSAND)
            {
                return new Quicksand(blockRect);
            }
            else if (blockCode == CODE_REFLECTOR)
            {
                return new Reflector(blockRect);
            }
            else if (blockCode == CODE_COPPIED_THIN_SNOW)
            {
                return new CopiedThinSnow(blockRect);
            }
            else
            {
                throw new InvalidOperationException($"{typeof(BlockFactory).Name} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
