using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class Reflector: BoxBlock, IBlockDebugColor
    {
        public readonly Rectangle collider;
        public int Ratio { get; private set; }

        public Reflector(Rectangle p_collider, byte rario = 1) : base(p_collider)
        {
            collider = p_collider;
            Ratio = (int)rario;
        }

        public Color DebugColor
        {
            get
            {
                return new Color(Ratio, Constants.ReflectorWallCodes.REFLECTOR_G, Constants.ReflectorWallCodes.REFLECTOR_B);
            }
        }
    }
}
