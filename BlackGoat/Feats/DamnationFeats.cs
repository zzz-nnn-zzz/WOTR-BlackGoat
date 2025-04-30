using System;
using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using TabletopTweaks.Core.NewEvents;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Enums.Damage;
using Kingmaker.Blueprints.Classes.Spells;

namespace BlackGoat.Feats
{
    internal class DamnationFeats
    {
        private const string DamnationFeat = "Damnation";
        internal const string DamnationName = "Damnation.Name";
        private const string DamnationDescription = "Damnation.Description";

        public static void Configure()
        {
            var icon = FeatureRefs.BloodlineAbyssalDemonicMight.Reference.Get().Icon;

            var feat = FeatureSelectionConfigurator.New(DamnationFeat, Guids.Damnation)
                .SetDisplayName(DamnationName)
                .SetDescription(DamnationDescription)
                .SetIcon(icon)
                .SetIgnorePrerequisites(false)
                .SetObligatory(false)
                .AddToAllFeatures(SoullessGazeFeat())
                .AddToAllFeatures(MaskofVirtueFeat())
                .AddToAllFeatures(FiendskinFeat())
                .AddToAllFeatures(MaleficiumFeat())
                .Configure();

            FeatureSelectionConfigurator.For(FeatureSelectionRefs.BasicFeatSelection)
                .AddToAllFeatures(feat)
                .Configure();

            FeatureSelectionConfigurator.For(FeatureSelectionRefs.DragonLevel2FeatSelection)
                .AddToAllFeatures(feat)
                .Configure();
        }

        #region SoullessGaze
        private const string SoullessGaze = "SoullessGaze";
        internal const string SoullessGazeName = "SoullessGaze.Name";
        private const string SoullessGazeDescription = "SoullessGaze.Description";

        public static BlueprintFeature SoullessGazeFeat()
        {
            var icon = AbilityRefs.Blindness.Reference.Get().Icon;

           return FeatureConfigurator.New(SoullessGaze, Guids.SoullessGaze)
                .SetDisplayName(SoullessGazeName)
                .SetDescription(SoullessGazeDescription)
                .SetIcon(icon)
                .AddFacts(new() { SoullessGaze1Feat() })
                .Configure();
        }

