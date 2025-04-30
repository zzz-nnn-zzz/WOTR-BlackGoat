using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;

namespace BlackGoat.Feats
{
    internal class RHCloudGiant
    {
        private const string RHCloudGiantFeat = "RHCloudGiant";
        internal const string RHCloudGiantName = "RHCloudGiant.Name";
        private const string RHCloudGiantDescription = "RHCloudGiant.Description";
        public static void Configure()
        {
            var icon = AbilityRefs.TricksterGloriousBeard.Reference.Get().Icon;

            FeatureConfigurator.New(RHCloudGiantFeat, Guids.RHCloudGiant, FeatureGroup.Feat)
                .SetDisplayName(RHCloudGiantName)
                .SetDescription(RHCloudGiantDescription)
                .SetIcon(icon)
                .AddPrerequisiteFeature(RaceRefs.HumanRace.Reference.Get())
                .Configure();
        }
    }
}
