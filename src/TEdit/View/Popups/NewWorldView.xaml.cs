﻿using System.Windows;
using TEdit.Terraria;
using TEdit.Terraria.Objects;

namespace TEdit.View.Popups
{
    /// <summary>
    /// Interaction logic for NewWorldView.xaml
    /// </summary>
    public partial class NewWorldView : Window
    {
        private readonly World _newWorld;
        public NewWorldView()
        {
            InitializeComponent();
            _newWorld = new World(1200, 4200, "TEdit World");
            _newWorld.Version = World.CompatibleVersion;
            _newWorld.GroundLevel = 350;
            _newWorld.RockLevel = 480;
            _newWorld.ResetTime();
            AddCharNames();
            DataContext = NewWorld;
        }

        private void AddCharNames()
        {
            _newWorld.CharacterNames.Add(new NpcName(17, "Harold"));
            _newWorld.CharacterNames.Add(new NpcName(18, "Molly"));
            _newWorld.CharacterNames.Add(new NpcName(19, "Dominique"));
            _newWorld.CharacterNames.Add(new NpcName(20, "Felicitae"));
            _newWorld.CharacterNames.Add(new NpcName(22, "Steve"));
            _newWorld.CharacterNames.Add(new NpcName(54, "Fitz"));
            _newWorld.CharacterNames.Add(new NpcName(38, "Gimut"));
            _newWorld.CharacterNames.Add(new NpcName(107, "Knogs"));
            _newWorld.CharacterNames.Add(new NpcName(108, "Fizban"));
            _newWorld.CharacterNames.Add(new NpcName(124, "Nancy"));

            // New for 1.2
            _newWorld.CharacterNames.Add(new NpcName(160, "Truffle"));
            _newWorld.CharacterNames.Add(new NpcName(178, "Steampunker"));
            _newWorld.CharacterNames.Add(new NpcName(207, "Dye Trader"));
            _newWorld.CharacterNames.Add(new NpcName(208, "Party Girl"));
            _newWorld.CharacterNames.Add(new NpcName(209, "Cyborg"));
            _newWorld.CharacterNames.Add(new NpcName(227, "Painter"));
            _newWorld.CharacterNames.Add(new NpcName(228, "Witch Doctor"));
            _newWorld.CharacterNames.Add(new NpcName(229, "Pirate"));
        }

        public World NewWorld
        {
            get { return _newWorld; }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
