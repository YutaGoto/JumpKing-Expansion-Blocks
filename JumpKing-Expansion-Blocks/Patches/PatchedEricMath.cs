using EntityComponent;
using ErikMaths;
using HarmonyLib;
using JumpKing.Player;
using System.Diagnostics;

namespace JumpKing_Expansion_Blocks.Patches
{
    [HarmonyPatch(typeof(ErikMath))]
    internal class PatchedEricMath
    {
        public PatchedEricMath(Harmony harmony)
        {
            var clampMethod = typeof(ErikMath).GetMethod("Clamp").MakeGenericMethod(typeof(float));
            harmony.Patch(
                clampMethod,
                new HarmonyMethod(AccessTools.Method(typeof(PatchedEricMath), nameof(PrefixClamp))),
                null
            );
        }

        private static bool PrefixClamp(float p_val, float p_min, float p_max, ref float __result)
        {
            PlayerEntity player = ModEntry.Player;
            if (player == null) return true;

            if (player.m_body.IsOnBlock<Blocks.NoResetVelocity>())
            {
                if (!PatchedJumpState.ResetVelocity)
                {
                    __result = p_val;
                    return false;
                }
            }

            return true;
        }


    }
}
