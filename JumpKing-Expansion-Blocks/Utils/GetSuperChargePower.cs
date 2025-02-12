using HarmonyLib;
using JumpKing.API;
using JumpKing.Player;
using System.Collections.Generic;
using System.Reflection;

namespace JumpKing_Expansion_Blocks.Utils
{
    internal class GetSuperChargePower
    {
        private static Behaviours.SuperCharge _superChargeBlockBehaviour;

        public static float Power(PlayerEntity player)
        {
            FieldInfo blockBehavioursField = AccessTools.Field(typeof(BodyComp), "m_blockBehaviours");
            LinkedList<IBlockBehaviour> m_blockBehaviours = (LinkedList<IBlockBehaviour>)blockBehavioursField.GetValue(player.m_body);

            using (LinkedList<IBlockBehaviour>.Enumerator enumerator = m_blockBehaviours.GetEnumerator())
                while (enumerator.MoveNext())
                {
                    IBlockBehaviour current = enumerator.Current;
                    if (current.GetType() == typeof(Behaviours.SuperCharge))
                    {
                        _superChargeBlockBehaviour = (Behaviours.SuperCharge)current;

                        if (_superChargeBlockBehaviour.SuperChargeRatio == 0)
                        {
                            return 0.36f;
                        }
                        else
                        {
                            return _superChargeBlockBehaviour.SuperChargeRatio;
                        }
                    }
                }

            return 1f;
        }
    }
}
