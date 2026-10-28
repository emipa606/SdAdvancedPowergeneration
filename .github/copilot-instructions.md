# GitHub Copilot Instructions for RimWorld Modding Project: [sd] Advanced Powergeneration (Continued)

## Mod Overview and Purpose

[sd] Advanced Powergeneration (Continued) is an update and continuation of the "advanced powergeneration" mod. It adds more advanced power generation options to the game, such as advanced wind turbines, solar panels, geothermal power plants, and nuclear power plants. These new power sources are research-dependent and offer higher power outputs compared to vanilla power sources.

## Key Features and Systems

### Advanced Power Plants
- **Advanced Wind Turbine**: Produces up to 6k power.
- **Advanced Solar Panels**: Produces up to 3.4k power.
- **Advanced Geothermal Power Plant**: Produces up to 7.5k power.
- **Nuclear Power Plant**: Produces up to 20k power.
- **Advanced Watermill**: Produces up to 2.2k power.

### Research
All advanced power plants require research at the Spacer technology level. The research tree is dependent on vanilla research branches (electricity, solar, geothermal). The research positions are placed after the ship research in the tree, which may cause some strange connection lines and conflicts with other mods.

### Placement Restrictions
- Wind turbines and geothermal power plants have placement restrictions similar to vanilla.

### Weights and Costs
- **Advanced Wind Turbine**: 500
- **Advanced Solar Panels**: 450
- **Advanced Geothermal Power Plant**: 1000
- **Nuclear Power Plant**: 1250
- **Advanced Watermill**: 2.2k

## Coding Patterns and Conventions

### C# Coding Patterns
- **Namespace**: Use `sd_power` for all C# files.
- **File Structure**: Each power plant has its own C# file with relevant component classes.
- **Component Classes**: Use `CompAdvPowerPlant` as a prefix for power plant component classes.
- **Placement Workers**: Use `PlaceWorker` as a prefix for placement worker classes.
- **ThingDefOf**: Define `ThingDefOf` for all new building definitions.

### XML Integration
- **ResearchProjectDefs.xml**: Defines new research projects for each power plant.
- **ResearchTabDef.xml**: Defines research tab definitions.
- **ThingDefs_buildings.xml**: Defines new building ThingDefs for each power plant.
- **TranslationTemplate**: Contains templates for translation files.

## Harmony Patching

Harmony patching is used to integrate new power plants with existing RimWorld systems. Patches are typically applied in the `Mod.cs` file to modify behavior of vanilla classes or methods.

## Suggestions for Copilot

### XML Definitions
- Use XML tags such as `<ResearchProjectDef>`, `<ResearchTabDef>`, and `<ThingDef>` to define new research and building definitions.
- Ensure that XML files are properly structured and include all necessary attributes and values.

### C# Code
- Use the `sd_power` namespace and follow the established naming conventions.
- Implement component classes for each power plant, including power output calculations and placement restrictions.
- Use Harmony patches to integrate new power plants with existing game systems.

### Research Tree
- Place research projects in the appropriate positions in the research tree, taking into account vanilla dependencies.
- Ensure that research project definitions include all required attributes and dependencies.

### Translation Files
- Create translation templates for new research and building definitions.
- Ensure that all translations are included for additional languages.

### Example Code

#### XML Definition Example
xml
<ResearchProjectDef>
    <defName>AdvancedWindTurbine</defName>
    <label>Advanced Wind Turbine</label>
    <description>Generates up to 6k power.</description>
    <prerequisites>
        <li>ElectricityResearch</li>
    </prerequisites>
    <techLevel>Spacer</techLevel>
</ResearchProjectDef>


#### C# Code Example
csharp
using RimWorld;
using Verse;

namespace sd_power
{
    public class CompAdvPowerPlantWind : CompPowerPlant
    {
        public override float DesiredPowerOutput => 6000f;

        public override void PostDraw()
        {
            base.PostDraw();
            // Additional drawing logic if needed
        }

        public override float RoofedPowerOutputFactor => 0.5f;
    }
}


By following these guidelines, you can effectively use GitHub Copilot to assist in the development of [sd] Advanced Powergeneration (Continued).

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

