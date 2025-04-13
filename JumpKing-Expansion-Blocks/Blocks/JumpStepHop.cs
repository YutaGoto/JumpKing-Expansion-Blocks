using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class JumpStepHop: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public JumpStepHop(Rectangle p_collider) : base(p_collider) { }
        public Color DebugColor { get { return Constants.ColorCodes.CODE_JUMP_STEP_HOP; } }
    }
}
