using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class AutoJumpCharge: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public int Direction { get; private set; } 
        public AutoJumpCharge(Rectangle p_collider, byte direction) : base(p_collider)
        {
            Direction = (int)direction;
        }

        public Color DebugColor
        {
            get
            {
                return new Color(Constants.AutoJumpChargeColorCodes.CODE_AUTO_JUMP_CHARGE_R, Direction, Constants.AutoJumpChargeColorCodes.CODE_AUTO_JUMP_CHARGE_B);
            }
        }
    }
}
