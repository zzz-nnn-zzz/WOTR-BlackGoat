using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums.Damage;

namespace BlackGoat.Components
{
    public class Fiendskin2
    {
        private static readonly string FeatName = "Fiendskin2";
        internal const string DisplayName = "Fiendskin2.Name";
        private static readonly string Description = "Fiendskin2.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Fiendskin2)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.AcidArrow.Reference.Get().Icon)
                .AddDamageResistanceEnergy(type: DamageEnergyType.Acid, value: 10)
                .Configure();
        }
    }
}
