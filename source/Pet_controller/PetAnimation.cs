using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PetDesktop
{
    public class PetAnimation
    {
        //Frame Atual e resetando ele
        private int currentFrame = 0;
        public void ResetAnimation()
        {
            currentFrame = 0;
        }
        
        //animação de andar para esquerda e retornando ela
        private BitmapImage[] Walk_LeftSprite =
        {
            new BitmapImage(new Uri("/source/Sprites/Cat_move_left/cat_walk_left1.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_left/cat_walk_left2.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_left/cat_walk_left3.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_left/cat_walk_left4.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_left/cat_walk_left5.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_left/cat_walk_left6.png", UriKind.Relative))
        };

        public BitmapImage GetNextWalkLeftFrame()
        {
            BitmapImage sprite = Walk_LeftSprite[currentFrame];
            currentFrame++;

            if (currentFrame >= Walk_LeftSprite.Length)
            {
                currentFrame = 0;
            }

            return sprite;
        }

        //animação de andar para direita e retornando ela
        private BitmapImage[] Walk_RightSprite =
        {
            new BitmapImage(new Uri("/source/Sprites/Cat_move_right/cat_walk_right1.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_right/cat_walk_right2.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_right/cat_walk_right3.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_right/cat_walk_right4.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_right/cat_walk_right5.png", UriKind.Relative)),
            new BitmapImage(new Uri("/source/Sprites/Cat_move_right/cat_walk_right6.png", UriKind.Relative))
        };

        public BitmapImage GetNextWalkRightFrame()
        {
            BitmapImage sprite = Walk_RightSprite[currentFrame];
            currentFrame++;

            if (currentFrame >= Walk_RightSprite.Length)
            {
                currentFrame = 0;
            }

            return sprite;
        }

        //parte Idle do gato + algumas animações Idle
        public BitmapImage GetFrontIdle()
        {
            BitmapImage sprite = new BitmapImage(new Uri("/source/Sprites/Cat_idle/cat_idle_front.png", UriKind.Relative));

            return sprite;
        }
        public BitmapImage GetBackIdle()
        {
            BitmapImage sprite = new BitmapImage(new Uri("/source/Sprites/Cat_idle/cat_idle_back.png", UriKind.Relative));

            return sprite;
        }

        //animação de ta sendo segurado
        public BitmapImage GetCatDrag()
        {
            BitmapImage sprite = new BitmapImage(new Uri("/source/Sprites/Cat_Drag/cat_drag3.png", UriKind.Relative));

            return sprite;
        }
    }
}
