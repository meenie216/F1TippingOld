using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using F1Tipping;
using System.Collections;

namespace F1TippingUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Driver> QuallyResultsCollection;
        ObservableCollection<Driver> RaceResultsCollection;
        ObservableCollection<Driver> QuallyDrivers;
        ObservableCollection<Driver> RaceDrivers;

        private App app;
        private ListBox dragSource;

        public MainWindow()
        {
            InitializeComponent();

            app = (Application.Current as App);

            GrandPrixSelection.DataContext = app.Comp.Season.Races;
            TipsterSelection.DataContext = app.Comp.Tipsters;
            QualifyingDriverTip.DataContext = app.Comp.Season.Drivers;
            RaceDriverTip.DataContext = app.Comp.Season.Drivers;


            QuallyDrivers = new ObservableCollection<Driver>(app.Comp.Season.Drivers);
            RaceDrivers = new ObservableCollection<Driver>(app.Comp.Season.Drivers);

            QuallyResultsCollection = new ObservableCollection<Driver>();
            QuallyDriverList.DataContext = QuallyDrivers;
            QuallyResults.DataContext = QuallyResultsCollection;

            RaceResultsCollection = new ObservableCollection<Driver>();
            RaceDriverList.DataContext = RaceDrivers;
            RaceResults.DataContext = RaceResultsCollection;

            app.Comp.GenerateScores();
        }

        private void DragDriver(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;

            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        private void QuallyDriverList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = GetDataFromListBox(parent, e.GetPosition(parent));
            QuallyResultsCollection.Add((Driver)data);
            this.QuallyResults.Items.Add(data);
            ((IList)parent.ItemsSource).Remove(data);
        }
        private static object GetDataFromListBox(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }
                    if (element == source)
                    {
                        return null;
                    }
                }
                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        private void QuallyResults_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(Driver));
            ((IList)dragSource.ItemsSource).Remove(data);
            QuallyResultsCollection.Add((Driver)data);
            parent.Items.Add(data);
        }

        private void RaceResults_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(Driver));
            ((IList)dragSource.ItemsSource).Remove(data);
            RaceResultsCollection.Add((Driver)data);
            parent.Items.Add(data);
        }

        private void QuallyOK_Click(object sender, RoutedEventArgs e)
        {
            GrandPrix race = (GrandPrix)this.GrandPrixSelection.SelectedItem;

            if (race != null)
            {
                race.QualifyingResults = new List<Driver>();

                for (int i = 0; i < this.QuallyResultsCollection.Count; i++)
                {
                    race.QualifyingResults.Add(this.QuallyResultsCollection[i]);
                }

            }

            app.Comp.GenerateScores();
        }

        private void QuallyReset_Click(object sender, RoutedEventArgs e)
        {
            GrandPrix race = (GrandPrix)this.GrandPrixSelection.SelectedItem;

            if (race != null)
            {
                race.QualifyingResults = new List<Driver>();
            }

            QuallyResultsCollection.Clear();
            QuallyDrivers = new ObservableCollection<Driver>(app.Comp.Season.Drivers);
        }

        private void RaceOK_Click(object sender, RoutedEventArgs e)
        {
            GrandPrix race = (GrandPrix)this.GrandPrixSelection.SelectedItem;

            if (race != null)
            {
                race.RaceResults = new List<Driver>();

                for (int i = 0; i < this.RaceResultsCollection.Count; i++)
                {
                    race.RaceResults.Add(this.RaceResultsCollection[i]);
                }

            }

            app.Comp.GenerateScores();
        }

        private void RaceReset_Click(object sender, RoutedEventArgs e)
        {
            GrandPrix race = (GrandPrix)this.GrandPrixSelection.SelectedItem;

            if (race != null)
            {
                race.RaceResults = new List<Driver>();
            }

            RaceResultsCollection.Clear();
            RaceDrivers = new ObservableCollection<Driver>(app.Comp.Season.Drivers);
        }


        #region TippingEntryTab
        private void TippingEntryOK_Click(object sender, RoutedEventArgs e)
        {
            var tipster = (Tipster)this.TipsterSelection.SelectedItem;

            var quallyTip = (Driver)this.QualifyingDriverTip.SelectedItem;
            var raceTip = (Driver)this.RaceDriverTip.SelectedItem;

            var grandPrix = (GrandPrix)this.GrandPrixSelection.SelectedItem;

            app.Comp.AddTip(tipster, grandPrix, new Tip() { Qualifying = quallyTip, Race = raceTip });
        }

        private void AddNewTipster_Click(object sender, RoutedEventArgs e)
        {
            var addTipsterWindow = new AddTipster();
            addTipsterWindow.ShowDialog();
        }

        #endregion

        private void UpdateSummary(object sender, RoutedEventArgs e)
        {
            if (this.GrandPrixSelection.SelectedItem != null)
            {
                GrandPrix grandPrix = (GrandPrix)this.GrandPrixSelection.SelectedItem;

                var tippingSummary =
                    app.Comp.Tipsters
                    .Where(t => t.Tips.ContainsKey(grandPrix))
                    .Select(t =>
                        new
                        {
                            Tipster = t,
                            Qualifying = t.Tips.FirstOrDefault(a => a.Key == grandPrix).Value.Qualifying,
                            Race = t.Tips.FirstOrDefault(a => a.Key == grandPrix).Value.Race
                        });

                tippingSummaryGrid.DataContext = tippingSummary;
            }
        }

        private void GrandPrixSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSummary(sender, e);
            BuildSummaryForRace();
        }

        private void tabItem5_GotFocus(object sender, RoutedEventArgs e)
        {
            
            this.scoreSummaryGrid.DataContext = app.Comp.Tipsters;
        }

        private void RaceDriverList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = GetDataFromListBox(parent, e.GetPosition(parent));
            RaceResultsCollection.Add((Driver)data);
            this.RaceResults.Items.Add(data);
            ((IList)parent.ItemsSource).Remove(data);
        }

        private void tabItem6_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void BuildSummaryForRace()
        {
            GrandPrix race = (GrandPrix)this.GrandPrixSelection.SelectedItem;

            if (race != null)
            {
                this.RaceSummary.Text = app.Comp.BuildSummaryForGrandPrix(race); // race.GenerateSummary();
            }
        }

        private void tippingSummaryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }





    }
}
