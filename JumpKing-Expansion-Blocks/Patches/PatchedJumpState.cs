using BehaviorTree;
using EntityComponent;
using ErikMaths;
using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing_Expansion_Blocks.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JumpKing_Expansion_Blocks.Patches
{
    [HarmonyPatch(typeof(JumpState))]
    internal class PatchedJumpState
    {
        private static float previous_timer { get; set; }
        public static float t_timer { get; set; } = 0f;
        private static Behaviours.AutoJumpCharge _autoJumpChargeBevaviour;
        private static Behaviours.ForceDirectionJump _forceDirectionJumpBehaviour;
        private static InputComponent m_input;
        public static bool ResetVelocity { get; internal set; }

        public static int JumpFrames { get; internal set; }

        public PatchedJumpState(Harmony harmony)
        {
            harmony.Patch(
                AccessTools.Method(typeof(JumpState), "MyRun"),
                new HarmonyMethod(AccessTools.Method(GetType(), nameof(PrefixRun))),
                new HarmonyMethod(AccessTools.Method(GetType(), nameof(Run)))
            );
            harmony.Patch(AccessTools.Method(typeof(JumpState), "DoJump"), new HarmonyMethod(AccessTools.Method(GetType(), nameof(Jump))), null);
            harmony.Patch(AccessTools.Method(typeof(JumpState), "Start"), new HarmonyMethod(AccessTools.Method(GetType(), nameof(CheckStart))), null);
        }

        private static bool PrefixRun(TickData p_data, ref BTresult __result, JumpState __instance)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();

            if (player != null && player.m_body.IsOnBlock<Blocks.Trampoline>())
            {
                __result = BTresult.Failure;
                return false;
            }

            if (player != null && player.m_body.IsOnBlock<Blocks.RevokeJumpCharge>())
            {
                __result = BTresult.Failure;
                return false;
            }

            if (player != null && player.m_body.IsOnBlock<Blocks.AutoJumpCharge>() && (player.m_body.IsOnGround || player.m_body.IsOnBlock<SandBlock>()))
            {
                if (t_timer <= 0.0f)
                {
                    if (!player.m_body.IsOnBlock<IceBlock>())
                        player.m_body.Velocity.X = 0f;
                    AccessTools.Method(typeof(JumpState), "Start").Invoke(__instance, null);
                }

                m_input = player.GetComponent<InputComponent>();

                if ((t_timer >= PlayerValues.JUMP_TIME || m_input.GetState().jump) && t_timer > 0.0f)
                {
                    FieldInfo blockBehavioursField = AccessTools.Field(typeof(BodyComp), "m_blockBehaviours");
                    LinkedList<IBlockBehaviour> m_blockBehaviours = (LinkedList<IBlockBehaviour>)blockBehavioursField.GetValue(player.m_body);

                    using (LinkedList<IBlockBehaviour>.Enumerator enumerator = m_blockBehaviours.GetEnumerator())
                        while (enumerator.MoveNext())
                        {
                            IBlockBehaviour current = enumerator.Current;
                            if (current.GetType() == typeof(Behaviours.AutoJumpCharge))
                            {
                                _autoJumpChargeBevaviour = (Behaviours.AutoJumpCharge)current;
                            }
                        }

                    AccessTools.Method(typeof(JumpState), "DoJump").Invoke(__instance, new object[] { Math.Min(1f, t_timer / PlayerValues.JUMP_TIME) });

                    if (_autoJumpChargeBevaviour != null)
                    {
                        if (_autoJumpChargeBevaviour.dirctionType == Behaviours.Direction.Right)
                        {
                            player.m_body.Velocity.X = PlayerValues.SPEED;
                        }
                        else if (_autoJumpChargeBevaviour.dirctionType == Behaviours.Direction.Left)
                        {
                            player.m_body.Velocity.X = -PlayerValues.SPEED;
                        }
                    }

                    t_timer = 0f;
                    __result = BTresult.Success;
                    return false;
                }
                else
                {
                    t_timer += (1.0f / (float)PlayerValues.FPS) * player.m_body.GetMultipliers();

                    if (!player.m_body.IsOnBlock<IceBlock>())
                    {
                        player.m_body.Velocity.X = 0f;
                    }

                    __result = BTresult.Running;
                    return false;
                }
            }
            else if (player != null && player.m_body.IsOnBlock<Blocks.NoResetVelocity>())
            {
                t_timer = 0.0f;

                m_input = player.GetComponent<InputComponent>();
                InputComponent.State state = m_input.GetState();

                object inputBuffer = Traverse.Create(__instance).Field("m_left_right_input_buffer").GetValue();
                AccessTools.Method(inputBuffer.GetType(), "Push").Invoke(inputBuffer, new object[] { state });

                float m_timer = (float)Traverse.Create(__instance).Field("m_timer").GetValue();
                if (m_timer == 0f && !m_input.TryConsumeJump())
                {
                    __result = BTresult.Failure;
                    return false;
                }

                if (m_timer == 0f && state.jump)
                {
                    AccessTools.Method(typeof(JumpState), "Start").Invoke(__instance, null);
                }

                m_timer += p_data.delta_time * __instance.body.GetMultipliers();
                Traverse.Create(__instance).Field("m_timer").SetValue(m_timer);

                if (m_timer >= PlayerValues.JUMP_TIME || !state.jump)
                {
                    ResetVelocity = false;
                    AccessTools.Method(typeof(JumpState), "DoJump").Invoke(__instance, new object[] { Math.Min(1f, m_timer / PlayerValues.JUMP_TIME) });
                    __result = BTresult.Success;
                    ResetVelocity = true;
                    return false;
                }

                __result = BTresult.Running;
                return false;
            }
            else
            {
                t_timer = 0.0f;
            }

            return true;
        }

        private static void Run(TickData p_data, BTresult __result, JumpState __instance)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();

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

            if (player != null && player.m_body.IsOnBlock<Blocks.ForceDirectionJump>() && __result == BTresult.Success)
            {
                FieldInfo blockBehavioursField = AccessTools.Field(typeof(BodyComp), "m_blockBehaviours");
                LinkedList<IBlockBehaviour> m_blockBehaviours = (LinkedList<IBlockBehaviour>)blockBehavioursField.GetValue(player.m_body);

                using (LinkedList<IBlockBehaviour>.Enumerator enumerator = m_blockBehaviours.GetEnumerator())
                    while (enumerator.MoveNext())
                    {
                        IBlockBehaviour current = enumerator.Current;
                        if (current.GetType() == typeof(Behaviours.ForceDirectionJump))
                        {
                            _forceDirectionJumpBehaviour = (Behaviours.ForceDirectionJump)current;
                        }
                    }

                if (_forceDirectionJumpBehaviour != null)
                {
                    if (_forceDirectionJumpBehaviour.direction == Behaviours.ForceDirectionJump.Direction.Right)
                    {
                        player.m_body.Velocity.X = PlayerValues.SPEED;
                    }
                    else if (_forceDirectionJumpBehaviour.direction == Behaviours.ForceDirectionJump.Direction.Left)
                    {
                        player.m_body.Velocity.X = -PlayerValues.SPEED;
                    }
                    else if (_forceDirectionJumpBehaviour.direction == Behaviours.ForceDirectionJump.Direction.Neutral)
                    {
                        player.m_body.Velocity.X = 0f;
                    }
                }
            }

            if (player != null && player.m_body.IsOnBlock<Blocks.DiamondHandsIce>() && __result == BTresult.Success)
            {
                m_input = player.GetComponent<InputComponent>();

                if (m_input.GetState().right)
                {
                    player.m_body.Velocity.X = PlayerValues.SPEED;
                }
                else if (m_input.GetState().left)
                {
                    player.m_body.Velocity.X = -PlayerValues.SPEED;
                }
                else
                {
                    player.m_body.Velocity.X = 0f;
                }
            }
        }

        private static bool Jump(ref float p_intensity)
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            if (player != null)
            {
                if (player.m_body.IsOnBlock<Blocks.ReversedCharge>())
                {
                    p_intensity = (1.0f + 2.0f / (PlayerValues.FPS * PlayerValues.JUMP_TIME)) - p_intensity;
                }

                if (player.m_body.IsOnBlock<Blocks.ForceFramesJump>())
                {
                    p_intensity = GetForceFrames.Frames(player) / (PlayerValues.FPS * PlayerValues.JUMP_TIME);
                }

                if (player.m_body.IsOnBlock<Blocks.SuperCharge>())
                {
                    p_intensity *= GetSuperChargePower.Power(player);
                }

                if (player.m_body.IsOnBlock<Blocks.HeavyIce>() && p_intensity <= 0.2f)
                {
                    p_intensity = 0.0f;
                    return false;
                }

                if (player.m_body.IsOnBlock<Blocks.DisabledSmallJump>() && p_intensity <= 0.2f)
                {
                    p_intensity = 0.0f;
                    return false;
                }

                if (player.m_body.IsOnBlock<Blocks.RevokeJumpCharge>())
                {
                    p_intensity = 0.0f;
                    return false;
                }
            }

            return true;
        }

        private static bool CheckStart()
        {
            PlayerEntity player = EntityManager.instance.Find<PlayerEntity>();
            if (player != null)
            {
                return !player.m_body.IsOnBlock<Blocks.RevokeJumpCharge>();
            }

            return true;
        }
    }
}
