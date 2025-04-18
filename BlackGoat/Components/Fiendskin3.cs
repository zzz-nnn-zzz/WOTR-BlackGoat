using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums.Damage;

namespace BlackGoat.Components
{
    public class Fiendskin3
    {
        private static readonly string FeatName = "Fiendskin3";
        internal const string DisplayName = "Fiendskin3.Name";
        private static readonly string Description = "Fiendskin3.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Fiendskin3)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.OracleRevelationIceArmorAbility.Reference.Get().Icon)
                .AddEnergyDamageImmunity(DamageEnergyType.Cold)
                .Configure();
        }
    }
}
