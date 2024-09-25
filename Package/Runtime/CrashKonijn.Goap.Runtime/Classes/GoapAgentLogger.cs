﻿using System.Linq;
using CrashKonijn.Agent.Core;
using CrashKonijn.Agent.Runtime;
using CrashKonijn.Goap.Core;

namespace CrashKonijn.Goap.Runtime
{
    public class GoapAgentLogger : LoggerBase<IMonoGoapActionProvider>
    {
        protected override string Name => this.source.name;

        protected override void RegisterEvents()
        {
            if (this.source == null)
                return;
            
            // Todo
            this.source.Events.OnNoActionFound += this.NoActionFound;
            this.source.Events.OnGoalStart += this.GoalStart;
            this.source.Events.OnGoalCompleted += this.GoalCompleted;
        }
        
        protected override void UnregisterEvents()
        {
            if (this.source == null)
                return;
            
            // Todo
            this.source.Events.OnNoActionFound -= this.NoActionFound;
            this.source.Events.OnGoalStart -= this.GoalStart;
            this.source.Events.OnGoalCompleted -= this.GoalCompleted;
        }
        
        private void NoActionFound(IGoalRequest request) => this.Handle((builder) =>
        {
            builder.Append($"No action found for goals {string.Join(", ", request.Goals.Select(x => x.GetType().GetGenericTypeName()))}");
        }, DebugSeverity.Warning);
        private void GoalStart(IGoal goal) => this.Handle((builder) =>
        {
            builder.Append($"Goal {goal?.GetType().GetGenericTypeName()} started");
        }, DebugSeverity.Log);
        private void GoalCompleted(IGoal goal) => this.Handle((builder) =>
        {
            builder.Append($"Goal {goal?.GetType().GetGenericTypeName()} completed");
        }, DebugSeverity.Log);

    }
}