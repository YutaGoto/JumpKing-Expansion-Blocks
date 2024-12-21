using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class QuickMove: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public readonly Rectangle collider;
        public int Ratio { get; private set; }

        public QuickMove(Rectangle p_collider, byte ratio = 0) : base(p_collider)
        {
            collider = p_collider;
            Ratio = (int)ratio; 
        }

        public Color DebugColor
        {
            get
            {
                return new Color(Constants.QuickMoveCodes.QUICK_MOVE_R, Ratio, Constants.QuickMoveCodes.QUICK_MOVE_B);
            }
        }
    }
}
