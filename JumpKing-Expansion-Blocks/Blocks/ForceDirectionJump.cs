using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class ForceDirectionJump : BoxBlock, IBlockDebugColor
    {
        protected override bool canBlockPlayer => false;
        public int Dir { get; private set; }
        public ForceDirectionJump(Rectangle p_collider, byte dir) : base(p_collider) 
        {
            Dir = (int)dir;
        }

        public Color DebugColor
        {
            get
            {
                return new Color(Constants.ForceDirectionJumpCodes.CODE_FORCE_DIRECTION_JUMP_R, Dir, Constants.ForceDirectionJumpCodes.CODE_FORCE_DIRECTION_JUMP_B);
            }
        }
    }
}
