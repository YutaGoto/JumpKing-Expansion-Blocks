using JumpKing.Level;
using JumpKing_Expansion_Blocks.Behaviours;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class TrapHopping : BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public int HoppingType { get; private set; }

        public TrapHopping(Rectangle p_collider, byte hopping_type) : base(p_collider)
        {
            HoppingType = (int)hopping_type;
        }

        public Color DebugColor
        {
            get
            {
                return new Color(Constants.TrapHoppingCodes.TRAP_HOPPING_R, HoppingType, Constants.TrapHoppingCodes.TRAP_HOPPING_B);
            }
        }
    }
}
