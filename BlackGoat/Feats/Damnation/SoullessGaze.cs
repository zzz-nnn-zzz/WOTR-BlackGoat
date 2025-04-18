using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;

namespace BlackGoat.Feats.Damnation
{
    public class SoullessGaze
    {
        private static readonly string FeatName = "SoullessGaze";
        internal const string DisplayName = "SoullessGaze.Name";
        private static readonly string Description = "SoullessGaze.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.SoullessGaze, FeatureGroup.Feat)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.Blindness.Reference.Get().Icon)
                .AddFeatureTagsComponent(FeatureTag.Skills)
                .AddFacts(new() { Guids.SoullessGaze1 })
                .Configure();
        }
    }
}
