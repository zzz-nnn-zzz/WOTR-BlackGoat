using BlackGoat.Utils;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;

namespace BlackGoat.Feats
{
    internal class DestroyIdentity
    {
        private const string DestroyIdentityFeat = "DestroyIdentity";
        internal const string DestroyIdentityName = "DestroyIdentity.Name";
        private const string DestroyIdentityDescription = "DestroyIdentity.Description";

        private const string DestroyIdentityBuff = "DestroyIdentityBuff";
        private const string DestroyIdentityAbility = "DestroyIdentityAbility";
        public static void Configure()
        {
            var icon = FeatureRefs.LamashtuFeature.Reference.Get().Icon;

            var buff = BuffConfigurator.New(DestroyIdentityBuff, Guids.DestroyIdentityBuff)
                .SetDisplayName(DestroyIdentityName)
                .SetDescription(DestroyIdentityDescription)
                .SetIcon(icon)
                .AddInitiatorAttackWithWeaponTrigger(
                   onlyHit: true,
                   criticalHit: true,
                   action: ActionsBuilder.New()
                        .ApplyBuff(BuffRefs.Staggered.ToString(), ContextDuration.Fixed(1))
                        .DealDamageToAbility(StatType.Charisma, ContextDice.Value(DiceType.One, diceCount: 2)))
                .Configure();

            var toggle = ActivatableAbilityConfigurator.New(DestroyIdentityAbility, Guids.DestroyIdentityToggle)
                .SetDisplayName(DestroyIdentityName)
                .SetDescription(DestroyIdentityDescription)
                .SetIcon(icon)
                .SetBuff(buff)
                .Configure();

            FeatureConfigurator.New(DestroyIdentityFeat, Guids.DestroyIdentity, FeatureGroup.Feat)
                .SetDisplayName(DestroyIdentityName)
                .SetDescription(DestroyIdentityDescription)
                .SetIcon(icon)
                .AddFacts(new() { toggle })
                .AddPrerequisiteFeature(FeatureRefs.CriticalFocus.Reference.Get())
                .AddPrerequisiteStatValue(StatType.BaseAttackBonus, value: 11)
                .AddPrerequisiteFeature(FeatureRefs.LamashtuFeature.Reference.Get())
                .Configure();
        }
    }
}