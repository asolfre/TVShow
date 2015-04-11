﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TVShow.ViewModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Threading;
using TVShow.CustomPanels;
using TVShow.Events;

namespace TVShow.UserControls
{
    /// <summary>
    /// Logique d'interaction pour Movies.xaml
    /// </summary>
    public partial class Movies : UserControl
    {
        public Movies()
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await MoviesUcLoaded(s, e);
            };
        }

        private async Task MoviesUcLoaded(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MoviesViewModel;
            if (vm != null)
            {
                vm.MoviesInfosLoaded += MoviesInfosLoaded;
                vm.MoviesInfosLoading += MoviesInfosLoading;
                await vm.LoadNextMoviesInfos();
            }
        }

        private async void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            double sum = e.VerticalOffset + e.ViewportHeight;
            if (sum.Equals(e.ExtentHeight))
            {
                var vm = DataContext as MoviesViewModel;
                if (vm != null && !ProgressRing.IsActive)
                {
                    if (String.IsNullOrEmpty(vm.SearchMovies))
                    {
                        await vm.LoadNextMoviesInfos();
                    }
                    else
                    {
                        await vm.LoadNextMoviesInfos(vm.SearchMovies);
                    }
                }
            }
            if (e.VerticalOffset.Equals(0.0) && e.VerticalChange < 0.0)
            {
                var vm = DataContext as MoviesViewModel;
                if (vm != null && !ProgressRing.IsActive)
                {
                    vm.LoadPreviousMovies();
                }
            }
        }

        private void MoviesInfosLoading(object sender, EventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                ProgressRing.IsActive = true;

                #region Fading Opacity
                DoubleAnimationUsingKeyFrames opacityAnimation = new DoubleAnimationUsingKeyFrames();
                opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                PowerEase opacityEasingFunction = new PowerEase();
                opacityEasingFunction.EasingMode = EasingMode.EaseInOut;
                EasingDoubleKeyFrame startOpacityEasing = new EasingDoubleKeyFrame(1, KeyTime.FromPercent(0));
                EasingDoubleKeyFrame endOpacityEasing = new EasingDoubleKeyFrame(0.2, KeyTime.FromPercent(1.0),
                    opacityEasingFunction);
                opacityAnimation.KeyFrames.Add(startOpacityEasing);
                opacityAnimation.KeyFrames.Add(endOpacityEasing);
                ItemsList.BeginAnimation(OpacityProperty, opacityAnimation);
                #endregion

                if (this.NoMouvieFound.Visibility == Visibility.Visible)
                {
                    this.NoMouvieFound.Visibility = Visibility.Collapsed;
                }
            });
        }

        private void MoviesInfosLoaded(object sender, EventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                ProgressRing.IsActive = false;

                #region Fading Opacity

                DoubleAnimationUsingKeyFrames opacityAnimation = new DoubleAnimationUsingKeyFrames();
                opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                PowerEase opacityEasingFunction = new PowerEase();
                opacityEasingFunction.EasingMode = EasingMode.EaseInOut;
                EasingDoubleKeyFrame startOpacityEasing = new EasingDoubleKeyFrame(0.2, KeyTime.FromPercent(0));
                EasingDoubleKeyFrame endOpacityEasing = new EasingDoubleKeyFrame(1, KeyTime.FromPercent(1.0),
                    opacityEasingFunction);
                opacityAnimation.KeyFrames.Add(startOpacityEasing);
                opacityAnimation.KeyFrames.Add(endOpacityEasing);
                ItemsList.BeginAnimation(OpacityProperty, opacityAnimation);
                #endregion
                var vm = DataContext as MoviesViewModel;
                if (vm != null)
                {
                    if (!vm.Movies.Any() && !String.IsNullOrEmpty(vm.SearchMovies))
                    {
                        this.NoMouvieFound.Visibility = Visibility.Visible;
                    }

                    if (vm.Pagination != 1 && vm.Pagination != 2 && vm.Pagination != vm.PaginationLimit)
                    {
                        ScrollView.ScrollToVerticalOffset(ItemsList.ActualHeight / 2);
                    }
                }
            });
        }

        private async void Container_GotMouseCapture(object sender, MouseEventArgs e)
        {
            var vm = DataContext as MoviesViewModel;
            if (vm != null && ProgressRing.IsActive)
            {
                await vm.StopLoadingMoviesInfos();
            }
        }

        private async void Container_KeyDown(object sender, KeyEventArgs e)
        {
            var vm = DataContext as MoviesViewModel;
            if (vm != null && ProgressRing.IsActive)
            {
                await vm.StopLoadingMoviesInfos();
            }
        }

        private void ElasticWrapPanel_Loaded(object sender, RoutedEventArgs e)
        {
            var elasticWrapPanel = sender as ElasticWrapPanel;
            if (elasticWrapPanel != null)
            {
                elasticWrapPanel.NumberOfColumnsChanged += NumberOfColumnsChanged;
            }
        }

        private void NumberOfColumnsChanged(object sender, NumberOfColumnChangedEventArgs e)
        {
            var vm = DataContext as MoviesViewModel;
            if (vm != null && ProgressRing.IsActive)
            {
                vm.MaxMoviesPerPage = e.NumberOfColumns*5;
            }
        }
    }
}