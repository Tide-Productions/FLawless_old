﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GQLib
{
    abstract class Patterns
    {

        /// <summary>
        /// Patternzustand updaten
        /// </summary>
        public abstract void Update();
        public abstract void Draw(SpriteBatch spriteBatch);
    }
    class PolarPatterns : Patterns
    {
        int typeOB;
        int n;
        float accelerationOB;
        float angleChangeOB;
        float startSpeedOB;
        float angleChangeOS;
        int interval;
        Vector2 spawnPosition;
        float angle;
        List<PolarBullet> Pattern = new List<PolarBullet>();
        int timer;
        int counter;
        private String path;
        private ContentManager Content;

        public PolarPatterns(int typeOB, int numberOB, int intervalOS, float accelerationOB, float startAngleOS, float angleChangeOB, float startSpeedOB, Vector2 spawnPosition, String texturePath, ContentManager content, float angleChangeOS)
        {
            this.typeOB = typeOB;
            n = numberOB;
            this.spawnPosition = spawnPosition;
            this.accelerationOB = accelerationOB;
            this.angleChangeOB = angleChangeOB;
            this.startSpeedOB = startSpeedOB;
            this.angleChangeOS = angleChangeOS;
            interval = intervalOS;
            angle = startAngleOS;
            Content = content;
            path = texturePath;
        }

        public PolarPatterns(int typeOB, int numberOB, int intervalOS, float accelerationOB, float startAngleOS, float angleChangeOB, float startSpeedOB, Vector2 spawnPosition, String texturePath, ContentManager content)
        {
            this.typeOB = typeOB;
            n = numberOB;
            this.spawnPosition = spawnPosition;
            this.accelerationOB = accelerationOB;
            this.angleChangeOB = angleChangeOB;
            this.startSpeedOB = startSpeedOB;
            interval = intervalOS;
            angleChangeOS = 360 / n;
            angle = startAngleOS;
            Content = content;
            path = texturePath;
        }

        public override void Update()
        {

            //initalize the pattern
            if ((interval != 0) && (timer % interval == 0) && (counter <= n))
            {
                //create new bullets one by one
                angle += angleChangeOS;
                Pattern.Add(new PolarBullet(typeOB, path, startSpeedOB, angleChangeOB, accelerationOB, spawnPosition, angle, Content));
                counter++;
            }
            else if ((counter <= n) && (interval == 0))
            {

                //create n new bullets at once
                for (int i = 0; i < n; i++)
                {

                    Pattern.Add(new PolarBullet(typeOB, path, startSpeedOB, angleChangeOB, accelerationOB, spawnPosition, angle + i * angleChangeOS, Content));
                    counter++;
                }

            }

            //update the pattern
            for (int i = Pattern.Count - 1; i >= 0; i--)
            {
                if (Pattern[i].getPosition().X > 1000 || Pattern[i].getPosition().X < -100 || Pattern[i].getPosition().Y > 700 || Pattern[i].getPosition().Y < -100)
                {
                    //if (Pattern[i].getPosition().Y >= 370) GameStuff.Instance.score += 10;
                    Pattern.RemoveAt(i);
                }
                else if (Pattern[i].getAlive() == false) { Pattern.RemoveAt(i); }
                else { Pattern[i].Update(); }
            }

            //tick counter
            timer++;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (PolarBullet i in Pattern)
            {
                i.Draw(spriteBatch);
            }
        }
    }

}