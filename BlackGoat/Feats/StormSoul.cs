using BlackGoat.Utils;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Enums.Damage;

namespace BlackGoat.Feats
{
    internal class StormSoul
    {
        private const string StormSoulFeat = "StormSoul";
        internal const string StormSoulName = "StormSoul.Name";
        private const string StormSoulDescription = "StormSoul.Description";
        public static void Configure()
        {
            var icon = AbilityRefs.ResistElectricity.Reference.Get().Icon;

            FeatureConfigurator.New(StormSoulFeat, Guids.StormSoul, FeatureGroup.Feat)
                .SetDisplayName(StormSoulName)
                .SetDescription(StormSoulDescription)
                .SetIcon(icon)
                .AddPrerequisiteFeature(Guids.RHCloudGiant)
                .AddEnergyDamageImmunity(DamageEnergyType.Electricity)
                .Configure();
        }
    }
}