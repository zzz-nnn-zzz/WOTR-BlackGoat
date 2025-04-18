using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace BlackGoat.Feats.Damnation
{
    public class MaskofVirtue
    {
        private static readonly string FeatName = "MaskofVirtue";
        internal const string DisplayName = "MaskofVirtue.Name";
        private static readonly string Description = "MaskofVirtue.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.MaskofVirtue, FeatureGroup.Feat)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.FalseLife.Reference.Get().Icon)
                .AddFeatureTagsComponent(FeatureTag.Skills)
                .AddFacts(new() { Guids.MaskofVirtue1 })
                .AddFacts(new() { Guids.SoullessGaze2 })
                .AddPrerequisiteFeature(Guids.SoullessGaze)
                .Configure();
        }
    }
}