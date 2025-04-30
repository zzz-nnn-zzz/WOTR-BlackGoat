using BlackGoat.Utils;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Mechanics;
using static Kingmaker.UI.GenericSlot.EquipSlotBase;
using static Kingmaker.UnitLogic.Commands.Base.UnitCommand;

namespace BlackGoat.Feats
{
    internal class HellknightObsession
    {
        private const string HellknightObsessionFeat = "HellknightObsession";
        internal const string HellknightObsessionName = "HellknightObsession.Name";
        private const string HellknightObsessionDescription = "HellknightObsession.Description";

        private const string HellknightObsessionBuff3 = "HellknightObsessionBuff3";
        private const string HellknightObsessionBuff4 = "HellknightObsessionBuff4";
        private const string HellknightObsessionAbility3 = "HellknightObsessionAbility3";
        internal const string HellknightObsessionAbility3Name = "HellknightObsessionAbility3.Name";
        private const string HellknightObsessionAbility3Description = "HellknightObsessionAbility3.Description";
        private const string HellknightObsessionAbility4 = "HellknightObsessionAbility4";
        private const string HellknightObsessionAbility4MainHand = "HellknightObsessionAbility4MainHand";
        private const string HellknightObsessionAbility4OffHand = "HellknightObsessionAbility4OffHand";
        internal const string HellknightObsessionAbility4Name = "HellknightObsessionAbility4.Name";
        private const string HellknightObsessionAbility4Description = "HellknightObsessionAbility4.Description";
        private const string HellknightObsessionAbilityResource = "HellknightObsessionAbilityResource";

        public static void Configure()
        {
            var icon = AbilityRefs.EnlargePerson.Reference.Get().Icon;

            var abilityresource = AbilityResourceConfigurator.New(HellknightObsessionAbilityResource, Guids.HellknightObsessionAbilityResource)
                .SetMaxAmount(ResourceAmountBuilder.New(1))
                .Configure();

            var buff3 = BuffConfigurator.New(HellknightObsessionBuff3, Guids.HellknightObsessionBuff3)
                .SetDisplayName(HellknightObsessionAbility3Name)
                .SetDescription(HellknightObsessionAbility3Description)
                .SetIcon(AbilityRefs.Fear.Reference.Get().Icon)
                .AddBuffSkillBonus(
                stat: StatType.CheckIntimidate, value: 3, descriptor: ModifierDescriptor.UntypedStackable)
                .AddBuffSkillBonus(
                stat: StatType.SkillPerception, value: 3, descriptor: ModifierDescriptor.UntypedStackable)
                .Configure();

            var buff4 = BuffConfigurator.New(HellknightObsessionBuff4, Guids.HellknightObsessionBuff4)
                 .SetDisplayName(HellknightObsessionAbility4Name)
                 .SetDescription(HellknightObsessionAbility4Description)
                 .SetIcon(AbilityRefs.ArcaneAccuracyAbility.Reference.Get().Icon)
                 .Configure();

            var ability3 = AbilityConfigurator.New(HellknightObsessionAbility3, Guids.HellknightObsessionAbility3)
                .SetDisplayName(HellknightObsessionAbility3Name)
                .SetDescription(HellknightObsessionAbility3Description)
                .SetIcon(AbilityRefs.Fear.Reference.Get().Icon)
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                       .ApplyBuffWithDurationSeconds(buff3, 3600)
                       .Build())
                .SetRange(AbilityRange.Personal)
                .SetActionType(CommandType.Swift)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch)
                .AddAbilityResourceLogic(1, isSpendResource: true, requiredResource: Guids.HellknightObsessionAbilityResource)
                .Configure();

            var ability4 = AbilityConfigurator.New(HellknightObsessionAbility4, Guids.HellknightObsessionAbility4)
                .SetDisplayName(HellknightObsessionAbility4Name)
                .SetDescription(HellknightObsessionAbility4Description)
                .SetIcon(AbilityRefs.ArcaneAccuracyAbility.Reference.Get().Icon)
                .SetRange(AbilityRange.Personal)
                .SetActionType(CommandType.Swift)
                .AddPretendSpellLevel(spellLevel: 1)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch)
                .SetEffectOnAlly(AbilityEffectOnUnit.Helpful)
                .AddAbilityVariants(new() { ConfigureVariant(mainHand: true), ConfigureVariant(mainHand: false) })
                .AddAbilityResourceLogic(1, isSpendResource: true, requiredResource: Guids.HellknightObsessionAbilityResource)
                .Configure();

            FeatureConfigurator.New(HellknightObsessionFeat, Guids.HellknightObsession, FeatureGroup.Feat)
                .SetDisplayName(HellknightObsessionName)
                .SetDescription(HellknightObsessionDescription)
                .SetIcon(icon)
                .AddFacts(new() { ability3 })
                .AddFacts(new() { ability4 })
                .AddAbilityResources(resource: abilityresource, restoreAmount: true)
                .AddPrerequisiteFeature(Guids.HellknightObedience)
                .AddPrerequisiteStatValue(StatType.SkillKnowledgeArcana, 3)
                .AddPrerequisiteAlignment(AlignmentMaskType.Lawful)
                .Configure();
        }

        private static BlueprintAbility ConfigureVariant(bool mainHand)
        {
            var icon = AbilityRefs.ArcaneAccuracyAbility.Reference.Get().Icon;

            return AbilityConfigurator.New(
                mainHand ? HellknightObsessionAbility4MainHand : HellknightObsessionAbility4OffHand,
                mainHand ? Guids.HellknightObsessionAbility4MainHand : Guids.HellknightObsessionAbility4OffHand)
              .SetDisplayName(mainHand ? HellknightObsessionAbility4Name : HellknightObsessionAbility4Name)
              .SetDescription(HellknightObsessionAbility4Description)
              .SetIcon(icon)
              .SetRange(AbilityRange.Personal)
              .SetActionType(CommandType.Swift)
              .AddPretendSpellLevel(spellLevel: 1)
              .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch)
              .SetEffectOnAlly(AbilityEffectOnUnit.Helpful)
              .SetParent(Guids.HellknightObsessionAbility4)
              .AddAbilityEffectRunAction(ActionsBuilder.New()
                  .EnchantWornItem(
                    ContextDuration.Variable(ContextValues.Rank(), rate: DurationRate.Hours),
                    WeaponEnchantmentRefs.Keen.ToString(),
                    slot: mainHand ? SlotType.PrimaryHand : SlotType.SecondaryHand)
                       .ApplyBuffWithDurationSeconds(Guids.HellknightObsessionBuff4, 3600)
                       .Build())
              .Configure();
        }
    }
}