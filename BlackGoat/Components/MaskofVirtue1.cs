using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;

namespace BlackGoat.Feats.Damnation
{
    public class MaskofVirtue1
    {
        private static readonly string FeatName = "MaskofVirtue1";
        internal const string DisplayName = "MaskofVirtue1.Name";
        private static readonly string Description = "MaskofVirtue1.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.MaskofVirtue1)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.FalseLifeGreater.Reference.Get().Icon)
                .AddUndetectableAlignment()
                .Configure();
        }
    }
}