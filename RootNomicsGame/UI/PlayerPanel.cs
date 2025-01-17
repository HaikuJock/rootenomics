﻿using Haiku.MathExtensions;
using Haiku.MonoGameUI;
using Haiku.MonoGameUI.Layouts;
using Haiku.MonoGameUI.LayoutStrategies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexturePackerLoader;
using TexturePackerMonoGameDefinitions;

namespace RootNomicsGame.UI
{
    internal class PlayerPanel : Panel
    {
        internal static PlayerPanel Instance = null;
        Label healthLabel;
        internal int Health { get; private set; }
        internal Button GrowButton;
        Label expectedDamageLabel;
        const int GrowPanelHeight = 60;

        public PlayerPanel(Rectangle frame, SpriteSheet uiTextureAtlas)
            : base(frame, new FlexLayoutStrategy(new Flex
            {
                FlexDirection = FlexDirection.Row,
                ContentJustification = ContentJustification.SpaceAround,
                ItemAlignment = ItemAlignment.Center,
            }))
        {
            Instance = this;
            BackgroundColor = new Color(0xF5F5DC);

            var healthTitle = new Label("Health:", BodyFont);
            Health = 100;
            healthLabel = new Label(Health.ToString());

            AddChild(healthTitle);
            AddChild(healthLabel);

            var expectedDamageLayout = new LinearLayout(Orientation.Horizontal, 8);
            var expectedDamageTitle = new Label("Next Turn Damage:", BodyFont);
            expectedDamageLabel = new Label("0");
            expectedDamageLayout.AddChildren(new[] { expectedDamageTitle, expectedDamageLabel });

            var growFrame = new Rectangle(0, 0, 128, GrowPanelHeight);
            var growLayout = new Layout(growFrame);

            GrowButton = new Button();
            GrowButton.SetBackground(
                uiTextureAtlas.NinePatch(UITextureAtlas.ButtonRedNormal),
                uiTextureAtlas.NinePatch(UITextureAtlas.ButtonRedActive),
                uiTextureAtlas.NinePatch(UITextureAtlas.ButtonRedSelected));
            growLayout.AddChild(GrowButton);
            GrowButton.Frame = new Rectangle(0, 0, 112, 44);
            GrowButton.SetForeground("End Turn");
            GrowButton.CenterInParent();

            AddChild(expectedDamageLayout);
            AddChild(growLayout);
        }

        internal void Update(int damage, int damageMin, int damageMax)
        {
            Health -= damage;
            Health = Health.Clamp(0, 100);
            healthLabel.Text = Health.ToString();

            UpdateExpectedNextTurnDamage(damageMin, damageMax);
        }

        internal void UpdateExpectedNextTurnDamage(int min, int max)
        {
            if (max == 0)
            {
                expectedDamageLabel.Text = "0";
            }
            else
            {
                expectedDamageLabel.Text = $"{min}-{max}";
            }
        }
    }
}
