using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace BlackGoat.Components
{
    public class SoullessGaze3
    {
        private static readonly string FeatName = "SoullessGaze3";
        internal const string DisplayName = "SoullessGaze3.Name";
        private static readonly string Description = "SoullessGaze3.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.SoullessGaze3)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.EyesOfTheBodak.Reference.Get().Icon)
                .AddFeatureTagsComponent(FeatureTag.Skills)
                .AddBuffSkillBonus(
                stat: StatType.CheckIntimidate, value: 2, descriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }
    }
}
