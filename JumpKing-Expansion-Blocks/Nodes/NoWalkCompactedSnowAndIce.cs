using BehaviorTree;
using ErikMaths;
using JumpKing;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Nodes
{
    public class NoWalkCompactedSnowAndIce: PlayerNode
    {
        public NoWalkCompactedSnowAndIce(PlayerEntity playerEntity): base(playerEntity) { }

        protected override BTresult MyRun(TickData p_data)
        {
            if (GetComponent<BodyComp>().IsOnBlock(typeof(Blocks.CompactedSnowAndIce)))
            {
                base.body.Velocity.X = ErikMath.MoveTowards(base.body.Velocity.X, 0f, PlayerValues.ICE_FRICTION * 2f);
            }
            return BTresult.Success;
        }
    }
}
