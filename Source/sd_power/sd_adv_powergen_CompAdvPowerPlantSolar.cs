/*
 * Created by SharpDevelop.
 * User: sulusdacor
 * Date: 17.11.2016
 * Time: 10:32
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

// ----------------------------------------------------------------------
// These are RimWorld-specific usings. Activate/Deactivate what you need:
// ----------------------------------------------------------------------

using RimWorld;
using UnityEngine;
using Verse;
// Always needed
//using VerseBase;         // Material/Graphics handling functions are found here
// RimWorld universal objects are here (like 'Building')
//using Verse.AI;          // Needed when you do something with the AI
//using Verse.Sound;       // Needed when you do something with Sound
//using Verse.Noise;       // Needed when you do something with Noises
// RimWorld specific functions are found here (like 'Building_Battery')
//using RimWorld.Planet;   // RimWorld specific functions for world creation
//using RimWorld.SquadAI;  // RimWorld specific functions for squad brains

namespace sd_adv_powergen;

[StaticConstructorOnStartup]
public class sd_adv_powergen_CompAdvPowerPlantSolar : CompPowerPlant
{
    private const float sd_adv_powergen_FullSunPower = 3400f;

    private const float sd_adv_powergen_NightPower = 0f;

    private static readonly Vector2 sd_adv_powergen_BarSize = new Vector2(2.3f, 0.14f);

    private static readonly Material sd_adv_powergen_PowerPlantSolarBarFilledMat =
        SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f));

    private static readonly Material sd_adv_powergen_PowerPlantSolarBarUnfilledMat =
        SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f));

    protected override float DesiredPowerOutput =>
        Mathf.Lerp(0f, 3400f, parent.Map.skyManager.CurSkyGlow) * RoofedPowerOutputFactor;

    private float RoofedPowerOutputFactor
    {
        get
        {
            var num = 0;
            var num2 = 0;
            foreach (var current in parent.OccupiedRect())
            {
                num++;
                if (parent.Map.roofGrid.Roofed(current))
                {
                    num2++;
                }
            }

            return (num - num2) / (float)num;
        }
    }

    public override void PostDraw()
    {
        base.PostDraw();
        var r = default(GenDraw.FillableBarRequest);
        r.center = parent.DrawPos + (Vector3.up * 0.1f);
        r.size = sd_adv_powergen_BarSize;
        r.fillPercent = PowerOutput / sd_adv_powergen_FullSunPower;
        r.filledMat = sd_adv_powergen_PowerPlantSolarBarFilledMat;
        r.unfilledMat = sd_adv_powergen_PowerPlantSolarBarUnfilledMat;
        r.margin = 0.15f;
        var rotation = parent.Rotation;
        rotation.Rotate(RotationDirection.Clockwise);
        r.rotation = rotation;
        GenDraw.DrawFillableBar(r);
    }
}