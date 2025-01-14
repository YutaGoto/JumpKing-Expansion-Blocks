using BehaviorTree;
using EntityComponent;
using HarmonyLib;
using JumpKing;
using JumpKing.Player;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace JumpKing_Expansion_Blocks.Patches
{
    [HarmonyPatch(typeof(JumpState))]
    internal class PatchedJumpState
    {
        private static float previous_timer { get; set; }

        public static int JumpFrames { get; internal set; }

        public PatchedJumpState(Harmony harmony)
        {
            harmony.Patch(AccessTools.Method(typeof(JumpState), "MyRun"), new HarmonyMethod(AccessTools.Method(GetType(), "PrefixRun")), new HarmonyMethod(AccessTools.Method(GetType(), "Run")));
            
            harmony.Patch(
                AccessTools.Method(typeof(JumpState), "DoJump"), 
                new HarmonyMethod(AccessTools.Method(GetType(), "Jump")), 
                null,
                new HarmonyMethod(AccessTools.Method(typeof(PatchedJumpState), nameof(transpileDoJump)))
            );
        }

        private static bool PrefixRun(ref BTresult __result)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();

            if (player != null && player.m_body.IsOnBlock<Blocks.Trampoline>())
            {
                __result = BTresult.Failure;
                return false;
            }

            return true;
        }

        private static void Run(TickData p_data, BTresult __result, JumpState __instance)
        {
            if (__result != BTresult.Failure)
            {
                float m_timer = (float)Traverse.Create(__instance).Field("m_timer").GetValue();
                if (__result == BTresult.Success)
                {
                    m_timer = previous_timer + p_data.delta_time * __instance.body.GetMultipliers();
                }
                if (__instance.last_result != 0)
                {
                    JumpFrames = -1;
                }
                JumpFrames++;
                previous_timer = m_timer;
            }
        }

        private static void Jump(ref float p_intensity)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            if (player != null)
            {
                if (player.m_body.IsOnBlock<Blocks.ReversedCharge>())
                {
                    p_intensity = (1.0f + 2.0f / (PlayerValues.FPS * PlayerValues.JUMP_TIME)) - p_intensity;
                }

                if (player.m_body.IsOnBlock<Blocks.DisabledJump>())
                {
                    p_intensity = 2.0f / (PlayerValues.FPS * PlayerValues.JUMP_TIME);
                }
            }
        }

        private static IEnumerable<CodeInstruction> transpileDoJump(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            CodeMatcher matcher = new CodeMatcher(instructions, generator);

            matcher.MatchStartForward(
                // base.body.Velocity.Y = JUMP_STRENGTH * p_intensity;
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Call, AccessTools.Method("JumpKing.Player.PlayerNode:get_body")),
                new CodeMatch(OpCodes.Ldflda, AccessTools.Field("JumpKing.Player.BodyComp:Velocity")),
                new CodeMatch(OpCodes.Ldarg_0),
                new CodeMatch(OpCodes.Call, AccessTools.Method("JumpKing.Player.JumpState:get_JUMP_STRENGTH")),
                new CodeMatch(OpCodes.Ldarg_1),
                new CodeMatch(OpCodes.Mul),
                new CodeMatch(OpCodes.Stfld, AccessTools.Field("Microsoft.Xna.Framework.Vector2:Y"))
            ).ThrowIfInvalid($"Cant find code in {nameof(JumpState)}");
            matcher.Advance(5);
            matcher.InsertAndAdvance(
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(PatchedJumpState), nameof(negative)))
            );

            return matcher.Instructions();
        }

        private static float negative(float value)
        {
            return Utils.negative(value);
        }
    }
}
