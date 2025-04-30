using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using System;

namespace BlackGoat.Components
{
	[AllowMultipleComponents]
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("38BDE75F-B5BF-470C-85C6-EFAFE84DC53B")]
	public class GodclawBonusHealingPerDie : UnitFactComponentDelegate, 
		ITargetRulebookHandler<RuleHealDamage>,
		IRulebookHandler<RuleHealDamage>, 
		ISubscriber, 
		ITargetRulebookSubscriber
	{

		public void OnEventAboutToTrigger(RuleHealDamage evt)
		{
            try
            {
				if (evt.Reason.Ability == null)
				{
					return;
				}

				if(evt.GetRuleTarget().Equals(base.Owner))
                {
					HealBonus = new ContextValue();
					HealBonus.Value = evt.HealFormula.ModifiedValue.Rolls * 1;

					evt.AdditionalBonus.Add(new Modifier(this.HealBonus.Calculate(base.Context), base.Fact, ModifierDescriptor.UntypedStackable));
				}
			}
			catch (Exception ex)
            {

            }
		}

		public void OnEventDidTrigger(RuleHealDamage evt)
		{
		}

		public ContextValue HealBonus;
		
	}
}