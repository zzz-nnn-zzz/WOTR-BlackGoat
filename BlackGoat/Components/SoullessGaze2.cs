using System;
using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ElementsSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using TabletopTweaks.Core.NewEvents;

namespace BlackGoat.Components
{
    #region SoullessGaze2
    public class SoullessGaze2
    {
        private static readonly string FeatName = "SoullessGaze2";
        internal const string DisplayName = "SoullessGaze2.Name";
        private static readonly string Description = "SoullessGaze2.Description";

        public static BlueprintFeature Configure()
        {
            return FeatureConfigurator.New(FeatName, Guids.SoullessGaze2)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.SenseVitals.Reference.Get().Icon)
                .AddComponent<SoullessGaze2Component>()
                .Configure();
        }
        [TypeId("c01de10e-c307-450d-8c78-81bc2fdaacb3")]
        private class SoullessGaze2Component : UnitFactComponentDelegate, IInitiatorDemoralizeHandler
        {
            private static BlueprintBuff _frightened;
            private static BlueprintBuff Frightened
            {
                get
                {
                    _frightened ??= BuffRefs.Frightened.Reference.Get();
                    return _frightened;
                }
            }

            private static BlueprintBuff _cowering;
            private static BlueprintBuff Cowering
            {
                get
                {
                    _cowering ??= BuffRefs.CowerBuff.Reference.Get();
                    return _cowering;
                }
            }

            public void AfterIntimidateSuccess(Demoralize action, RuleSkillCheck intimidateCheck, Buff appliedBuff)
            {
                try
                {
                    var target = ContextData<MechanicsContext.Data>.Current?.CurrentTarget?.Unit;
                    if (target is null)
                    {
                        return;
                    }

                    if (appliedBuff is null)
                    {
                        return;
                    }

                    var caster = Context.MaybeCaster;
                    if (caster is null)
                    {
                        return;
                    }

                    var succeedBy = intimidateCheck.RollResult - intimidateCheck.DC;
                    if (succeedBy < 5)
                    {
                        return;
                    }

                    if (succeedBy >= 50 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 10.Rounds().Seconds);
                    }

                    else if (succeedBy >= 50)
                    {
                        target.AddBuff(Frightened,Context, duration: 10.Rounds().Seconds);
                    }

                    else if (succeedBy >= 45 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 9.Rounds().Seconds);
                    }

                    else if (succeedBy >= 45)
                    {
                        target.AddBuff(Frightened, Context, duration: 9.Rounds().Seconds);
                    }

                    else if (succeedBy >= 40 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 8.Rounds().Seconds);
                    }

                    else if (succeedBy >= 40)
                    {
                        target.AddBuff(Frightened, Context, duration: 8.Rounds().Seconds);
                    }

                    else if (succeedBy >= 35 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 7.Rounds().Seconds);
                    }

                    else if (succeedBy >= 35)
                    {
                        target.AddBuff(Frightened, Context, duration: 7.Rounds().Seconds);
                    }

                    else if (succeedBy >= 30 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 6.Rounds().Seconds);
                    }

                    else if (succeedBy >= 30)
                    {
                        target.AddBuff(Frightened, Context, duration: 6.Rounds().Seconds);
                    }

                    else if (succeedBy >= 25 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 5.Rounds().Seconds);
                    }

                    else if (succeedBy >= 25)
                    {
                        target.AddBuff(Frightened, Context, duration: 5.Rounds().Seconds);
                    }

                    else if (succeedBy >= 20 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 4.Rounds().Seconds);
                    }

                    else if (succeedBy >= 20)
                    {
                        target.AddBuff(Frightened, Context, duration: 4.Rounds().Seconds);
                    }

                    else if (succeedBy >= 15 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 3.Rounds().Seconds);
                    }

                    else if (succeedBy >= 15)
                    {
                        target.AddBuff(Frightened, Context, duration: 3.Rounds().Seconds);
                    }

                    else if (succeedBy >= 10 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 2.Rounds().Seconds);
                    }

                    else if (succeedBy >= 10)
                    {
                        target.AddBuff(Frightened, Context, duration: 2.Rounds().Seconds);
                    }

                    else if (succeedBy >= 5 && target.HasFact(Frightened))
                    {
                        target.AddBuff(Cowering, Context, duration: 1.Rounds().Seconds);
                    }

                    else if (succeedBy >= 5)
                    {
                        target.AddBuff(Frightened, Context, duration: 1.Rounds().Seconds);
                    }
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
    #endregion