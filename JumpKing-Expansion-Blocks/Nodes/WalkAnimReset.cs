using System;
using BehaviorTree;
using JumpKing;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Nodes
{
    public class WalkAnimReset : PlayerNode
    {
        private readonly WalkAnim walkAnimNode;

        public WalkAnimReset(WalkAnim walkAnimNode, PlayerEntity playerEntity) : base(playerEntity)
        {
            this.walkAnimNode = walkAnimNode ?? throw new ArgumentNullException("walkAnimNode");
        }

        protected override BTresult MyRun(TickData p_data)
        {
            if (GetComponent<BodyComp>().IsOnBlock(typeof(Blocks.CompactedSnowAndIce)))
            {
                base.player.SetSprite(Game1.instance.contentManager.playerSprites.idle);
                walkAnimNode?.Reset();
            }

            return BTresult.Success;
        }
    }
}
