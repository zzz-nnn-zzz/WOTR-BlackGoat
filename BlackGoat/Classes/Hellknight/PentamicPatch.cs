using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace BlackGoat.Classes.Hellknight
{
    internal class PentamicPatch
    {
        public static void Configure()
        {
            var pentamicFaithGuid = "b9750875e9d7454e85347d739a1bc894";

            FeatureSelectionConfigurator.For(pentamicFaithGuid)
                .RemoveComponents(comp => comp is IParamPrerequisite || comp is Prerequisite)
                .Configure();
        }
    }
}
