using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class JkqPlatform: BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public int Form { get; private set; }
        public JkqPlatform(Rectangle p_collider, byte form) : base(p_collider)
        {
            Form = (int)form;
        }

        public Color DebugColor
        {
            get
            {
                return new Color(Constants.JkqPlatformsCodes.JKQ_R, Constants.JkqPlatformsCodes.JKQ_G, Form);
            }
        }
    }
}
