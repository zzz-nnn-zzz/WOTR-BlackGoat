using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace BlackGoat.Feats.Damnation
{
    public class Maleficium
    {
        private static readonly string FeatName = "Maleficium";
        internal const string DisplayName = "Maleficium.Name";
        private static readonly string Description = "Maleficium.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Maleficium, FeatureGroup.Feat)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.BestowCurse.Reference.Get().Icon)
                .AddFeatureTagsComponent(FeatureTag.Skills)
                //
                .AddFacts(new() { Guids.Fiendskin4 })
                //
                .AddFacts(new() { Guids.Maleficium1 })
                .AddFacts(new() { Guids.Maleficium2 })
                .AddFacts(new() { Guids.Maleficium3 })
                .AddFacts(new() { Guids.Maleficium4 })
                .AddPrerequisiteFeature(Guids.SoullessGaze)
                .AddPrerequisiteFeature(Guids.MaskofVirtue)
                .AddPrerequisiteFeature(Guids.Fiendskin)
                .Configure();
        }
    }
}