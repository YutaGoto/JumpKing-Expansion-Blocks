using HarmonyLib;
using JumpKing.API;
using JumpKing.Player;
using System.Collections.Generic;
using System.Reflection;

namespace JumpKing_Expansion_Blocks.Utils
{
    internal class GetForceFrames
    {
        private static Behaviours.ForceFramesJump _forceFrameJumpBlockBehaviour;

        public static float Frames(PlayerEntity player)
        {
            FieldInfo blockBehavioursField = AccessTools.Field(typeof(BodyComp), "m_blockBehaviours");
            LinkedList<IBlockBehaviour> m_blockBehaviours = (LinkedList<IBlockBehaviour>)blockBehavioursField.GetValue(player.m_body);

            using (LinkedList<IBlockBehaviour>.Enumerator enumerator = m_blockBehaviours.GetEnumerator())
                while (enumerator.MoveNext())
                {
                    IBlockBehaviour current = enumerator.Current;
                    if (current.GetType() == typeof(Behaviours.ForceFramesJump))
                    {
                        _forceFrameJumpBlockBehaviour = (Behaviours.ForceFramesJump)current;

                        if (_forceFrameJumpBlockBehaviour.ForceFrames == 0)
                        {
                            return 0f;
                        }
                        else
                        {
                            return (float)_forceFrameJumpBlockBehaviour.ForceFrames;
                        }
                    }
                }

            return 1f;
        }
    }
}
