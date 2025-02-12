using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class SuperCharge: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public readonly Rectangle collider;
        public int Ratio { get; private set; }

        public SuperCharge(Rectangle p_collider, byte ratio = 1) : base(p_collider)
        {
            collider = p_collider;
            Ratio = (int)ratio;
        }

        public Color DebugColor { get
            {
                return new Color(Constants.SuperChargeCodes.SUPER_CHARGE_R, Constants.SuperChargeCodes.SUPER_CHARGE_G, Ratio);
            }
        }
    }
}
