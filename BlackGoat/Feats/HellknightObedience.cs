using System.Collections.Generic;
using BlackGoat.Utils;
using BlackGoat.Components;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.BasicEx;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using TabletopTweaks.Base.NewContent.Spells;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace BlackGoat.Feats
{
    internal class HellknightObedience
    {
        private const string HellknightObedienceFeat = "HellknightObedience";
        internal const string HellknightObedienceDisplayName = "HellknightObedience.Name";
        private const string HellknightObedienceDescription = "HellknightObedience.Description";

        private const string HellknightObedienceAbility = "HellknightObedienceAbility";
        private const string HellknightObedienceAbilityResource = "HellknightObedienceAbilityResource";

        public static void Configure()
        {
            var icon = AbilityRefs.Prayer.Reference.Get().Icon;

            var resource = AbilityResourceConfigurator.New(HellknightObedienceAbilityResource, Guids.HellknightObedienceAbilityResource)
                .SetMaxAmount(ResourceAmountBuilder.New(6))
                .Configure();

            AbilityConfigurator.New(HellknightObedienceAbility, Guids.HellknightObedienceAbility)
                .CopyFrom(
                AbilityRefs.CommunityDomainGreaterAbility, typeof(AbilitySpawnFx))
                .SetDisplayName(HellknightObedienceDisplayName)
                .SetDescription(HellknightObedienceDescription)
                .SetIcon(icon)
                .SetType(AbilityType.Special)
                .AllowTargeting(self: true)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                      .Conditional(conditions: ConditionsBuilder.New().Build(),
                      ifTrue: ActionsBuilder.New()
                              .DealDamage(value: ContextDice.Value(DiceType.D4, bonus: 0, diceCount: ContextValues.Constant(1)), damageType: DamageTypes.Direct())
                              .Build())
                      .Build())
                .Configure();

            var feat = FeatureSelectionConfigurator.New(HellknightObedienceFeat, Guids.HellknightObedience)
                .SetDisplayName(HellknightObedienceDisplayName)
                .SetDescription(HellknightObedienceDescription)
                .SetIcon(icon)
                .SetIgnorePrerequisites(false)
                .SetObligatory(false)
                .AddToAllFeatures(PyreFeat())
                .AddToAllFeatures(NailFeat())
                .AddToAllFeatures(GodclawFeat())
                .AddPrerequisiteNoFeature(Guids.HellknightObedience)
                .AddPrerequisiteStatValue(StatType.SkillKnowledgeArcana, 3)
                .AddPrerequisiteAlignment(AlignmentMaskType.Lawful)
                .AddAbilityResources(resource: resource, restoreAmount: true)
                .AddRestTrigger(ActionsBuilder.New().CastSpell(Guids.HellknightObedienceAbility).Build())
                .Configure();

            FeatureSelectionConfigurator.For(FeatureSelectionRefs.BasicFeatSelection)
                .AddToAllFeatures(feat)
                .Configure();

            FeatureSelectionConfigurator.For(FeatureSelectionRefs.DragonLevel2FeatSelection)
                .AddToAllFeatures(feat)
                .Configure();
        }

        #region Pyre

        private const string Pyre = "PyreObedience";
        internal const string PyreDisplayName = "PyreObedience.Name";
        private const string PyreDescription = "PyreObedience.Description";

        private const string PyreObedienceBuff = "PyreObedienceBuff";
        private const string PyreObedienceAbility = "PyreObedienceAbility";
        public static BlueprintFeature PyreFeat()
        {
            var icon = FeatureRefs.FavoredEnemySpellcasting.Reference.Get().Icon;

            var buff = BuffConfigurator.New(PyreObedienceBuff, Guids.PyreObedienceBuff)
                .SetDisplayName(PyreDisplayName)
                .SetDescription(PyreDescription)
                .SetIcon(icon)
                .AddDamageResistanceEnergy(type: Kingmaker.Enums.Damage.DamageEnergyType.Fire, value: ContextValues.Constant(10))
                .AddUniqueBuff()
                .Configure();

            var ability = AbilityConfigurator.New(PyreObedienceAbility, Guids.PyreObedienceAbility)
                .SetDisplayName(PyreDisplayName)
                .SetDescription(PyreDescription)
                .SetIcon(icon)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                       .ApplyBuffWithDurationSeconds(buff, 600)
                       .Build())
                .SetActionType(CommandType.Free)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch)
                .SetRange(AbilityRange.Personal)
                .SetType(AbilityType.SpellLike)
                //add better animation
                .AddAbilityResourceLogic(6, isSpendResource: true, requiredResource: Guids.HellknightObedienceAbilityResource)
                .Configure();

            List<BlueprintCore.Utils.Blueprint<Kingmaker.Blueprints.BlueprintUnitFactReference>> profList = new List<BlueprintCore.Utils.Blueprint<Kingmaker.Blueprints.BlueprintUnitFactReference>>();
            profList.Add(FeatureRefs.GlaiveProficiency.ToString());

            var proficiencyList = new[] { WeaponCategory.Glaive };

            return ProgressionConfigurator.New(Pyre, Guids.PyreObedience)
                .SetDisplayName(PyreDisplayName)
                .SetDescription(PyreDescription)
                .SetIcon(icon)
                .AddProficiencies(weaponProficiencies: proficiencyList)
                .AddFacts(profList)
                .AddBackgroundWeaponProficiency(WeaponCategory.Glaive, stackBonusType: ModifierDescriptor.Trait, stackBonus: ContextValues.Constant(1))
                .SetGiveFeaturesForPreviousLevels(true)
                .AddToLevelEntry(12, PyreBoons1Feat())
                .AddToLevelEntry(16, PyreBoons2Feat())
                .AddToLevelEntry(20, PyreBoons3Feat())
                .AddFacts(new() { ability })
                .Configure();
        }

        private const string PyreBoons1 = "PyreBoons1";
        internal const string PyreBoons1Name = "PyreBoons1.Name";
        private const string PyreBoons1Description = "PyreBoons1.Description";
        private static BlueprintFeature PyreBoons1Feat()
        {
            var icon = FeatureRefs.SneakAttack.Reference.Get().Icon;

            return FeatureConfigurator.New(PyreBoons1, Guids.PyreBoons1)
                .SetDisplayName(PyreBoons1Name)
                .SetDescription(PyreBoons1Description)
                .SetIcon(icon)
                .AddBuffSkillBonus(stat: StatType.CheckIntimidate, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .AddBuffSkillBonus(stat: StatType.SkillKnowledgeArcana, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .AddBuffSkillBonus(stat: StatType.SkillPerception, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .AddBuffSkillBonus(stat: StatType.SkillLoreNature, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .AddStatBonus(stat: StatType.AdditionalAttackBonus, value: 1, descriptor: ModifierDescriptor.UntypedStackable)
                .AddStatBonus(stat: StatType.AdditionalDamage, value: 1, descriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }

        private const string PyreBoons2 = "PyreBoons2 ";
        internal const string PyreBoons2Name = "PyreBoons2.Name";
        private const string PyreBoons2Description = "PyreBoons2.Description";

        private const string PyreBoons2Ability = "PyreBoons2ABility";
        private const string PyreBoons2AbilityResource = "PyreBoons2AbilityResource";
        private static BlueprintFeature PyreBoons2Feat()
        {
            var icon = AbilityRefs.Fireball.Reference.Get().Icon;

            var abilityresource = AbilityResourceConfigurator.New(PyreBoons2AbilityResource, Guids.PyreBoons2AbilityResource)
                .SetMaxAmount(
                    ResourceAmountBuilder.New(1))
                .Configure();

            var ability = AbilityConfigurator.New(PyreBoons2Ability, Guids.PyreBoons2Ability)
                .CopyFrom(
                AbilityRefs.Fireball,
                typeof(AbilityEffectRunAction),
                typeof(SpellComponent),
                typeof(AbilityDeliverProjectile),
                typeof(ContextRankConfig),
                typeof(SpellDescriptorComponent),
                typeof(AbilityTargetsAround))
                .SetIcon(icon)
                .SetType(AbilityType.SpellLike)
                .AddAbilityResourceLogic(1, isSpendResource: true, requiredResource: Guids.PyreBoons2AbilityResource)
                .Configure();

            return FeatureConfigurator.New(PyreBoons2, Guids.PyreBoons2)
                .SetDisplayName(PyreBoons2Name)
                .SetDescription(PyreBoons2Description)
                .SetIcon(icon)
                .AddFacts(new() { ability })
                .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                .Configure();
        }

        private const string PyreBoons3 = "PyreBoons3";
        internal const string PyreBoons3Name = "PyreBoons3.Name";
        private const string PyreBoons3Description = "PyreBoons3.Description";

        private const string PyreBoons3Buff = "PyreBoons3Buff";
        private const string PyreBoons3Ability = "PyreBoons3Ability";
        private const string PyreBoons3AbilityResource = "PyreBoons3AbilityResource";

        private static BlueprintFeature PyreBoons3Feat()
        {
            var icon = AbilityRefs.OverwhelmingPresence.Reference.Get().Icon;

            var abilityresource = AbilityResourceConfigurator.New(PyreBoons3AbilityResource, Guids.PyreBoons3AbilityResource)
                .SetMaxAmount(
                    ResourceAmountBuilder.New(1))
                .Configure();

            var buff = BuffConfigurator.New(PyreBoons3Buff, Guids.PyreBoons3Buff)
                .SetDisplayName(PyreBoons3Name)
                .SetDescription(PyreBoons3Description)
                .SetIcon(icon)
                .AddCondition(Kingmaker.UnitLogic.UnitCondition.SpellcastingForbidden)
                .Configure();

            var ability = AbilityConfigurator.New(PyreBoons3Ability, Guids.PyreBoons3Ability)
                .SetDisplayName(PyreBoons3Name)
                .SetDescription(PyreBoons3Description)
                .SetIcon(icon)
                .SetType(AbilityType.Supernatural)
                .SetRange(AbilityRange.Long)
                .AllowTargeting(true, true, true, true)
                .SetActionType(CommandType.Standard)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .AddAbilityResourceLogic(1, isSpendResource: true, requiredResource: Guids.PyreBoons3AbilityResource)
                .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                 .ConditionalSaved(failed: ActionsBuilder.New()
                      .ApplyBuff(buff, ContextDuration.Fixed(10))
                      .Build())
                 .Build(), savingThrowType: SavingThrowType.Will)
                .Configure();

            return FeatureConfigurator.New(PyreBoons3, Guids.PyreBoons3)
                .SetDisplayName(PyreBoons3Name)
                .SetDescription(PyreBoons3Description)
                .SetIcon(icon)
                .AddFacts(new() { ability })
                .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                .Configure();

        }

        #endregion

        #region Nail

        private const string Nail = "NailObedience";
        internal const string NailDisplayName = "NailObedience.Name";
        private const string NailDescription = "NailObedience.Description";

        public static BlueprintFeature NailFeat()
        {
            var icon = FeatureRefs.FavoriteEnemyDemonOfStrength.Reference.Get().Icon;

            List<BlueprintCore.Utils.Blueprint<Kingmaker.Blueprints.BlueprintUnitFactReference>> profList = new List<BlueprintCore.Utils.Blueprint<Kingmaker.Blueprints.BlueprintUnitFactReference>>();
            profList.Add(FeatureRefs.FauchardProficiency.ToString());

            var proficiencyList = new[] { WeaponCategory.Fauchard };

            return ProgressionConfigurator.New(Nail, Guids.NailObedience)
                .SetDisplayName(NailDisplayName)
                .SetDescription(NailDescription)
                .SetIcon(icon)
                .AddProficiencies(weaponProficiencies: proficiencyList)
                .AddFacts(profList)
                .AddBackgroundWeaponProficiency(WeaponCategory.Fauchard, stackBonusType: ModifierDescriptor.Trait, stackBonus: ContextValues.Constant(1))
                .AddBuffSkillBonus(stat: StatType.SkillKnowledgeWorld, value: 4, descriptor: ModifierDescriptor.UntypedStackable)
                .SetGiveFeaturesForPreviousLevels(true)
                .AddToLevelEntry(12, NailBoons1Feat())
                .AddToLevelEntry(16, NailBoons2Feat())
                .AddToLevelEntry(20, NailBoons3Feat())
                .Configure();
        }

        private const string NailBoons1 = "NailBoons1";
        internal const string NailBoons1Name = "NailBoons1.Name";
        private const string NailBoons1Description = "NailBoons1.Description";

        private static BlueprintFeature NailBoons1Feat()
        {

            var icon = FeatureRefs.RangerFavoredEnemy.Reference.Get().Icon;

            return FeatureSelectionConfigurator.New(NailBoons1, Guids.NailBoons1)
                .CopyFrom(
                FeatureSelectionRefs.FavoriteEnemySelection.Reference.Get())
                .SetDisplayName(NailBoons1Name)
                .SetDescription(NailBoons1Description)
                .SetIcon(icon)
                .Configure();
        }

        private const string NailBoons2 = "NailBoons2";
        internal const string NailBoons2Name = "NailBoons2.Name";
        private const string NailBoons2Description = "NailBoons2.Description";

        private const string NailBoons2Buff = "NailBoons2Buff";
        private const string NailBoons2Ability = "NailBoons2Ability";
        private const string NailBoons2AbilityResource = "NailBoons2AbilityResource";

        private static BlueprintFeature NailBoons2Feat()
        {
            var icon = AbilityRefs.TrueSeeing.Reference.Get().Icon;

            var buff = BuffConfigurator.New(NailBoons2Buff, Guids.NailBoons2Buff)
                .SetDisplayName(NailBoons2Name)
                .SetDescription(NailBoons2Description)
                .SetIcon(icon)
                .AddStatBonus(stat: StatType.SaveWill, value:-2)
                .AddStatBonus(stat: StatType.SaveReflex, value: -2)
                .AddStatBonus(stat: StatType.SaveFortitude, value: -2)
                .AddStatBonus(stat: StatType.AdditionalAttackBonus, value: -2)
                .AddStatBonus(stat: StatType.AdditionalDamage, value: -2)
                .Configure();

            var abilityresource = AbilityResourceConfigurator.New(NailBoons2AbilityResource, Guids.NailBoons2AbilityResource)
                .SetMaxAmount(
                    ResourceAmountBuilder.New(3))
                .Configure();

            var ability = AbilityConfigurator.New(NailBoons2Ability, Guids.NailBoons2Ability)
                .SetDisplayName(NailBoons2Name)
                .SetDescription(NailBoons2Description)
                .SetIcon(icon)
                .SetType(AbilityType.Supernatural)
                .SetRange(AbilityRange.Long)
                .AllowTargeting(true, true, true, true)
                .SetActionType(CommandType.Swift)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .AddAbilityResourceLogic(1, isSpendResource: true, requiredResource: Guids.NailBoons2AbilityResource)
                .AddAbilityEffectRunAction(
                actions: ActionsBuilder.New()
                 .ConditionalSaved(failed: ActionsBuilder.New()
                      .ApplyBuff(buff, ContextDuration.Fixed(3600))
                      .Build())
                 .Build(), savingThrowType: SavingThrowType.Will)
                .Configure();

            return FeatureConfigurator.New(NailBoons2, Guids.NailBoons2)
                .SetDisplayName(NailBoons2Name)
                .SetDescription(NailBoons2Description)
                .SetIcon(icon)
                .AddFacts(new() { ability })
                .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                .Configure();

        }

        private const string NailBoons3 = "NailBoons3";
        internal const string NailBoons3Name = "NailBoons3.Name";
        private const string NailBoons3Description = "NailBoons3.Description";

        private const string NailBoons3Ability = "NailBoons3Ability";
        private const string NailBoons3AbilityResource = "NailBoons3AbilityResource";

        private static BlueprintFeature NailBoons3Feat()
        {
            var icon = AbilityRefs.CorruptMagic.Reference.Get().Icon;

            var ability = AbilityConfigurator.New(NailBoons3Ability, Guids.NailBoons3Ability)
                .CopyFrom(
                "85f616bb-9d05-4f66-9f35-7636c6c06338",
                typeof(AbilityEffectRunAction),
                typeof(SpellComponent),
                typeof(ContextRankConfig),
                typeof(SpellDescriptorComponent),
                typeof(AbilityTargetsAround))
                .SetIcon(icon)
                .SetType(AbilityType.SpellLike)
                .AddPretendSpellLevel(spellLevel: 20)
                .AddAbilityResourceLogic(1, isSpendResource: true, requiredResource: Guids.NailBoons3AbilityResource)
                .Configure();

            var abilityresource = AbilityResourceConfigurator.New(NailBoons3AbilityResource, Guids.NailBoons3AbilityResource)
                .SetMaxAmount(
                    ResourceAmountBuilder.New(1))
                .Configure();

            return FeatureConfigurator.New(NailBoons3, Guids.NailBoons3)
                .SetDisplayName(NailBoons3Name)
                .SetDescription(NailBoons3Description)
                .SetIcon(icon)
                .AddFacts(new() { ability })
                .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                .Configure();
        }

        #endregion


        #region Godclaw

        private const string Godclaw = "GodclawObedience";
        internal const string GodclawDisplayName = "GodclawObedience.Name";
        private const string GodclawDescription = "GodclawObedience.Description";

        public static BlueprintFeature GodclawFeat()
        {
            var icon = FeatureRefs.GodclawFeature.Reference.Get().Icon;

            List<BlueprintCore.Utils.Blueprint<Kingmaker.Blueprints.BlueprintUnitFactReference>> profList = new List<BlueprintCore.Utils.Blueprint<Kingmaker.Blueprints.BlueprintUnitFactReference>>();
            profList.Add(FeatureRefs.HeavyMaceProficiency.ToString());

            var proficiencyList = new[] { WeaponCategory.HeavyMace };

            return ProgressionConfigurator.New(Godclaw, Guids.GodclawObedience)
                .SetDisplayName(GodclawDisplayName)
                .SetDescription(GodclawDescription)
                .SetIcon(icon)
                .AddProficiencies(weaponProficiencies: proficiencyList)
                .AddFacts(profList)
                .AddBackgroundWeaponProficiency(WeaponCategory.HeavyMace, stackBonusType: ModifierDescriptor.Trait, stackBonus: ContextValues.Constant(1))
                .AddComponent<GodclawBonusHealingPerDie>()
                .SetGiveFeaturesForPreviousLevels(true)
                .AddToLevelEntry(12, GodclawBoons1Feat())
                .AddToLevelEntry(16, GodclawBoons2Feat())
                .AddToLevelEntry(20, GodclawBoons3Feat())
                .Configure();
        }

        private const string GodclawBoons1 = "GodclawBoons1";
        internal const string GodclawBoons1Name = "GodclawBoons1.Name";
        private const string GodclawBoons1Description = "GodclawBoons1.Description";
      
        private const string GodclawBoons1Ability1 = "GodclawBoons1Ability1";
        private const string GodclawBoons1Ability2 = "GodclawBoons1Ability2";
        private const string GodclawBoons1Ability3 = "GodclawBoons1Ability3";
        private const string GodclawBoons1AbilityResource = "GodclawBoons1AbilityResource";

        private static BlueprintFeature GodclawBoons1Feat()
        {
            var icon = AbilityRefs.Command.Reference.Get().Icon;

            var abilityresource = AbilityResourceConfigurator.New(GodclawBoons1AbilityResource, Guids.GodclawBoons1AbilityResource)
                .SetMaxAmount(
                    ResourceAmountBuilder.New(6))
                .Configure();

            var ability1 = AbilityConfigurator.New(GodclawBoons1Ability1, Guids.GodclawBoons1Ability1)
                .CopyFrom(
                AbilityRefs.Bane,
                typeof(AbilityEffectRunAction),
                typeof(SpellComponent),
                typeof(SpellDescriptorComponent),
                typeof(AbilityTargetsAround),
                typeof(AbilitySpawnFx))
                .AddAbilityResourceLogic(2, isSpendResource: true, requiredResource: Guids.GodclawBoons1AbilityResource)
                .SetType(AbilityType.SpellLike)
                .Configure();

            var ability2 = AbilityConfigurator.New(GodclawBoons1Ability2, Guids.GodclawBoons1Ability2)
                .CopyFrom(
                AbilityRefs.Bless,
                typeof(AbilityEffectRunAction),
                typeof(SpellComponent),
                typeof(SpellDescriptorComponent),
                typeof(AbilityTargetsAround),
                typeof(AbilitySpawnFx))
                .AddAbilityResourceLogic(2, isSpendResource: true, requiredResource: Guids.GodclawBoons1AbilityResource)
                .SetType(AbilityType.SpellLike)
                .Configure();

            //ability3

            return FeatureConfigurator.New(GodclawBoons1, Guids.GodclawBoons1)
                   .SetDisplayName(GodclawBoons1Name)
                   .SetDescription(GodclawBoons1Description)
                   .SetIcon(icon)
                   .AddFacts(new() { ability1 })
                   .AddFacts(new() { ability2 })
                   .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                   .Configure();

        }

        private const string GodclawBoons2 = "GodclawBoons2";
        internal const string GodclawBoons2Name = "GodclawBoons2.Name";
        private const string GodclawBoons2Description = "GodclawBoons2.Description";

        private const string GodclawBoons2Buff = "GodclawBoons2Buff";
        private const string GodclawBoons2Ability = "GodclawBoons2Ability";
        private const string GodclawBoons2AbilityResource = "GodclawBoons2AbilityResource";

        private static BlueprintFeature GodclawBoons2Feat()
        {
            var icon = AbilityRefs.HealMass.Reference.Get().Icon;

            var buff = BuffConfigurator.New(GodclawBoons2Buff, Guids.GodclawBoons2Buff)
                .SetDisplayName(GodclawBoons2Name)
                .SetDescription(GodclawBoons2Description)
                .SetIcon(icon)
                .AddStatBonus(stat: StatType.AdditionalAttackBonus, value: 3, descriptor: ModifierDescriptor.Sacred)
                .AddCriticalConfirmationBonus(value: 6)
                .Configure();

            var abilityresource = AbilityResourceConfigurator.New(GodclawBoons2AbilityResource, Guids.GodclawBoons2AbilityResource)
                .SetMaxAmount(
                ResourceAmountBuilder.New(3))
                .Configure();


            var ability = ActivatableAbilityConfigurator.New(GodclawBoons2Ability, Guids.GodclawBoons2Ability)
                .CopyFrom(
                ActivatableAbilityRefs.JusticeJudgmentAbility)
                .SetDisplayName(GodclawBoons2Name)
                .SetDescription(GodclawBoons2Description)
                .SetIcon(icon)
                .SetBuff(buff)
                .AddActivatableAbilityResourceLogic(requiredResource: abilityresource, spendType: ActivatableAbilityResourceLogic.ResourceSpendType.Start)
                .Configure();

            return FeatureConfigurator.New(GodclawBoons2, Guids.GodclawBoons2)
                .SetDisplayName(GodclawBoons2Name)
                .SetDescription(GodclawBoons2Description)
                .SetIcon(icon)
                .AddFacts(new() { ability})
                .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                .Configure();

        }

        private const string GodclawBoons3 = "GodclawBoons3";
        internal const string GodclawBoons3Name = "GodclawBoons3.Name";
        private const string GodclawBoons3Description = "GodclawBoons3.Description";

        private const string GodclawBoons3Buff = "GodclawBoons3Buff";
        private const string GodclawBoons3Ability = "GodclawBoons3Ability";
        private const string GodclawBoons3AbilityResource = "GodclawBoons3AbilityResource";

        private static BlueprintFeature GodclawBoons3Feat()
        {
            var icon = AbilityRefs.DivinePower.Reference.Get().Icon;

            var buff = BuffConfigurator.New(GodclawBoons3Buff, Guids.GodclawBoons3Buff)
                .SetDisplayName(GodclawBoons3Name)
                .SetDescription(GodclawBoons3Description)
                .SetIcon(icon)
                .Configure();

            var abilityresource = AbilityResourceConfigurator.New(GodclawBoons3AbilityResource, Guids.GodclawBoons3AbilityResource)
                .SetMaxAmount(
                ResourceAmountBuilder.New(1))
                .Configure();

            var ability = AbilityConfigurator.New(GodclawBoons3Ability, Guids.GodclawBoons3Ability)
                .CopyFrom(
                AbilityRefs.ChannelEnergyPaladinHeal,
                typeof(AbilitySpawnFx))
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                       .HealTarget(ContextDice.Value(DiceType.D6, 10, 0))
                       .Build())
                .SetDisplayName(GodclawBoons3Name)
                .SetDescription(GodclawBoons3Description)
                .SetIcon(icon)
                .AddHideDCFromTooltip()
                .SetRange(AbilityRange.Personal)
                .AddAbilityTargetsAround(radius: 30.Feet(), targetType: TargetType.Ally)
                .AddAbilityResourceLogic(1, isSpendResource: true, requiredResource: Guids.GodclawBoons3AbilityResource)
                .SetType(AbilityType.Supernatural)
                .Configure();

            return FeatureConfigurator.New(GodclawBoons3, Guids.GodclawBoons3)
                .SetDisplayName(GodclawBoons3Name)
                .SetDescription(GodclawBoons3Description)
                .SetIcon(icon)
                .AddFacts(new() { ability })
                .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                .Configure();

        }








        #endregion
    }
}