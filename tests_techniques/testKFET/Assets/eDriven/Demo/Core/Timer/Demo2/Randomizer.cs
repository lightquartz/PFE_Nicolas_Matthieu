﻿using System;

namespace eDriven.Playground.Demo.Core.Timer.Demo2
{
    public class Randomizer
    {
        public static float RandomizeAround(float center, float amount)
        {
            return center + (-1 + new Random().Next(200) * 0.01f) * amount;
        }
    }
}