using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    internal class ForceFramesJump: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public readonly Rectangle collider;
        public int Frame { get; private set; }

        public ForceFramesJump(Rectangle p_collider, byte flames = 0) : base(p_collider)
        {
            collider = p_collider;
            Frame = (int)flames;
        }
        public Color DebugColor { get
            {
                return new Color(Frame, Constants.ForceFramesJumpCodes.FORCE_FRAME_JUMP_G, Constants.ForceFramesJumpCodes.FORCE_FRAME_JUMP_B);
            }
        }
    }
}
