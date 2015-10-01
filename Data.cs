namespace BFCalc{
    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Improved;
    using LibGit2Sharp;
    using Newtonsoft.Json;
    using static System.Environment;
    using static System.IO.File;
    public static class Data{
        public static string Info=$"{GetFolderPath(SpecialFolder.ApplicationData)}/BFCalc/";
        public static string AppData=$"{Info}data/";
        public static string Capitalise(this string s)
            =>
                s.Split(' ','_')
                    .Aggregate("",(a,c)=>a+char.ToUpper(c[0])+c.Remove(0,1));
        public static string Spacify(this string s) =>s.Aggregate("",(a,c)=>a+(char.IsUpper(c)?" ":"")+c);
        public static void Initialise(){
            ItemData.Initialise();
            EvoData.Initialise();
            UnitData.Initialise();
            KeepDiscard.Initialise();
        }//TODO:sortbyid, oncraftablechanged, add combobox text as combobox.Text?
        public static void UpdateData(){
            var path=$"{Info}info.txt";
            if(!Exists(path)) Create(path);
            var lastCheck=ReadAllText(path);
            if(lastCheck==string.Empty) lastCheck=DateTime.MinValue.ToString("s")+"Z";
            if(
                !string.IsNullOrWhiteSpace(new WebClient().DownloadString(//TODO: nononononono DeathMax has an update time, and change time format
                    $"https://api.github.com/repos/Deathmax/bravefrontier_data/commits?since={lastCheck}")))
                new Repository("https://www.github.com/repos/Deathmax/bravefrontier_data/").Network.Pull(
                    new Signature("","",new DateTimeOffset(DateTime.Now)),
                    null);
            WriteAllText(path,DateTime.UtcNow.ToString("s")+"Z");
            var itemsPaths=new[]{$"{AppData}items.json",$"{AppData}jp/items.json",$"{AppData}eu/items.json"};
            foreach(var itemsPath in itemsPaths)
                WriteAllText(itemsPath,
                    ReadAllText(itemsPath).Replace("\n        \"effect\": [","\n        \"effects\": ["));
            var itemsLights=new[]{$"{AppData}jp/items_light.json",$"{AppData}eu/items_light.json"};
            foreach(var itemsLight in itemsLights) Delete(itemsLight);
            WriteAllText($"{AppData}data/info.json",
                JsonConvert.SerializeObject(
                    JsonConvert.DeserializeObject<ListClass.Dict<int,Unit>>(ReadAllText($"{AppData}data/info.json"))));
        }
    }
    public static class Enums{
        public static T? Parse<T>(string s) where T:struct{
            T t;
            if(Enum.TryParse(s.Capitalise(),out t)) return t;
            if(
                Enum.TryParse(
                    typeof(T).GetMembers()
                        .FirstOrDefault(
                            x=>
                                ((EnumMemberAttribute)x.GetCustomAttribute(typeof(EnumMemberAttribute)))?.EnumMemberName==
                                    s)?
                        .Name,
                    out t)) return t;
            return null;
        }
    }
    public class EnumConverter<T>:JsonConverter where T:struct{
        public override void WriteJson(JsonWriter writer,object value,JsonSerializer serializer) {}
        public override object ReadJson(JsonReader reader,
            System.Type objectType,
            object existingValue,
            JsonSerializer serializer){
            if(reader.TokenType!=JsonToken.String) return null;
            var value=reader.Value.ToString();
            T t;
            if(Enum.TryParse(value.Capitalise(),out t)) return t;
            if(
                Enum.TryParse(
                    typeof(T).GetMembers()
                        .FirstOrDefault(
                            x=>
                                ((EnumMemberAttribute)x.GetCustomAttribute(typeof(EnumMemberAttribute)))?.EnumMemberName==
                                    value)?
                        .Name,
                    out t)) return t;
            throw new InvalidOperationException($"{value} is not of type {objectType.Name}");
        }
        public override bool CanConvert(System.Type objectType) =>objectType.IsEnum;
    }
    [AttributeUsage(AttributeTargets.Field)] public class EnumMemberAttribute:System.Attribute{
        public EnumMemberAttribute(string enumMemberName) {EnumMemberName=enumMemberName;}
        public string EnumMemberName{get;set;}
    }
}
