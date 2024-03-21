using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace sd_adv_powergen;

public class PlaceWorker_AdvWatermillGenerator : PlaceWorker
{
    private static readonly List<Thing> advwaterMills = [];

    public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot, Map map,
        Thing thingToIgnore = null, Thing thing = null)
    {
        foreach (var current in CompPowerPlantWater.GroundCells(loc, rot))
        {
            if (map.terrainGrid.TerrainAt(current).affordances.Contains(TerrainAffordanceDefOf.Heavy))
            {
                continue;
            }

            var result =
                new AcceptanceReport(
                    "TerrainCannotSupport_TerrainAffordance".Translate(checkingDef,
                        TerrainAffordanceDefOf.Heavy));
            return result;
        }

        if (!WaterCellsPresent(loc, rot, map))
        {
            return new AcceptanceReport("MustBeOnMovingWater".Translate());
        }

        return true;
    }

    private bool WaterCellsPresent(IntVec3 loc, Rot4 rot, Map map)
    {
        foreach (var current in CompPowerPlantWater.WaterCells(loc, rot))
        {
            if (!map.terrainGrid.TerrainAt(current).affordances.Contains(TerrainAffordanceDefOf.MovingFluid))
            {
                return false;
            }
        }

        return true;
    }

    public override void DrawGhost(ThingDef def, IntVec3 loc, Rot4 rot, Color ghostCol, Thing thing = null)
    {
        GenDraw.DrawFieldEdges(CompPowerPlantWater.GroundCells(loc, rot).ToList(), Color.white);
        var color = !WaterCellsPresent(loc, rot, Find.CurrentMap)
            ? Designator_Place.CannotPlaceColor.ToOpaque()
            : Designator_Place.CanPlaceColor.ToOpaque();
        GenDraw.DrawFieldEdges(CompPowerPlantWater.WaterCells(loc, rot).ToList(), color);
        var overlaps = false;
        var cellRect = CompPowerPlantWater.WaterUseRect(loc, rot);
        advwaterMills.AddRange(
            Find.CurrentMap.listerBuildings.AllBuildingsColonistOfDef(ThingDefOf
                .sd_adv_powergen_WatermillGenerator));
        advwaterMills.AddRange(from t in Find.CurrentMap.listerThings.ThingsInGroup(ThingRequestGroup.Blueprint)
            where t.def.entityDefToBuild == ThingDefOf.sd_adv_powergen_WatermillGenerator
            select t);
        advwaterMills.AddRange(from t in Find.CurrentMap.listerThings.ThingsInGroup(ThingRequestGroup.BuildingFrame)
            where t.def.entityDefToBuild == ThingDefOf.sd_adv_powergen_WatermillGenerator
            select t);
        foreach (var current in advwaterMills)
        {
            GenDraw.DrawFieldEdges(CompPowerPlantWater.WaterUseCells(current.Position, current.Rotation).ToList(),
                new Color(0.2f, 0.2f, 1f));
            if (cellRect.Overlaps(CompPowerPlantWater.WaterUseRect(current.Position, current.Rotation)))
            {
                overlaps = true;
            }
        }

        advwaterMills.Clear();
        var color2 = !overlaps ? Designator_Place.CanPlaceColor.ToOpaque() : new Color(1f, 0.6f, 0f);
        if (!overlaps || Time.realtimeSinceStartup % 0.4f < 0.2f)
        {
            GenDraw.DrawFieldEdges(CompPowerPlantWater.WaterUseCells(loc, rot).ToList(), color2);
        }
    }

    [DebuggerHidden]
    public override IEnumerable<TerrainAffordanceDef> DisplayAffordances()
    {
        yield return TerrainAffordanceDefOf.Heavy;
        yield return TerrainAffordanceDefOf.MovingFluid;
    }
}