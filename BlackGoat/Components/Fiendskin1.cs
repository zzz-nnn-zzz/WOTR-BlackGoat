using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums.Damage;

namespace BlackGoat.Components
{
    public class Fiendskin1
    {
        private static readonly string FeatName = "Fiendskin1";
        internal const string DisplayName = "Fiendskin1.Name";
        private static readonly string Description = "Fiendskin1.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Fiendskin1)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.AcidSplash.Reference.Get().Icon)
                .AddDamageResistanceEnergy(type: DamageEnergyType.Acid, value: 5)
                .Configure();
        }
    }
}
