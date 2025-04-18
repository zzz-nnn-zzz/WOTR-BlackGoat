using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;

namespace BlackGoat.Components
{
    public class Maleficium2
    {
        private static readonly string FeatName = "Maleficium2";
        internal const string DisplayName = "Maleficium2.Name";
        private static readonly string Description = "Maleficium2.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Maleficium2)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.BestowCurse.Reference.Get().Icon)
                //TODO
                .Configure();
        }
    }
}
