using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class LightWallJump : BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public LightWallJump(Rectangle p_collider) : base(p_collider) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_LIGHT_WALL_JUMP; } }
    }
}
