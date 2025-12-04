using JumpKing.API;
using JumpKing.Level;
using JumpKing.Level.Sampler;
using JumpKing.Workshop;
using JumpKing_Expansion_Blocks.Blocks;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace JumpKing_Expansion_Blocks
{
    class BlockFactory: IBlockFactory
    {
        public static ulong LastUsedMapId { get; private set; } = ulong.MaxValue;

        private readonly HashSet<Color> supportedBlockCodes = new HashSet<Color>
        {
            Constants.ColorCodes.CODE_HIGH_GRAVITY,
            Constants.ColorCodes.CODE_RAIN_GRAVITY,
            Constants.ColorCodes.CODE_SPECIAL_HIGH_GRAVITY,
            Constants.ColorCodes.CODE_SUPER_LOW_GRAVITY,
            Constants.ColorCodes.CODE_DEEP_WATER,
            Constants.ColorCodes.CODE_ACCELERATE,
            Constants.ColorCodes.CODE_SPEED_UP,
            Constants.ColorCodes.CODE_INFINITY_JUMP,
            Constants.ColorCodes.CODE_WALL_JUMP,
            Constants.ColorCodes.CODE_SLIPPERY_ICE,
            Constants.ColorCodes.CODE_ZERO_FRICTION,
            Constants.ColorCodes.CODE_ONE_WAY_ICE_BLOCK,
            Constants.ColorCodes.CODE_HEAVY_ICE,
            Constants.ColorCodes.CODE_TRAMPOLINE,
            Constants.ColorCodes.CODE_QUICKSAND,
            Constants.ColorCodes.CODE_SIDE_SAND,
            Constants.ColorCodes.CODE_MAGIC_SAND,
            Constants.ColorCodes.CODE_UP_SAND,
            Constants.ColorCodes.CODE_RESTRAINED_ICE,
            Constants.ColorCodes.CODE_CURSED_ICE,
            Constants.ColorCodes.CODE_REVERSED_WALK,
            Constants.ColorCodes.CODE_REVERSED_CHARGE,
            Constants.ColorCodes.CODE_REVERSED_GRAVITY,
            Constants.ColorCodes.CODE_ASCEND,
            Constants.ColorCodes.CODE_MOVE_UP,
            Constants.ColorCodes.CODE_DOUBLE_JUMP,
            Constants.ColorCodes.CODE_JUMP_STEP_HOP,
            Constants.ColorCodes.CODE_AIR_JUMP,
            Constants.ColorCodes.CODE_AERIAL_JUMP,
            Constants.ColorCodes.CODE_CLOUD_JUMP,
            Constants.ColorCodes.CODE_AIR_DASH,
            Constants.ColorCodes.CODE_DISABLED_JUMP,
            Constants.ColorCodes.CODE_DISABLED_SMALL_JUMP,
            Constants.ColorCodes.CODE_REVOKE_JUMP_CHARGE,
            Constants.ColorCodes.CODE_REVOKE_WALKING,
            Constants.ColorCodes.CODE_FORCE_NEUTRAL_JUMP,
            Constants.ColorCodes.CODE_ANTI_GIANT_BOOTS,
            Constants.ColorCodes.CODE_SOFT_PLATFORM,
            Constants.ColorCodes.CODE_CEILING_SHIFT,
        };

        private readonly ArrayList solidBlocksCode = new ArrayList
        {
            Constants.ColorCodes.CODE_SLIPPERY_ICE,
            Constants.ColorCodes.CODE_ZERO_FRICTION,
            Constants.ColorCodes.CODE_ONE_WAY_ICE_BLOCK,
            Constants.ColorCodes.CODE_HEAVY_ICE,
            Constants.ColorCodes.CODE_TRAMPOLINE,
            Constants.ColorCodes.CODE_QUICKSAND,
            Constants.ColorCodes.CODE_SIDE_SAND,
            Constants.ColorCodes.CODE_MAGIC_SAND,
            Constants.ColorCodes.CODE_UP_SAND,
            Constants.ColorCodes.CODE_INFINITY_JUMP,
            Constants.ColorCodes.CODE_WALL_JUMP,
            Constants.ColorCodes.CODE_RESTRAINED_ICE,
            Constants.ColorCodes.CODE_CURSED_ICE,
            Constants.ColorCodes.CODE_REVERSED_WALK
        };

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            if (!supportedBlockCodes.Contains(blockCode))
            {
                if (IsConveyorBlock(blockCode))
                {
                    return true;
                }
                if (IsMultiWarpBlock(blockCode))
                {
                    return true;
                }
                if (IsQuickMoveBlock(blockCode))
                {
                    return true;
                }
                if (IsSuperChargeBlock(blockCode))
                {
                    return true;
                }
                if (IsSideLockBlock(blockCode))
                {
                    return true;
                }
                if (IsReflectorWallBlock(blockCode))
                {
                    return true;
                }
                if (IsJkqPlatformBlock(blockCode))
                {
                    return true;
                }
                if (IsTrapHoppingBlock(blockCode))
                {
                    return true;
                }
                if (IsAutoJumpChargeBlock(blockCode))
                {
                    return true;
                }
                if (IsForceFramesJump(blockCode))
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        public bool IsSolidBlock(Color blockCode)
        {
            if (solidBlocksCode.Contains(blockCode) )
            {
                return true;
            }
            else if (IsConveyorBlock(blockCode))
            {
                return true;
            }
            else if (IsReflectorWallBlock(blockCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, JumpKing.Workshop.Level level, LevelTexture textureSrc, int currentScreen, int x, int y)
        {
            if (LastUsedMapId != level.ID)
            {
                LastUsedMapId = level.ID;
            }

            if (blockCode == Constants.ColorCodes.CODE_HIGH_GRAVITY)
            {
                return new HighGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_RAIN_GRAVITY)
            {
                return new RainGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SPECIAL_HIGH_GRAVITY)
            {
                return new SpecialHighGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SUPER_LOW_GRAVITY)
            {
                return new SuperLowGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_DEEP_WATER)
            {
                return new DeepWater(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_ACCELERATE)
            {
                return new Accelerate(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SPEED_UP)
            {
                return new SpeedUp(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_INFINITY_JUMP)
            {
                return new InfinityJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_WALL_JUMP)
            {
                return new WallJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SLIPPERY_ICE)
            {
                return new SlipperyIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_ZERO_FRICTION)
            {
                return new ZeroFriction(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_ONE_WAY_ICE_BLOCK)
            {
                return new OneWayIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_HEAVY_ICE)
            {
                return new HeavyIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_TRAMPOLINE)
            {
                return new Trampoline(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_QUICKSAND)
            {
                return new Quicksand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SIDE_SAND)
            {
                return new SideSand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_MAGIC_SAND)
            {
                return new MagicSand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_UP_SAND)
            {
                return new UpSand(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_RESTRAINED_ICE)
            {
                return new RestrainedIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_CURSED_ICE)
            {
                return new CursedIce(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REVERSED_WALK)
            {
                return new ReversedWalk(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REVERSED_CHARGE)
            {
                return new ReversedCharge(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REVERSED_GRAVITY)
            {
                return new ReversedGravity(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_ASCEND)
            {
                return new Ascend(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_MOVE_UP)
            {
                return new MoveUp(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_DOUBLE_JUMP)
            {
                return new DoubleJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_JUMP_STEP_HOP)
            {
                return new JumpStepHop(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_AERIAL_JUMP)
            {
                return new AerialJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_AIR_JUMP)
            {
                return new AirJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_CLOUD_JUMP)
            {
                return new CloudJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_AIR_DASH)
            {
                return new AirDash(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_DISABLED_JUMP)
            {
                return new ForceFramesJump(blockRect, 1);
            }
            else if (blockCode == Constants.ColorCodes.CODE_DISABLED_SMALL_JUMP)
            {
                return new DisabledSmallJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REVOKE_JUMP_CHARGE)
            {
                return new RevokeJumpCharge(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_REVOKE_WALKING)
            {
                return new RevokeWalking(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_FORCE_NEUTRAL_JUMP)
            {
                return new ForceNeutralJump(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_ANTI_GIANT_BOOTS)
            {
                return new AntiGiantBoots(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_SOFT_PLATFORM)
            {
                return new SoftPlatform(blockRect);
            }
            else if (blockCode == Constants.ColorCodes.CODE_CEILING_SHIFT)
            {
                return new CeilingShift(blockRect);
            }

            else if (IsConveyorBlock(blockCode))
            {
                return new Conveyor(blockRect, blockCode.R, blockCode.B);
            }
            else if (IsMultiWarpBlock(blockCode))
            {
                return new MultiWarp(blockRect, blockCode.R, blockCode.G);
            }
            else if (IsQuickMoveBlock(blockCode))
            {
                return new QuickMove(blockRect, blockCode.G);
            }
            else if (IsSuperChargeBlock(blockCode))
            {
                return new SuperCharge(blockRect, blockCode.B);
            }
            else if (IsSideLockBlock(blockCode))
            {
                return new SideLock(blockRect, blockCode.R);
            }
            else if (IsReflectorWallBlock(blockCode))
            {
                return new Reflector(blockRect, blockCode.R);
            }
            else if (IsJkqPlatformBlock(blockCode))
            {
                return new JkqPlatform(blockRect, blockCode.B);
            }
            else if (IsTrapHoppingBlock(blockCode))
            {
                return new TrapHopping(blockRect, blockCode.G);
            }
            else if (IsAutoJumpChargeBlock(blockCode))
            {
                return new AutoJumpCharge(blockRect, blockCode.G);
            }
            else if (IsForceFramesJump(blockCode))
            {
                return new ForceFramesJump(blockRect, blockCode.R);
            }
            else
            {
                throw new InvalidOperationException($"{typeof(BlockFactory).Name} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }

        private static bool IsConveyorBlock(Color blockCode)
        {
            return blockCode.G == Constants.ConveyorSpeedCodes.CONVEYOR_G
                    && blockCode.R <= Constants.ConveyorSpeedCodes.CONVEYOR_R_MAX && blockCode.R >= Constants.ConveyorSpeedCodes.CONVEYOR_R_MIN
                    && (blockCode.B == Constants.ConveyorSpeedCodes.CONVEYOR_B_RIGHT || blockCode.B == Constants.ConveyorSpeedCodes.CONVEYOR_B_LEFT);
        }

        private static bool IsMultiWarpBlock(Color blockCode)
        {
            return blockCode.G >= Constants.MultiWarpColorCodes.MULTI_WARP_G_MIN && blockCode.G <= Constants.MultiWarpColorCodes.MULTI_WARP_G_MAX
					&& blockCode.B == Constants.MultiWarpColorCodes.MULTI_WARP_B
                    && blockCode.R >= Constants.MultiWarpColorCodes.MULTI_WARP_R_MIN && blockCode.R <= Constants.MultiWarpColorCodes.MULTI_WARP_R_MAX;
        }

        private static bool IsQuickMoveBlock(Color blockCode)
        {
            return blockCode.R == Constants.QuickMoveCodes.QUICK_MOVE_R && blockCode.G >= Constants.QuickMoveCodes.QUICK_MOVE_G_MIN && blockCode.G <= Constants.QuickMoveCodes.QUICK_MOVE_G_MAX && blockCode.B == Constants.QuickMoveCodes.QUICK_MOVE_B;
        }

        private static bool IsSuperChargeBlock(Color blockCode)
        {
            return blockCode.R == Constants.SuperChargeCodes.SUPER_CHARGE_R
                    && blockCode.G == Constants.SuperChargeCodes.SUPER_CHARGE_G
                    && blockCode.B >= Constants.SuperChargeCodes.SUPER_CHARGE_B_MIN && blockCode.B <= Constants.SuperChargeCodes.SUPER_CHARGE_B_MAX;
        }

        private static bool IsSideLockBlock(Color blockCode)
        {
            return (blockCode.R == Constants.SideLockColorCodes.SIDE_LOCK_R_RIGHT || blockCode.R == Constants.SideLockColorCodes.SIDE_LOCK_R_LEFT)
                    && blockCode.G == Constants.SideLockColorCodes.SIDE_LOCK_G
                    && blockCode.B == Constants.SideLockColorCodes.SIDE_LOCK_B;
        }

        private static bool IsReflectorWallBlock(Color blockCode)
        {
            return blockCode.R >= Constants.ReflectorWallCodes.REFLECTOR_R_MIN && blockCode.R <= Constants.ReflectorWallCodes.REFLECTOR_R_MAX
                    && blockCode.G == Constants.ReflectorWallCodes.REFLECTOR_G
                    && blockCode.B == Constants.ReflectorWallCodes.REFLECTOR_B;
        }

        private static bool IsJkqPlatformBlock(Color blockCode)
        {
            return blockCode.R == Constants.JkqPlatformsCodes.JKQ_R
                    && blockCode.G <= Constants.JkqPlatformsCodes.JKQ_G
                    && (blockCode.B >= Constants.JkqPlatformsCodes.PLATFORM && blockCode.B <= Constants.JkqPlatformsCodes.CEIL);
        }

        private static bool IsTrapHoppingBlock(Color blockCode)
        {
            return blockCode.R == Constants.TrapHoppingCodes.TRAP_HOPPING_R
                    && (blockCode.G >= Constants.TrapHoppingCodes.TRAP_HOPPING_G_RIGHT && blockCode.G <= Constants.TrapHoppingCodes.TRAP_HOPPING_G_RANDOM)
                    && blockCode.B == Constants.TrapHoppingCodes.TRAP_HOPPING_B;
        }

        private static bool IsAutoJumpChargeBlock(Color blockCode)
        {
            return blockCode.R == Constants.AutoJumpChargeColorCodes.CODE_AUTO_JUMP_CHARGE_R
                    && blockCode.G >= Constants.AutoJumpChargeColorCodes.CODE_AUTO_JUMP_CHARGE_G_CONTROLLABLE && blockCode.G <= Constants.AutoJumpChargeColorCodes.CODE_AUTO_JUMP_CHARGE_G_RIGHT
                    && blockCode.B == Constants.AutoJumpChargeColorCodes.CODE_AUTO_JUMP_CHARGE_B;
        }

        private static bool IsForceFramesJump(Color blockCode)
        {
            return blockCode.R >= Constants.ForceFramesJumpCodes.FORCE_FRAME_JUMP_R_MIN && blockCode.R <= Constants.ForceFramesJumpCodes.FORCE_FRAME_JUMP_R_MAX
                    && blockCode.G == Constants.ForceFramesJumpCodes.FORCE_FRAME_JUMP_G
                    && blockCode.B == Constants.ForceFramesJumpCodes.FORCE_FRAME_JUMP_B;
        }
    }
}
