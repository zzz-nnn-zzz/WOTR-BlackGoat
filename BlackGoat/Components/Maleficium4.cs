using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;

namespace BlackGoat.Components
{
    public class Maleficium4
    {
        private static readonly string FeatName = "Maleficium4";
        internal const string DisplayName = "Maleficium4.Name";
        private static readonly string Description = "Maleficium4.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Maleficium4)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.BestowCurseGreater.Reference.Get().Icon)
                .AddIncreaseSpellDescriptorCasterLevel(2, SpellDescriptor.Evil, modifierDescriptor: ModifierDescriptor.UntypedStackable)
                .Configure();
        }
    }
}
