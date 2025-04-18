using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums;

namespace BlackGoat.Components
{
    public class Maleficium3
    {
        private static readonly string FeatName = "Maleficium3";
        internal const string DisplayName = "Maleficium3.Name";
        private static readonly string Description = "Maleficium3.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Maleficium3)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.BestowCurse.Reference.Get().Icon)
                .AddIncreaseSpellDescriptorDC(1, SpellDescriptor.Evil, modifierDescriptor:ModifierDescriptor.UntypedStackable)
                .Configure();
        }
    }
}
