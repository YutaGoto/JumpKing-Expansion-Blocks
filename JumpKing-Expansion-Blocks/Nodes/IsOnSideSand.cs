using BehaviorTree;
using EntityComponent;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Nodes
{
    public class IsOnSideSand: PlayerNode
    {
        public IsOnSideSand(Entity p_entity) : base(p_entity) { }

        protected override BTresult MyRun(TickData p_data)
        {
            if (!GetComponent<BodyComp>().IsOnBlock(typeof(Blocks.SideSand)))
            {
                return BTresult.Failure;
            }
            return BTresult.Success;
        }
    }
}
