namespace BFCalc{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using static EvoData;
    using static ItemData;
    using static KeepDiscard;
    using static UnitData;
    public partial class MainWindow{//TODO:sort units/items based on element, batch (how), show ids, resizing
        public MainWindow(){
            Data.Initialise();
            ShowMats=new[]{true,true};
            CurrentUnit=UnitData.Units.First().Value;
            InitializeComponent();
            SkillType.SelectedIndex=0;
            UnitSelector.SelectedIndex=0;
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        public bool[] ShowMats{get;set;}
        private void OnItemChanged(object sender,SelectionChangedEventArgs args){
            if(args.AddedItems.Count>0) CurrentItem=ItemsByName[(string)args.AddedItems[0]];
            Items.DataContext=CurrentItem;
            ItemSelector.SelectedIndex=-1;
        }
        private void OnUnitChanged(object sender,SelectionChangedEventArgs args){
            if(args?.AddedItems.Count>0){
                var name=(string)args.AddedItems[0];
                CurrentUnit=UnitsByName[name];
                CurrentEvo=EvosByName.ContainsKey(name)?EvosByName[name]:UnitsByName[name];
                UnitName.Text=name;
            }
            Evolution.DataContext=CurrentEvo;
            CurrentSkill=SkillType.SelectedIndex==0?UnitsByName[CurrentUnit.Name].Ls:UnitsByName[CurrentUnit.Name].Es;
            Skills.DataContext=CurrentSkill;
            UnitSelector.SelectedIndex=-1;
        }
        private void OnBbTypeChanged(object sender,SelectionChangedEventArgs args){
            //Bbs.DataContext=
        }
        private void OnLsEsChanged(object sender,SelectionChangedEventArgs args) {OnUnitChanged(sender,null);}
        private void OnCraftablesOnlyChecked(object sender,RoutedEventArgs e){
            ItemSelector.ItemsSource=CraftableNames;
        }
        private void OnCraftablesOnlyUnchecked(object sender,RoutedEventArgs e){
            ItemSelector.ItemsSource=ItemNames;
        }
        private void OnMaterialDoubleClicked(object sender,MouseButtonEventArgs e){
            if(Materials.SelectedItems.Count>0)
                ItemSelector.SelectedIndex=
                    ((List<string>)ItemSelector.ItemsSource).ToList()
                        .FindIndex(i=>i==(string)Materials.SelectedItems[0]);
        }
        private void OnUsedInDoubleClicked(object sender,MouseButtonEventArgs e){
            if(UsedIn.SelectedItems.Count>0)
                ItemSelector.SelectedIndex=
                    ((List<string>)ItemSelector.ItemsSource).FindIndex(i=>i==(string)UsedIn.SelectedItems[0]);
        }
        private void OnEvolveToClicked(object sender,MouseButtonEventArgs e){
            OnUnitChanged(sender,new SelectionChangedEventArgs(TextInputEvent,new List<string>(),new List<string>{EvolvesTo.Text}));
        }
        private void OnQuickSearchChanged(object sender,SelectionChangedEventArgs args){
            QuickSearchResult.Text=Keep.First(k=>k.Craftable==(string)QuickSearch.SelectionBoxItem).Keep?"Keep":"Discard";
        }
        private void OnBbChanged(object sender,SelectionChangedEventArgs args){
            //QuickSearchResult.Text=KeepDiscard.Keep.First(k=>k.Craftable==(string)QuickSearch.SelectionBoxItem).Keep?"Keep":"Discard";
        }
        private void OnSaveClicked(object sender,RoutedEventArgs args) {Save();}
        private void OnKeepChecked(object sender,RoutedEventArgs args){
            UpdateUsedIn(this,args,true);
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        private void OnKeepUnchecked(object sender,RoutedEventArgs args){
            UpdateUsedIn(this,args,false);
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        private void AddDiscardToSource(object sender,RoutedEventArgs args){
            ShowMats[1]=true;
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        private void TakeDiscardFromSource(object sender,RoutedEventArgs args){
            ShowMats[1]=false;
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        private void AddKeepToSource(object sender,RoutedEventArgs args){
            ShowMats[0]=true;
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        private void TakeKeepFromSource(object sender,RoutedEventArgs args){
            ShowMats[0]=false;
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        private void OnFilterChanged(object sender,RoutedEventArgs args){
            MaterialKeepOrDiscard.Items.Filter=ShowMaterial;
        }
        /*private void OnFilterChanged(object sender,RoutedEventArgs args){
            FilterMaterials(Filter.Text,Regex.IsChecked??false);
            UpdateFilteredMaterials2();
        }//TODO: do collections auto-change?
        private void AddDiscardToSource(object sender,RoutedEventArgs args){
            ShowMatsToDiscard=true;
            UpdateFilteredMaterials2();
        }
        private void TakeDiscardFromSource(object sender,RoutedEventArgs args){
            ShowMatsToDiscard=false;
            UpdateFilteredMaterials2();
        }
        private void AddKeepToSource(object sender,RoutedEventArgs args){
            ShowMatsToKeep=true;
            UpdateFilteredMaterials2();
        }
        private void TakeKeepFromSource(object sender,RoutedEventArgs args){
            ShowMatsToKeep=false;
            UpdateFilteredMaterials2();
        }
        //TODO: can xaml pass args?
        public void UpdateFilteredMaterials2(){
            switch((ShowMatsToKeep?2:0)+(ShowMatsToDiscard?1:0)) {
                case 0:
                    FilteredMaterials2=new KeepOrDiscardMaterial();
                    break;
                case 1:
                    FilteredMaterials2=ToKodm(FilteredMaterials.Where(m=>!m.Keep));
                    break;
                case 2:
                    FilteredMaterials2=ToKodm(FilteredMaterials.Where(m=>m.Keep));
                    break;
                case 3:
                    FilteredMaterials2=ToKodm(FilteredMaterials);
                    break;
            }
            MaterialKeepOrDiscard.ItemsSource=FilteredMaterials2;*/
//Children are not accepted
        public bool ShowMaterial(object item){
            var p=item as KeepMaterial;
            return (p!=null)&&ShowMats[p.Keep?0:1]&&p.Material.Contains(Filter.Text);
        }
    }
}
