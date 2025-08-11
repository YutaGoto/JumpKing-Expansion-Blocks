using JumpKing.API;
using JumpKing.BodyCompBehaviours;
using JumpKing.Level;
using JumpKing.Player;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using JumpKing_Expansion_Blocks;

namespace JumpKing_Expansion_Blocks.Behaviours
{
    internal class JkqPlatform : IBlockBehaviour
    {
        private static ulong? cachedLevelID = null;

        public float BlockPriority => 2f;

        public bool IsPlayerOnBlock { get; set; }
        internal static bool isThroughPlatform { get; set; } = false;
        private static Form formType { get; set; }

        static JkqPlatform()
        {
            // LevelID取得
            ulong levelID = 0UL;
            try
            {
                levelID = (ulong)typeof(ModEntry).GetField("levelID", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)?.GetValue(null);
            }
            catch { }

            cachedLevelID = levelID;

            string xmlPath = Path.Combine(ModEntry.AssemblyPath ?? "", "YutaGoto.JumpKing_Expansion_Blocks.JkqPlatform.xml");
            if (File.Exists(xmlPath))
            {
                try
                {
                    var doc = XDocument.Load(xmlPath);
                    var root = doc.Element("JkqPlatform");
                    var levelIdElem = root?.Element("LevelID");
                    var isThroughElem = root?.Element("IsThroughPlatform");
                    if (levelIdElem != null && isThroughElem != null && ulong.TryParse(levelIdElem.Value, out ulong fileLevelId))
                    {
                        if (fileLevelId == levelID)
                        {
                            isThroughPlatform = bool.TryParse(isThroughElem.Value, out bool val) ? val : false;
                            return;
                        }
                    }
                }
                catch { }
            }
            isThroughPlatform = false;
        }

        public bool AdditionalXCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info != null && behaviourContext != null)
            {
                BodyComp bodyComp = behaviourContext.BodyComp;

                if (
                    info.IsCollidingWith<Blocks.JkqPlatform>()
                    && !behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>()
                    && !isThroughPlatform
                )
                {
                    GetForm(behaviourContext);
                    switch (formType)
                    {
                        case Form.Platform:
                            return false;
                        case Form.RightWall:
                            return bodyComp.Velocity.X <= 0f;
                        case Form.LeftWall:
                            return bodyComp.Velocity.X >= 0f;
                        default:
                            return true;
                    }
                }
            }

            return false;
        }

        public bool AdditionalYCollisionCheck(AdvCollisionInfo info, BehaviourContext behaviourContext)
        {
            if (info != null && behaviourContext != null)
            {
                BodyComp bodyComp = behaviourContext.BodyComp;

                if (
                    info.IsCollidingWith<Blocks.JkqPlatform>()
                    && !behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>()
                    && !isThroughPlatform
                )
                {
                    GetForm(behaviourContext);
                    switch (formType)
                    {
                        case Form.Ceil:
                            return bodyComp.Velocity.Y <= 0.0f;
                        default:
                            if (bodyComp.IsOnGround) return true;

                            return bodyComp.Velocity.Y >= 0.0f;
                    }
                }
            }

            return false;
        }

        public bool ExecuteBlockBehaviour(BehaviourContext behaviourContext)
        {
            BodyComp bodyComp = behaviourContext.BodyComp;

            if (behaviourContext.CollisionInfo?.StartOfFrameCollisionInfo != null)
            {
                IsPlayerOnBlock = behaviourContext.CollisionInfo.StartOfFrameCollisionInfo.IsCollidingWith<Blocks.JkqPlatform>();
            }

            bool prevIsThroughPlatform = isThroughPlatform;

            if (bodyComp.IsOnGround)
            {
                isThroughPlatform = false;
            }

            isThroughPlatform = isThroughPlatform || IsPlayerOnBlock;

            // LevelID取得（キャッシュ）
            if (!cachedLevelID.HasValue)
            {
                try
                {
                    cachedLevelID = (ulong)typeof(ModEntry).GetField("levelID", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)?.GetValue(null);
                }
                catch { cachedLevelID = null; }
            }

            // isThroughPlatformが切り替わった場合のみ保存
            if (prevIsThroughPlatform != isThroughPlatform)
            {
                SaveIsThroughPlatformToXml(cachedLevelID ?? 0UL, isThroughPlatform);
            }

            return true;
        }

        private void SaveIsThroughPlatformToXml(ulong levelID, bool isThrough)
        {
            string dir = Path.Combine(ModEntry.AssemblyPath ?? "", "YutaGoto.JumpKing_Expansion_Blocks.JkqPlatform.xml");
            var doc = new XDocument(
                new XElement("JkqPlatform",
                    new XElement("LevelID", levelID),
                    new XElement("IsThroughPlatform", isThrough)
                )
            );
            doc.Save(dir);
        }

        public float ModifyGravity(float inputGravity, BehaviourContext behaviourContext)
        {
            return inputGravity;
        }

        public float ModifyXVelocity(float inputXVelocity, BehaviourContext behaviourContext)
        {
            return inputXVelocity;
        }

        public float ModifyYVelocity(float inputYVelocity, BehaviourContext behaviourContext)
        {
            return inputYVelocity;
        }

        private void GetForm(BehaviourContext behaviourContext)
        {
            if (behaviourContext?.CollisionInfo?.PreResolutionCollisionInfo != null)
            {
                Blocks.JkqPlatform jkqPlatform = behaviourContext.CollisionInfo.PreResolutionCollisionInfo.GetCollidedBlocks<Blocks.JkqPlatform>().FirstOrDefault() as Blocks.JkqPlatform;
                switch (jkqPlatform.Form)
                {
                    case Constants.JkqPlatformsCodes.PLATFORM:
                        formType = Form.Platform;
                        return;
                    case Constants.JkqPlatformsCodes.RIGHT_WALL:
                        formType = Form.RightWall;
                        return;
                    case Constants.JkqPlatformsCodes.LEFT_WALL:
                        formType = Form.LeftWall;
                        return;
                    case Constants.JkqPlatformsCodes.BOTH_WALL:
                        formType = Form.BothWall;
                        return;
                    case Constants.JkqPlatformsCodes.CEIL:
                        formType = Form.Ceil;
                        return;
                    default:
                        formType = Form.None;
                        return;
                }
            }
        }
    }

    internal enum Form
    {
        Platform,
        RightWall,
        LeftWall,
        BothWall,
        Ceil,
        None
    }
}
