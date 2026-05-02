using BehaviorTree;
using EntityComponent;
using HarmonyLib;
using JumpKing;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.Player;
using JumpKing_Expansion_Blocks.Utils;
using Microsoft.Xna.Framework;
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
            PlayerEntity player = ModEntry.Player;
            if (player == null) return true;

            if (player.m_body.IsOnBlock<Blocks.Trampoline>())
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
            PlayerEntity player = ModEntry.Player;
            if (player == null) return;

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

        private static bool Jump(ref float p_intensity, JumpState __instance)
        {
            PlayerEntity player = ModEntry.Player;
            if (player == null) return true;

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

                if (player.m_body.IsOnBlock<Blocks.NoResetVelocity>())
                {
                    m_input = player.GetComponent<InputComponent>();
                    int x = m_input.GetState().dpad.X;
                    if (x == 0)
                    {
                        object m_left_right_input_buffer = Traverse.Create(__instance).Field("m_left_right_input_buffer").GetValue();
                        if (m_left_right_input_buffer != null)
                        {
                            Type bufferType = m_left_right_input_buffer.GetType();
                            PropertyInfo validCountProperty = AccessTools.Property(bufferType, "ValidCount") ?? AccessTools.Property(bufferType, "Count");
                            FieldInfo validCountField = AccessTools.Field(bufferType, "ValidCount") ?? AccessTools.Field(bufferType, "m_valid_count");
                            MethodInfo getItemMethod = AccessTools.Method(bufferType, "GetItem") ?? AccessTools.Method(bufferType, "get_Item");

                            int validCount = 0;
                            object validCountValue = validCountProperty != null
                                ? validCountProperty.GetValue(m_left_right_input_buffer, null)
                                : validCountField?.GetValue(m_left_right_input_buffer);
                            if (validCountValue != null)
                            {
                                validCount = Convert.ToInt32(validCountValue);
                            }

                            if (getItemMethod != null && validCount > 0)
                            {
                                for (int i = 0; i < validCount; i++)
                                {
                                    object item = getItemMethod.Invoke(m_left_right_input_buffer, new object[] { i });
                                    if (item == null)
                                    {
                                        continue;
                                    }

                                    Type itemType = item.GetType();
                                    PropertyInfo dpadProperty = AccessTools.Property(itemType, "dpad");
                                    FieldInfo dpadField = AccessTools.Field(itemType, "dpad");
                                    object dpad = dpadProperty != null ? dpadProperty.GetValue(item, null) : dpadField?.GetValue(item);
                                    if (dpad == null)
                                    {
                                        continue;
                                    }

                                    Type dpadType = dpad.GetType();
                                    PropertyInfo xProperty = AccessTools.Property(dpadType, "X");
                                    FieldInfo xField = AccessTools.Field(dpadType, "X");
                                    object xValue = xProperty != null ? xProperty.GetValue(dpad, null) : xField?.GetValue(dpad);
                                    if (xValue == null)
                                    {
                                        continue;
                                    }

                                    int bufferedX = Convert.ToInt32(xValue);
                                    if (bufferedX != 0)
                                    {
                                        x = bufferedX;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    player.m_body.Velocity.Y = PlayerValues.JUMP * p_intensity;
                    player.m_body.Velocity.X += (float)x * PlayerValues.SPEED;
                    player.SetDirection(x);
                    Type bodyCompType = player.m_body.GetType();

                    AccessTools.Method(typeof(JumpState), "Reset").Invoke(__instance, null);
                    HandleParticles(player.m_body);
                    HandleSounds(player.m_body);

                    if (PlayerEntity.OnJumpCall != null)
                    {
                        PlayerEntity.OnJumpCall();
                    }

                    return false;
                }

            return true;
        }

        private static bool CheckStart()
        {
            PlayerEntity player = ModEntry.Player;
            if (player == null) return true;

            if (player.m_body.IsOnBlock<Blocks.RevokeJumpCharge>())
            {
                return false;
            }

            return true;
        }

        private static void HandleSounds(BodyComp bodycomp)
        {
            if (bodycomp.IsOnBlock(typeof(WaterBlock)))
            {
                Game1.instance?.contentManager?.audio?.player?.WaterJump?.PlayOneShot();
                return;
            }
            if (bodycomp.IsOnBlock(typeof(IceBlock)))
            {
                Game1.instance?.contentManager?.audio?.player?.IceJump?.PlayOneShot();
                return;
            }
            if (bodycomp.IsOnBlock(typeof(SnowBlock)))
            {
                Game1.instance?.contentManager?.audio?.player?.SnowJump?.PlayOneShot();
                return;
            }

            Game1.instance?.contentManager?.audio?.player?.Jump?.PlayOneShot();
        }

        private static void HandleParticles(BodyComp bodycomp)
        {
            Rectangle hitbox = bodycomp.GetHitbox();
            Type jumpParticleEntityType = AccessTools.TypeByName("JumpParticleEntity");
            if (jumpParticleEntityType != null)
            {
                Type particleSpawnerType = jumpParticleEntityType.GetNestedType("ParticleSpawner", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                if (particleSpawnerType != null)
                {
                    if (bodycomp.IsOnBlock(typeof(WaterBlock)))
                    {
                        MethodInfo createWaterParticle = AccessTools.Method(particleSpawnerType, "CreateWaterParticle");
                        if (createWaterParticle != null)
                        {
                            createWaterParticle.Invoke(null, new object[] { hitbox.Center.X, hitbox.Bottom });
                            return;
                        }
                    }
                    MethodInfo createParticle = AccessTools.Method(particleSpawnerType, "CreateParticle");
                    if (createParticle != null)
                    {
                        createParticle.Invoke(null, new object[] { hitbox.Center.X, hitbox.Bottom });
                    }
                }
            }
        }
    }
}
