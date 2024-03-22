﻿using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace JumpKing_Expansion_Blocks.Blocks
{
    public class Quicksand: BoxBlock
    {
        protected override bool canBlockPlayer => false;
        public Quicksand(Rectangle p_collider): base(p_collider) { }
    }
}
