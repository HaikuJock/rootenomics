﻿using Haiku.MonoGameUI;
using Haiku.MonoGameUI.Layouts;
using Haiku.MonoGameUI.LayoutStrategies;
using Microsoft.Xna.Framework;
using RootNomicsGame.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RootNomicsGame.UI
{
    internal class StatsPanel : Panel
    {
        Label foodLabel;
        Label wealthLabel;
        Label juiceLabel;

        public StatsPanel(Rectangle frame)
            : base(frame, new FlexLayoutStrategy(new Flex
            {
                FlexDirection = FlexDirection.Row,
                ContentJustification = ContentJustification.SpaceAround,
                ItemAlignment = ItemAlignment.Center,
            }))
        {
            BackgroundColor = new Color(0xF5F5DC);

            var foodLayout = new LinearLayout(Orientation.Horizontal, 4);
            var foodTitle = new Label("Nutrients:", BodyFont);
            foodLabel = new Label("0");
            foodLayout.AddChildren(new[] { foodTitle, foodLabel });
            foodLabel.CenterYInParent();

            var wealthLayout = new LinearLayout(Orientation.Horizontal, 4);
            var wealthTitle = new Label("Growth:", BodyFont);
            wealthLabel = new Label("0");
            wealthLayout.AddChildren(new[] { wealthTitle, wealthLabel });
            wealthLabel.CenterYInParent();

            var juiceLayout = new LinearLayout(Orientation.Horizontal, 4);
            var juiceTitle = new Label("Healing:", BodyFont);
            juiceLabel = new Label("0");
            juiceLayout.AddChildren(new[] { juiceTitle, juiceLabel });
            juiceLabel.CenterYInParent();

            AddChildren(new[] {foodLayout, wealthLayout, juiceLayout});
        }

        internal void Update(SimulationState state)
        {
            foodLabel.Text = state.TotalFood.ToString();
            wealthLabel.Text = Math.Max(0, state.TotalWealth).ToString();
            juiceLabel.Text = state.TotalMagicJuice.ToString();
        }
    }
}
