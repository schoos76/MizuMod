using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using Verse.AI;

namespace MizuMod
{
    public class WorkGiver_GetSnow : WorkGiver_Scanner
    {
        public const float ConsumeSnowPerOne = 0.5f;

        public override PathEndMode PathEndMode
        {
            get
            {
                return PathEndMode.Touch;
            }
        }

        public override IEnumerable<IntVec3> PotentialWorkCellsGlobal(Pawn pawn)
        {
            return pawn.Map.areaManager.SnowGet().ActiveCells;
        }

        public override bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            return pawn.Map.areaManager.SnowGet().TrueCount == 0;
        }

        public override bool HasJobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            if (pawn.Map.snowGrid.GetDepth(c) < ConsumeSnowPerOne) return false;

            if (c.IsForbidden(pawn)) return false;

            if (!pawn.CanReserve(c)) return false;

            return true;
        }

        public override Job JobOnCell(Pawn pawn, IntVec3 c, bool forced = false)
        {
            return new Job(MizuDef.Job_GetSnow, c);
        }
    }
}
