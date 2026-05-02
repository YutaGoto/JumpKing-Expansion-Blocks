using BehaviorTree;
using HarmonyLib;
using JumpKing.Level;
using JumpKing.Player;

namespace JumpKing_Expansion_Blocks.Patches
{
    [HarmonyPatch(typeof(Walk))]
    internal class PatchedWalk
    {

        public PatchedWalk(Harmony harmony)
        {
            harmony.Patch(
                AccessTools.Method(typeof(Walk), "MyRun"),
                new HarmonyMethod(AccessTools.Method(GetType(), nameof(PrefixRun))),
                null
            );
        }

        private static bool PrefixRun(ref BTresult __result)
        {
            PlayerEntity player = ModEntry.Player;
            if (player == null) return true;

            if (player.m_body.IsOnBlock<Blocks.RevokeWalking>() && player.m_body.IsOnGround)
            {
                __result = BTresult.Failure;

                if (!player.m_body.IsOnBlock<IceBlock>())
                {
                    player.m_body.Velocity.X = 0f;
                }
                return false;
            }

            if (player.m_body.IsOnBlock<Blocks.LightWallJump>() && player.m_body.IsOnGround)
            {
                __result = BTresult.Failure;
                return false;
            }

            return true;
        }
    }
}
