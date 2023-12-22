using Aoc2023.Helpers;
using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day19 : ISolver
{
    public string DayName => nameof(Day19);
    private string[] _lines;
    private const string _numberRegex = @"(\d)+";

    public void Solve()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);
        var numberRegex = new Regex(_numberRegex);
        var metalParts = new List<MetalPart>();
        var workflows = new List<Workflow>();

        for (var i = 0; i < _lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(_lines[i])) continue;

            if (_lines[i].StartsWith('{')) // metal part
            {
                var numberMatches = numberRegex.Matches(_lines[i]);

                metalParts.Add(new(
                    int.Parse(numberMatches[0].Value),
                    int.Parse(numberMatches[1].Value),
                    int.Parse(numberMatches[2].Value),
                    int.Parse(numberMatches[3].Value)));

                continue;
            }

            // workflow
            _lines[i] = _lines[i].Remove(_lines[i].Length - 1, 1); // remove closing }

            var workflowMainParts = _lines[i].Split('{');
            var rulesParts = workflowMainParts[1].Split(',');
            var workflow = new Workflow { Name = workflowMainParts[0] };
            workflow.Rules.AddRange(rulesParts.Select(x => new WorkflowRule(x)));

            workflows.Add(workflow);
        }

        // solution 1
        foreach (var metalPart in metalParts)
        {
            var currentWorkflowName = "in";
            bool existWasHit = false;

            while (!existWasHit)
            {
                var workflow = workflows.Find(x => x.Name == currentWorkflowName);
                currentWorkflowName = CalculateRedirect(workflow, metalPart);

                if (currentWorkflowName == "A")
                {
                    metalPart.Status = MetalPartStatus.Accepted;

                    existWasHit = true;
                }

                if (currentWorkflowName == "R")
                {
                    metalPart.Status = MetalPartStatus.Rejected;

                    existWasHit = true;
                }
            }
        }

        var solution1 = metalParts.Where(m => m.Status == MetalPartStatus.Accepted).Sum(mp => mp.Shiny + mp.Musical + mp.Aerodynamic + mp.XTremlyCoolLooking);

        ReadWriteHelpers.WriteResult(DayName, "1", solution1);
    }

    string CalculateRedirect(Workflow workflow, MetalPart metalPart)
    {
        var redirectRo = string.Empty;

        foreach (var workflowRule in workflow.Rules)
        {
            var workflowRuleParts = workflowRule.Rule.Split(':');

            if (workflowRuleParts.Length == 1)
            {
                redirectRo = workflowRule.Rule;

                break;
            }

            if ((workflowRule.Rule[0] == 'a' && EvaluateRule(workflowRuleParts[0], metalPart.Aerodynamic))
                || (workflowRule.Rule[0] == 'm' && EvaluateRule(workflowRuleParts[0], metalPart.Musical))
                || (workflowRule.Rule[0] == 's' && EvaluateRule(workflowRuleParts[0], metalPart.Shiny))
                || (workflowRule.Rule[0] == 'x' && EvaluateRule(workflowRuleParts[0], metalPart.XTremlyCoolLooking)))
            {
                redirectRo = workflowRuleParts[1];

                break;
            }
        }

        return redirectRo;
    }

    bool EvaluateRule(string rule, int compareTo)
    {
        var operation = rule[1];
        var value = rule.Split(operation)[1];

        return operation switch
        {
            '<' => compareTo < int.Parse(value),
            '>' => compareTo > int.Parse(value),
            _ => int.Parse(value) == compareTo
        };
    }

    record MetalPart(int XTremlyCoolLooking, int Musical, int Aerodynamic, int Shiny)
    {
        public MetalPartStatus Status { get; set; }
    }

    record WorkflowRule(string Rule);

    class Workflow
    {
        public string Name { get; set; }
        public List<WorkflowRule> Rules { get; set; } = [];
    }

    enum MetalPartStatus
    {
        Accepted,
        Rejected
    }
}
