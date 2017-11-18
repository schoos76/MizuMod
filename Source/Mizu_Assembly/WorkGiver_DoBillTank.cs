﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;
using Verse.AI;

namespace MizuMod
{
    public class WorkGiver_DoBillTank : WorkGiver_DoBill
    {
        public override Job JobOnThing(Pawn pawn, Thing thing, bool forced = false)
        {
            Job baseJob = base.JobOnThing(pawn, thing, forced);
            if (baseJob == null || baseJob.bill == null || baseJob.bill.recipe == null)
            {
                return null;
            }

            FaucetRecipeDef recipe = baseJob.bill.recipe as FaucetRecipeDef;
            if (recipe == null)
            {
                return null;
            }

            IBuilding_WaterNet building = thing as IBuilding_WaterNet;
            CompWaterNetTank comp = building.GetComp<CompWaterNetTank>();
            if (building == null || building.InputWaterNet == null)
            {
                return null;
            }
            if (recipe.needWaterType != comp.StoredWaterType)
            {
                return null;
            }
            if (recipe.needWaterVolume > comp.StoredWaterVolume)
            {
                return null;
            }

            Job job = new Job(MizuDef.Job_DoBillTank, (Thing)thing);
            job.targetQueueB = baseJob.targetQueueB;
            job.countQueue = baseJob.countQueue;
            job.haulMode = baseJob.haulMode;
            job.bill = baseJob.bill;
            return job;
        }
    }
}