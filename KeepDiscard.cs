namespace BFCalc{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using Newtonsoft.Json;
    using static System.IO.File;
    using static Data;
    using static ItemData;
    using static KeepCraftable;
    using static KeepMaterial;
    using static Use;
    public static class KeepDiscard{
        public static string SavePath=$"{Info}keep_discard.json";
        public static KeepOrDiscard Keep{get;set;}
        public static UsedIn UsedIn{get;set;}
        public static KeepOrDiscardMaterial Materials{get;private set;}
        public static IEnumerable<KeepMaterial> FilteredMaterials{get;set;}
        public static KeepOrDiscardMaterial FilteredMaterials2{get;set;}
        public static void Initialise(){
            if(Exists(SavePath)) Keep=JsonConvert.DeserializeObject<KeepOrDiscard>(ReadAllText(SavePath));
            else{
                Keep=new KeepOrDiscard();
                foreach(var c in CraftablesByName.Keys.ToList()) Keep.Add(Kc(c,false));
            }
            UsedIn=new UsedIn();
            Materials=new KeepOrDiscardMaterial();
            foreach(var i in Items.Values){
                UsedIn.Add(U(i.Name,new List<string>()));
                Materials.Add(Km(i.Name,false));
            }
            FilteredMaterials=FilteredMaterials2=Materials;
        }
        public static void FilterMaterials(string s,bool useRegex){//TODO: too slow
            if(s==string.Empty){
                FilteredMaterials=Materials;
                return;
            }
            var regex=useRegex?new Regex(s):new Regex($"^{s}.*");
            FilteredMaterials=Materials.Where(m=>regex.IsMatch(m.Material));
        }
        public static void UpdateUsedIn(object sender,RoutedEventArgs args,bool selected){//TODO: clear all (init etc), include base materials
            var mw=(MainWindow)sender;
            var item=(KeepCraftable)mw.CraftableSelector.CurrentItem;
            var craftable=item.Craftable;
            if(selected)
                foreach(var mat in CraftablesByName[craftable].Recipe.Materials.Select(m=>Items[m.Id].Name)){
                    Materials.First(m=>m.Material==mat).Keep=true;
                    var uses=UsedIn.First(m=>m.Material==mat).Uses;
                    if(!uses.Contains(craftable)) uses.Add(craftable);
                }
            else
                foreach(var mat in CraftablesByName[craftable].Recipe.Materials.Select(m=>Items[m.Id].Name)){
                    var uses=UsedIn.First(m=>m.Material==mat).Uses;
                    if(uses.Contains(craftable)) uses.Remove(craftable);
                    Materials.First(m=>m.Material==mat).Keep=uses.Any();
                }
        }
        public static void Save() {WriteAllText(SavePath,JsonConvert.SerializeObject(Keep));}
    }
    public class KeepOrDiscard:ObservableCollection<KeepCraftable> {}//TODO: remove horizalign/vertalign
    public class KeepCraftable{
        public KeepCraftable(string c,bool k){
            Craftable=c;
            Keep=k;
        }
        public string Craftable{get;set;}
        public bool Keep{get;set;}
        public static KeepCraftable Kc(string c,bool k) =>new KeepCraftable(c,k);
    }
    public class KeepUpdatedEventArgs:EventArgs{
        public KeepUpdatedEventArgs(string craftable,bool keep){
            Craftable=craftable;
            Keep=keep;
        }
        public string Craftable{get;set;}
        public bool Keep{get;set;}
    }
    public class UsedIn:ObservableCollection<Use> {}
    public class Use{
        public Use(string c,List<string> k){
            Material=c;
            Uses=k;
        }
        public string Material{get;set;}
        public List<string> Uses{get;set;}
        public static Use U(string c,List<string> k) =>new Use(c,k);
    }//TODO: make trash smaller
    public class KeepOrDiscardMaterial:ObservableCollection<KeepMaterial>{
        public static KeepOrDiscardMaterial ToKodm(IEnumerable i){
            var kodm=new KeepOrDiscardMaterial();
            foreach(var mat in i.Cast<KeepMaterial>()) kodm.Add(Km(mat.Material,mat.Keep));
            return kodm;
        }
    }
    public class KeepMaterial:INotifyPropertyChanged{
        private bool keep;
        public KeepMaterial(string c,bool k){
            Material=c;
            Keep=k;
        }
        public string Material{get;set;}
        public bool Keep{
            get {return keep;}
            set{
                keep=value;
                PropertyChanged?.Invoke(this,new PropertyChangedEventArgs("Keep"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public static KeepMaterial Km(string c,bool k) =>new KeepMaterial(c,k);
    }
}
