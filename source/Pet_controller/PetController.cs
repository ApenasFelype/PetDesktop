using PetDesktop;
using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Input;
using PetDesktop.source.UI_status;

namespace PetDesktop
{
    public class PetController
    {
        //estado atual do gato
        private PetState state = PetState.Walking;
        private int stateCounter = 0;

        //Necessidades
        public PetNeeds? Needs;

        //animação
        private PetAnimation animation;
        private int animationCounter = 0;

        //mudança no estado de idle
        private int IdleChance;

        //timer a cada update
        public DispatcherTimer? timer;

        //aleatorizando direção
        private Random random = new Random();
        private int direction = 1;

        //conectando com a janela e pegando o tamanho da tela atual
        private MainWindow window;

        private double screenWidth = SystemParameters.WorkArea.Width;
        private double screendHeight = SystemParameters.WorkArea.Height;
        private double ground = SystemParameters.WorkArea.Bottom;

        //gravidade
        double gravity = 0.5;
        double velocityY = 0;

        //interação com o mouse

        private bool isDragging = false;

        public PetController(MainWindow window)
        {
            this.window = window;
            Needs = new PetNeeds(this);
            animation = new PetAnimation();
        }

        public void ControllerMain()
        {
            timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromMilliseconds(30);
            timer.Tick += UpdatePet;

            timer.Start();
        }

        private void UpdatePet(object? sender, EventArgs e)
        {
            Needs.UpdateNeeds();
            

            ImFly();
            if (isDragging)
                return;

            GravityState();

            stateCounter++;
            switch (state)
            {
                case PetState.Walking:
                    MovePet();
                    break;

                case PetState.Idle:
                    IdlePet();
                    break;

                case PetState.Sleeping:
                    Sleeping();
                    break;
            }       
        }
        //gravidade + agarra e arrasta com o mouse + queda livre
        private void GravityState()
        {
            velocityY += gravity;

            double nextTop = window.Top + velocityY;

            if (state == PetState.Sleeping)
            {
                if (nextTop + window.Height >= ground + 32)
                {
                    window.Top = ground - window.Height + 51;
                    velocityY = 0;

                    return;
                }
            }
            else
            {
                if (nextTop + window.Height >= ground + 32)
                {
                    window.Top = ground - window.Height + 32;
                    velocityY = 0;

                    return;
                }
            }


            window.Top = nextTop;
        }

        private void ImFly()
        {
            if (window.Top + window.Height < ground + 31)
            {
                state = PetState.holding;
                window.CatImage.Source = animation.GetCatDrag();
                return;
            }
            if (state == PetState.holding)
            {
                IdleChance = 0;
                state = PetState.Idle;
            }
            
        }

        public void StartDragging()
        {
            isDragging = true;

            stateCounter = 0;
            animationCounter = 0;
        }

        

        public void StopDragging()
        {
            isDragging = false;

            velocityY = 0;
        }

        //mudando o state do gato
        public void ChangeState(PetState newState)
        {
            state = newState;
            stateCounter = 0;

            animationCounter = 0;
            animation.ResetAnimation();

            if (newState == PetState.Idle)
            {
                IdleChance = random.Next(2);
            }
        }

        private void ChooseNextState()
        {
            PetState newState = PetState.Idle;

            do
            {
                int choice = random.Next(100);

                if (choice <= 50)
                {
                    newState = PetState.Idle;
                }
                else if (choice <= 100)
                {
                    newState = PetState.Walking;
                }
            } while (newState == state);

            ChangeState(newState);

        }

        //ação + animação

        public void Sleeping()
        {
            window.CatImage.Source = animation.GetSleeping();

            Needs.Sleep += 0.1;

            if (Needs.Sleep >= 100)
            {
                Needs.Sleep = 100;
                ChooseNextState();
            }
        }
        public void IdlePet()
        {
            switch (IdleChance)
            {
                case 0:
                    window.CatImage.Source = animation.GetFrontIdle();
                    break;

                case 1:
                    window.CatImage.Source = animation.GetBackIdle();
                    break;
            }

            if (stateCounter >= 200)
            {
                ChooseNextState();
            }
        }

        public void MovePet()
        {
            //evitando a borda
            if (window.Left <= 0)
            {
                animationCounter = 0;
                animation.ResetAnimation();
                direction = 1;
            }

            if (window.Left + window.Width > screenWidth)
            {
                animationCounter = 0;
                animation.ResetAnimation();
                direction = -1;
            }

            //andando aleatoriamente
            if (random.Next(100) < 2)
            {
                animationCounter = 0;
                direction *= -1;
            }

            //colocando Animação na forma de andar para esquerda
            if (direction == -1)
            {
                animationCounter++;
                if (animationCounter >= 4)
                {
                    animationCounter = 0;
                    window.CatImage.Source = animation.GetNextWalkLeftFrame();
                }

            }

            if (direction == 1)
            {
                animationCounter++;
                if (animationCounter >= 4)
                {
                    animationCounter = 0;
                    window.CatImage.Source = animation.GetNextWalkRightFrame();
                }

            }

            //anda pra um lado caso o outro for Falso
            window.Left += direction;

            if (stateCounter >= 200)
            {
                ChooseNextState();
            }
        }
    }
}