        private const string SoullessGaze1 = "SoullessGaze1";
        internal const string SoullessGaze1Name = "SoullessGaze1.Name";
        private const string SoullessGaze1Description = "SoullessGaze1.Description";
        public static BlueprintFeature SoullessGaze1Feat()
        {
            var icon = AbilityRefs.Hypnotism.Reference.Get().Icon;

            return FeatureConfigurator.New(SoullessGaze1, Guids.SoullessGaze1)
                .SetDisplayName(SoullessGaze1Name)
                .SetDescription(SoullessGaze1Description)
                .SetIcon(icon)
                .AddBuffSkillBonus(
                stat: StatType.CheckIntimidate, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }

        private const string SoullessGaze2 = "SoullessGaze2";
        internal const string SoullessGaze2Name = "SoullessGaze2.Name";
        private const string SoullessGaze2Description = "SoullessGaze2.Description";
        public static BlueprintFeature SoullessGaze2Feat()
        {
            var icon = AbilityRefs.SenseVitals.Reference.Get().Icon;

            return FeatureConfigurator.New(SoullessGaze2, Guids.SoullessGaze2)
                .SetDisplayName(SoullessGaze2Name)
                .SetDescription(SoullessGaze2Description)
                .SetIcon(icon)
                .AddComponent<SoullessGaze2Component>()
                .Configure();
        }

        [TypeId("7B9EB4F8-DB97-400C-B61B-55CE71E87B34")]
        private class SoullessGaze2Component : UnitFactComponentDelegate, IInitiatorDemoralizeHandler
        {

            private static BlueprintBuff _shaken;
            private static BlueprintBuff Shaken
            {
                get
                {
                    _shaken ??= BuffRefs.Shaken.Reference.Get();
                    return _shaken;
                }
            }

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
                        target.AddBuff(Frightened, Context, duration: 10.Rounds().Seconds);
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

                    else if (succeedBy >= 5 && target.HasFact(Shaken))
                    {
                        target.AddBuff(Frightened, Context, duration: 1.Rounds().Seconds);
                    }

                    else if (succeedBy >= 5)
                    {
                        target.AddBuff(Shaken, Context, duration: 1.Rounds().Seconds);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        private const string SoullessGaze3 = "SoullessGaze3";
        internal const string SoullessGaze3Name = "SoullessGaze3.Name";
        private const string SoullessGaze3Description = "SoullessGaze3.Description";
        public static BlueprintFeature SoullessGaze3Feat()
        {
            var icon = AbilityRefs.EyesOfTheBodak.Reference.Get().Icon;

            return FeatureConfigurator.New(SoullessGaze3, Guids.SoullessGaze3)
                .SetDisplayName(SoullessGaze3Name)
                .SetDescription(SoullessGaze3Description)
                .SetIcon(icon)
                .AddBuffSkillBonus(
                stat: StatType.CheckIntimidate, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .Configure();

        }

        #endregion

        #region MaskofVirtue
        private const string MaskofVirtue = "MaskofVirtue";
        internal const string MaskofVirtueName = "MaskofVirtue.Name";
        private const string MaskofVirtueDescription = "MaskofVirtue.Description";
        public static BlueprintFeature MaskofVirtueFeat()
        {
            var icon = AbilityRefs.FalseLife.Reference.Get().Icon;

           return FeatureConfigurator.New(MaskofVirtue, Guids.MaskofVirtue)
                .SetDisplayName(MaskofVirtueName)
                .SetDescription(MaskofVirtueDescription)
                .SetIcon(icon)
                .AddFacts(new() { MaskofVirtue1Feat() })
                .AddFacts(new() { SoullessGaze2Feat() })
                .AddPrerequisiteFeature(Guids.SoullessGaze)
                .Configure();
        }

        private const string MaskofVirtue1 = "MaskofVirtue1";
        internal const string MaskofVirtue1Name = "MaskofVirtue1.Name";
        private const string MaskofVirtue1Description = "MaskofVirtue1.Description";
        public static BlueprintFeature MaskofVirtue1Feat()
        {
            var icon = AbilityRefs.FalseLifeGreater.Reference.Get().Icon;

            return FeatureConfigurator.New(MaskofVirtue1, Guids.MaskofVirtue1)
                .SetDisplayName(MaskofVirtue1Name)
                .SetDescription(MaskofVirtue1Description)
                .SetIcon(icon)
                .AddUndetectableAlignment()
                .Configure();
        }

        #endregion

        #region Fiendskin
        private const string Fiendskin = "Fiendskin";
        internal const string FiendskinName = "Fiendskin.Name";
        private const string FiendskinDescription = "Fiendskin.Description";
        public static BlueprintFeature FiendskinFeat()
        {
            var icon = AbilityRefs.PerfectForm.Reference.Get().Icon;

            return FeatureConfigurator.New(Fiendskin, Guids.Fiendskin)
                .SetDisplayName(FiendskinName)
                .SetDescription(FiendskinDescription)
                .SetIcon(icon)
                .AddFacts(new() { Fiendskin1Feat() })
                .AddFacts(new() { Fiendskin2Feat() })
                .AddFacts(new() { Fiendskin3Feat() })
                .AddFacts(new() { SoullessGaze3Feat() })
                .AddPrerequisiteFeature(Guids.SoullessGaze)
                .AddPrerequisiteFeature(Guids.MaskofVirtue)
                .Configure();
        }

        private const string Fiendskin1 = "Fiendskin1";
        internal const string Fiendskin1Name = "Fiendskin1.Name";
        private const string Fiendskin1Description = "Fiendskin1.Description";
        public static BlueprintFeature Fiendskin1Feat()
        {
            var icon = AbilityRefs.AcidSplash.Reference.Get().Icon;

            return FeatureConfigurator.New(Fiendskin1, Guids.Fiendskin1)
                .SetDisplayName(Fiendskin1Name)
                .SetDescription(Fiendskin1Description)
                .SetIcon(icon)
                .AddDamageResistanceEnergy(type: DamageEnergyType.Acid, value: 5)
                .Configure();
        }

        private const string Fiendskin2 = "Fiendskin2";
        internal const string Fiendskin2Name = "Fiendskin2.Name";
        private const string Fiendskin2Description = "Fiendskin2.Description";
        public static BlueprintFeature Fiendskin2Feat()
        {
            var icon = AbilityRefs.AcidArrow.Reference.Get().Icon;

            return FeatureConfigurator.New(Fiendskin2, Guids.Fiendskin2)
                .SetDisplayName(Fiendskin2Name)
                .SetDescription(Fiendskin2Description)
                .SetIcon(icon)
                .AddDamageResistanceEnergy(type: DamageEnergyType.Acid, value: 10)
                .Configure();
        }

        private const string Fiendskin3 = "Fiendskin3";
        internal const string Fiendskin3Name = "Fiendskin3.Name";
        private const string Fiendskin3Description = "Fiendskin3.Description";
        public static BlueprintFeature Fiendskin3Feat()
        {
            var icon = AbilityRefs.OracleRevelationIceArmorAbility.Reference.Get().Icon;

            return FeatureConfigurator.New(Fiendskin3, Guids.Fiendskin3)
                .SetDisplayName(Fiendskin3Name)
                .SetDescription(Fiendskin3Description)
                .SetIcon(icon)
                .AddEnergyDamageImmunity(DamageEnergyType.Cold)
                .Configure();
        }

        private const string Fiendskin4 = "Fiendskin4";
        internal const string Fiendskin4Name = "Fiendskin4.Name";
        private const string Fiendskin4Description = "Fiendskin4.Description";
        public static BlueprintFeature Fiendskin4Feat()
        {
            var icon = AbilityRefs.OracleRevelationArmorOfBonesAbility.Reference.Get().Icon;

            return FeatureConfigurator.New(Fiendskin4, Guids.Fiendskin4)
                .SetDisplayName(Fiendskin4Name)
                .SetDescription(Fiendskin4Description)
                .SetIcon(icon)
                .AddEnergyDamageImmunity(DamageEnergyType.Acid)
                .AddFacts(new() { FeatureRefs.OutsiderType.ToString() })
                .Configure();
        }
        #endregion

        #region Maleficium
        private const string Maleficium = "Maleficium";
        internal const string MaleficiumName = "Maleficium.Name";
        private const string MaleficiumDescription = "Maleficium.Description";
        public static BlueprintFeature MaleficiumFeat()
        {
            var icon = AbilityRefs.BestowCurse.Reference.Get().Icon;

            return FeatureConfigurator.New(Maleficium, Guids.Maleficium)
                .SetDisplayName(MaleficiumName)
                .SetDescription(MaleficiumDescription)
                .SetIcon(icon)
                //
                .AddFacts(new() { Fiendskin4Feat() })
                //
                .AddFacts(new() { Maleficium1Feat() })
                .AddFacts(new() { Maleficium2Feat() })
                .AddFacts(new() { Maleficium3Feat() })
                .AddFacts(new() { Maleficium4Feat() })
                .AddPrerequisiteFeature(Guids.SoullessGaze)
                .AddPrerequisiteFeature(Guids.MaskofVirtue)
                .AddPrerequisiteFeature(Guids.Fiendskin)
                .Configure();
        }

        private const string Maleficium1 = "Maleficium1";
        internal const string Maleficium1Name = "Maleficium1.Name";
        private const string Maleficium1Description = "Maleficium1.Description";
        public static BlueprintFeature Maleficium1Feat()
        {
            var icon = AbilityRefs.BestowCurse.Reference.Get().Icon;

            return FeatureConfigurator.New(Maleficium1, Guids.Maleficium1)
                .SetDisplayName(Maleficium1Name)
                .SetDescription(Maleficium1Description)
                .SetIcon(icon)
                .AddIncreaseSpellDescriptorDC(1, SpellDescriptor.Evil, modifierDescriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }

        private const string Maleficium2 = "Maleficium2";
        internal const string Maleficium2Name = "Maleficium2.Name";
        private const string Maleficium2Description = "Maleficium2.Description";
        public static BlueprintFeature Maleficium2Feat()
        {
            var icon = AbilityRefs.BestowCurse.Reference.Get().Icon;

            return FeatureConfigurator.New(Maleficium2, Guids.Maleficium2)
                .SetDisplayName(Maleficium2Name)
                .SetDescription(Maleficium2Description)
                .SetIcon(icon)
                //TODO
                .Configure();
        }

        private const string Maleficium3 = "Maleficium3";
        internal const string Maleficium3Name = "Maleficium3.Name";
        private const string Maleficium3Description = "Maleficium3.Description";
        public static BlueprintFeature Maleficium3Feat()
        {
            var icon = AbilityRefs.BestowCurse.Reference.Get().Icon;

            return FeatureConfigurator.New(Maleficium3, Guids.Maleficium3)
                .SetDisplayName(Maleficium3Name)
                .SetDescription(Maleficium3Description)
                .SetIcon(icon)
                .AddIncreaseSpellDescriptorDC(1, SpellDescriptor.Evil, modifierDescriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }

        private const string Maleficium4 = "Maleficium4";
        internal const string Maleficium4Name = "Maleficium4.Name";
        private const string Maleficium4Description = "Maleficium4.Description";
        public static BlueprintFeature Maleficium4Feat()
        {
            var icon = AbilityRefs.BestowCurseGreater.Reference.Get().Icon;

            return FeatureConfigurator.New(Maleficium4, Guids.Maleficium4)
                .SetDisplayName(Maleficium4Name)
                .SetDescription(Maleficium4Description)
                .SetIcon(icon)
                .AddIncreaseSpellDescriptorCasterLevel(2, SpellDescriptor.Evil, modifierDescriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }
        #endregion

    }
}