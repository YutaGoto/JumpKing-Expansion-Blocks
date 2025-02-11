using HarmonyLib;
using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JumpKing_Expansion_Blocks.Patches
{
    public static class PatchedResolveXCollisionBehaviour
    {
        private static BodyComp _bodyComp;
        private static AdvCollisionInfo _advCollisionInfo;
        private static Behaviours.Conveyor _conveyorBlockBehaviour;
        private static Rectangle _overlap;

        public static void UpdateXIfCompressedAgainstWall(ResolveXCollisionBehaviour __instance, BehaviourContext behaviourContext)
        {
            FieldInfo collisionQueryField = AccessTools.Field(typeof(ResolveXCollisionBehaviour), "m_collisionQuery");
            ICollisionQuery _m_collisionQuery = (ICollisionQuery)collisionQueryField.GetValue(__instance);

            FieldInfo blockBehavioursField = AccessTools.Field(typeof(ResolveXCollisionBehaviour), "m_blockBehaviours");
            LinkedList<IBlockBehaviour> _m_blockBehaviours = (LinkedList<IBlockBehaviour>)blockBehavioursField.GetValue(__instance);

            _bodyComp = behaviourContext.BodyComp;
            Rectangle hitbox = _bodyComp.GetHitbox();
            bool collisionCheck = _m_collisionQuery.CheckCollision(hitbox, out Rectangle overlap, out AdvCollisionInfo advCollisionInfo);
            _advCollisionInfo = advCollisionInfo;
            _overlap = overlap;

            if (!collisionCheck) return;
            
            using (LinkedList<IBlockBehaviour>.Enumerator enumerator = _m_blockBehaviours.GetEnumerator())
                while (enumerator.MoveNext())
                {
                    IBlockBehaviour current = enumerator.Current;
                    if (current.GetType() == typeof(Behaviours.Conveyor))
                    {
                        _conveyorBlockBehaviour = (Behaviours.Conveyor)current;

                        if (!_bodyComp.IsOnGround) return;
                        if (_conveyorBlockBehaviour._conveyorBlock == null) return;
                        if (IsBouncingAgainstConveyorBlock()) return;

                        if (_conveyorBlockBehaviour.IsPlayerOnBlock)
                        {
                            if (AnySlopeCollided())
                            {
                                _bodyComp.Position.X = _conveyorBlockBehaviour.IsPlayerOnLastFramePosition.X;
                                return;
                            }

                            _bodyComp.Position.X -= _overlap.Width * Math.Sign(_conveyorBlockBehaviour._conveyorBlock.Speed) * (IsCompressedAgainstWall() ? 1 : -1);

                            return;
                        }
                    }
                }
        }

        private static bool AnySlopeCollided()
        {
            IReadOnlyList<IBlock> collidedBlocks = _advCollisionInfo.GetCollidedBlocks();
            foreach (IBlock block in collidedBlocks)
            {
                if (block is SlopeBlock)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsCompressedAgainstWall()
        {
            var collidedBlocks = _advCollisionInfo.GetCollidedBlocks<BoxBlock>().Except(_advCollisionInfo.GetCollidedBlocks<Blocks.Conveyor>());
            var collidedConveyorBlock = _conveyorBlockBehaviour._conveyorBlock;

            foreach (var block in collidedBlocks)
            {
                if (block.GetRect().Location.Y >= collidedConveyorBlock.GetRect().Location.Y)
                {
                    continue;
                }
                if (collidedConveyorBlock.Speed >= 0)
                {
                    if (block.GetRect().Location.X > collidedConveyorBlock.GetRect().Location.X)
                    {
                        return true;
                    }
                }
                else
                {
                    if (block.GetRect().Location.X < collidedConveyorBlock.GetRect().Location.X)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IsBouncingAgainstConveyorBlock()
        {
            if (!_conveyorBlockBehaviour.IsPlayerOnBlock && !_conveyorBlockBehaviour.IsPlayerOnBlockLastFrame)
            {
                return false;
            }
            if (_conveyorBlockBehaviour._conveyorBlock.GetRect().Location.Y >= (_bodyComp.Position.Y + _bodyComp.GetHitbox().Height))
            {
                return false;
            }
            return true;

        }
    }
}
