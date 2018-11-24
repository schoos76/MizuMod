/*
 * Created by SharpDevelop.
 * User: Michael
 * Date: 11/4/2018
 * Time: 5:05 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using RimWorld;
using Verse;
using Verse.AI;

namespace MizuMod
{
	/// <summary>
	/// Description of JobGiver_GetWater_PrisonLabour.
	/// </summary>
	public class JobGiver_GetWater_PrisonLabour : ThinkNode_JobGiver
	{
		//private const int MaxDistanceOfSearchWaterTerrain = 300;
		private const int SearchWaterIntervalTick = 180;

		private ThirstCategory minCategory = ThirstCategory.SlightlyThirsty;

		public override ThinkNode DeepCopy(bool resolve = true)
		{
			var jobGiver_GetWater_PrisonLabour = base.DeepCopy(resolve) as JobGiver_GetWater_PrisonLabour;
			jobGiver_GetWater_PrisonLabour.minCategory = this.minCategory;
			return jobGiver_GetWater_PrisonLabour;
		}
        
		protected override Job TryGiveJob(Pawn pawn)
		{
			Need_Water need_water = pawn.needs.water();
			if (need_water == null)
				return null;

			if (need_water.lastSearchWaterTick + SearchWaterIntervalTick > Find.TickManager.TicksGame)
				return null;
			// Only trink if we're really thirsty.  
			if (need_water.CurLevelPercentage > need_water.PercentageThreshThirsty) 
				return null;
			
			
			need_water.lastSearchWaterTick = Find.TickManager.TicksGame;

			
			Thing thing = MizuUtility.TryFindBestWaterSourceFor(pawn, pawn, false, true);
			if (thing != null) {
				if (thing.CanDrinkWater()) {
					return new Job(MizuDef.Job_DrinkWater, thing) {
						count = MizuUtility.WillGetStackCountOf(pawn, thing)
					};
				} else if (thing is IBuilding_DrinkWater) {
					return new Job(MizuDef.Job_DrinkWaterFromBuilding, thing);
				}
			}

			// 何も見つからなかった場合は隠し水飲み場を探す
			// 人間、家畜、野生の動物全て
			IntVec3 hiddenWaterSpot;
			if (MizuUtility.TryFindHiddenWaterSpot(pawn, out hiddenWaterSpot)) {
				return new Job(MizuDef.Job_DrinkWater, hiddenWaterSpot) {
					count = 1
				};
			}

			// 水を発見できず
			return null;
		}
	}
}
