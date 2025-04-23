using UnityEngine;

public static class ProjectDataGenerator
{
    public static int GetRequiredSkill(ProjectType type, ProjectSize size, SkillType skill)
    {
        int min, max;

        switch (size)
        {
            case ProjectSize.Small:  (min, max) = (8, 14); break;
            case ProjectSize.Medium: (min, max) = (15, 25); break;
            case ProjectSize.Large:  (min, max) = (30, 40); break;
            default:                 (min, max) = (10, 20); break;
        }

        int bonus = 0;
        if (type == ProjectType.Strategy && skill == SkillType.Design) bonus = 5;
        if (type == ProjectType.RPG && skill == SkillType.Programming) bonus = 5;
        if (type == ProjectType.Casual && skill == SkillType.Art) bonus = 3;

        return Random.Range(min, max + 1) + bonus;
    }

    public static int GetRequiredWorkAmount(ProjectSize size)
    {
        return size switch
        {
            ProjectSize.Small => Random.Range(800, 1201),     // 800 ~ 1200
            ProjectSize.Medium => Random.Range(1600, 2401),   // 1600 ~ 2400
            ProjectSize.Large => Random.Range(3200, 4801),    // 3200 ~ 4800
            _ => 100
        };
    }

    public static int GetRewardEstimate(ProjectType type, ProjectSize size)
    {
        int min, max;

        switch (size)
        {
            case ProjectSize.Small:  (min, max) = (400, 600); break;
            case ProjectSize.Medium: (min, max) = (900, 1200); break;
            case ProjectSize.Large:  (min, max) = (1800, 2400); break;
            default:                 (min, max) = (500, 800); break;
        }

        return Random.Range(min, max + 1);
    }
}