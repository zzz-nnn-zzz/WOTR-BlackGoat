using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace BlackGoat.Components
{
    public class SoullessGaze1
    {
        private static readonly string FeatName = "SoullessGaze1";
        internal const string DisplayName = "SoullessGaze1.Name";
        private static readonly string Description = "SoullessGaze1.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.SoullessGaze1)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.Hypnotism.Reference.Get().Icon)
                .AddBuffSkillBonus(
                stat: StatType.CheckIntimidate, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }
    }
}
