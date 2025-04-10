using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    class AerialJump: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public AerialJump(Rectangle p_collidar) : base(p_collidar) { }
        public Color DebugColor { get { return Constants.ColorCodes.CODE_AERIAL_JUMP; } }
    }
}
