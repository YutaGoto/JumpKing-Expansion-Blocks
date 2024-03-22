using BehaviorTree;
using EntityComponent;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Nodes
{
    public class IsOnQuickSand: PlayerNode
    {
        public IsOnQuickSand(Entity p_entity) : base(p_entity) { }

        protected override BTresult MyRun(TickData p_data)
        {
            if (!GetComponent<BodyComp>().IsOnBlock(typeof(Blocks.Quicksand)))
            {
                return BTresult.Failure;
            }
            return BTresult.Success;
        }
    }
}
