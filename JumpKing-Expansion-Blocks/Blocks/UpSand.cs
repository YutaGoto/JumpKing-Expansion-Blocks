using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class UpSand : BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public UpSand(Rectangle p_collider) : base(p_collider) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_UP_SAND; } }
    }
}
