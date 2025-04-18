using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums.Damage;

namespace BlackGoat.Components
{
    public class Fiendskin4
    {
        private static readonly string FeatName = "Fiendskin4";
        internal const string DisplayName = "Fiendskin4.Name";
        private static readonly string Description = "Fiendskin4.Description";

        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, Guids.Fiendskin4)
                .SetReapplyOnLevelUp()
                .SetDisplayName(DisplayName)
                .SetDescription(Description)
                .SetIcon(AbilityRefs.OracleRevelationArmorOfBonesAbility.Reference.Get().Icon)
                .AddEnergyDamageImmunity(DamageEnergyType.Acid)
                .AddFacts(new() { FeatureRefs.OutsiderType.ToString() })
                .Configure();
        }
    }
}
