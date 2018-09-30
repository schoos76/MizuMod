using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Verse;
using RimWorld;

namespace MizuMod
{
    public class Designator_AreaMopExpand : Designator_AreaMop
    {
        public Designator_AreaMopExpand() : base(DesignateMode.Add)
        {
            this.defaultLabel = MizuStrings.DesignatorAreaMopExpand.Translate();
            this.defaultDesc = MizuStrings.DesignatorAreaMopExpandDescription.Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/HomeAreaOn", true);
            this.soundDragSustain = SoundDefOf.Designate_DragAreaAdd;
            this.soundDragChanged = SoundDefOf.Designate_DragStandard_Changed;
            this.soundSucceeded = SoundDefOf.Designate_AreaAdd;
        }
    }
}
