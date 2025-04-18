using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums.Damage;

namespace BlackGoat.Feats.Damnation
{
    public class Fiendskin
    {
        private static readonly string FeatName = "Fiendskin";
        internal const string DisplayName = "Fiendskin.Name";
        private static readonly string Description = "Fiendskin.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Fiendskin, FeatureGroup.Feat)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.PerfectForm.Reference.Get().Icon)
                .AddFeatureTagsComponent(FeatureTag.Skills)
                .AddFacts(new() { Guids.Fiendskin1 })
                .AddFacts(new() { Guids.Fiendskin2 })
                .AddFacts(new() { Guids.Fiendskin3 })
                .AddFacts(new() { Guids.SoullessGaze3 })
                .AddPrerequisiteFeature(Guids.SoullessGaze)
                .AddPrerequisiteFeature(Guids.MaskofVirtue)
                .Configure();
        }
    }
}
