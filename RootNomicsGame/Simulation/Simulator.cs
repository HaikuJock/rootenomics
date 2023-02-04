﻿using EconomySim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RootNomicsGame.Simulation
{
    internal class Simulator
    {
        Economy economy;

        internal Simulator()
        {
        }

        internal SimulationState Initialize(IDictionary<string, int> agentTypeCount)
        {
            // Create RootNomicEconomy
            // Create Markets
            // Add Markets to RootNomicEconomy
            // Construct Market with RootNomicEconomy
            // init Market with MarketData

            // or

            // Create DoranAndParberryEconomy - has some data in already
            economy = new DoranAndParberryEconomy();

            return CalculateSimulationState();
        }

        internal SimulationState Simulate(IDictionary<string, int> agentTypeCounts, int healingForPlants, int healingForPlayer)
        {
            economy.enforceAgentTypeCounts("default", agentTypeCounts);
            var market = economy.getMarket("default");
            if (healingForPlants > 0)
            {
                // add money to the economy
                market.addMoney(healingForPlants);
            }
            if (healingForPlayer > 0)
            {
                // remove tools from the economy
                market.removeGood("tools", healingForPlayer);
            }
            //var state = CalculateSimulationState();
            //var typeIds = Configuration.InitialAgentTypeCount.Keys.ToList();

            //foreach (var type in typeIds)
            //{
            //    System.Diagnostics.Debug.WriteLine($"{type}: {state.AgentTypeCounts[type]}");
            //}

            economy.simulate(200);

            return CalculateSimulationState();
        }

        private SimulationState CalculateSimulationState()
        {
            var market = economy.getMarket("default");
            var allGoodsCounts = market.countAllGoods();
            var result = new SimulationState
            {
                TotalWealth = allGoodsCounts["wealth"],
                TotalFood = allGoodsCounts["food"],
                TotalMagicJuice = allGoodsCounts["tools"]
            };

            var typeIds = Configuration.InitialAgentTypeCount.Keys.ToList();

            foreach (var type in typeIds)
            {
                result.AgentTypeCounts[type] = 0;
            }
            
            foreach (var agent in market._agents)
            {
                if (typeIds.Contains(agent.className))
                {
                    result.Agents.Add(new Agent
                    {
                        Id = agent.id.ToString(),
                        Type = agent.className,
                        Wealth = (int)Math.Round(agent.money),
                    });
                    result.AgentTypeCounts[agent.className] = result.AgentTypeCounts[agent.className] + 1;
                }
            }

            return result;
        }
    }
}
