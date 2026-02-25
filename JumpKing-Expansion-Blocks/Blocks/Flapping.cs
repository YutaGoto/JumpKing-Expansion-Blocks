using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class Flapping: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public Flapping(Rectangle p_collider) : base(p_collider) { }

        public Color DebugColor { get { return Constants.ColorCodes.CODE_FLAPPING; } }
    }
}
