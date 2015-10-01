namespace BFCalc{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using static System.Int32;
    public class Ai{
        public AiAction[] Actions{get;set;}
    }
    public class AiAction{
        [JsonProperty("conditions/set parameters")] public Parameter Parameter;
        [JsonProperty("partyconditions/set parameters")] public Parameter PartyParameter;
        public Parameter Action{get;set;}
        [JsonProperty("percent")] public decimal Chance{get;set;}
        public int Priority{get;set;}
        [JsonProperty("target conditions")] public TargetConditions TargetConditions{get;set;}
        [JsonProperty("target type")] public int TargetType{get;set;}
    }
    public class PartyParam:Parameter{
        [JsonConstructor] public PartyParam(string parameters,TargetParam targetparam,int targetType,ActionType type){
            Parameters=
                parameters.Split(new[]{','},StringSplitOptions.RemoveEmptyEntries).ToList().Select(Parse).ToArray();
            TargetParam=targetparam;
            TargetType=targetType;
            Type=type;
        }
        [JsonProperty("target param")] public TargetParam TargetParam{get;set;}
        public int TargetType{get;set;}
    }
    public enum TargetParam{
        All
    }
    public class Parameter{
        public Parameter() {}
        [JsonConstructor] public Parameter(string parameters,ActionType type){
            Parameters=
                parameters.Split(new[]{','},StringSplitOptions.RemoveEmptyEntries).ToList().Select(Parse).ToArray();
            Type=type;
        }
        public int[] Parameters{get;set;}
        public ActionType Type{get;set;}
    }
    public enum ActionType{
        Attack,
        Skill,
        Wait,
        FlgOn,
        FlgOff,
        SkillUse,
        ActBetween,
        SkillUnuse,
        HpPrUnder,
        HpPrOver,
        LimitedAct,
        BeforeTurnBb
    }
}
