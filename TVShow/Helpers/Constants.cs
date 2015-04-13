﻿using System.IO;

namespace TVShow.Helpers
{
    /// <summary>
    /// Constants of the project
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Directory of movies covers
        /// </summary>
        public static readonly string CoverMoviesDirectory = Path.GetTempPath() + "TVShow\\Covers\\";

        /// <summary>
        /// Directory of poster movies
        /// </summary>
        public static readonly string PosterMovieDirectory = Path.GetTempPath() + "TVShow\\Posters\\";

        /// <summary>
        /// Directory of movies' background image 
        /// </summary>
        public static readonly string BackgroundMovieDirectory = Path.GetTempPath() + "TVShow\\Backgrounds\\";

        /// <summary>
        /// Directory of directors images
        /// </summary>
        public static readonly string DirectorMovieDirectory = Path.GetTempPath() + "TVShow\\Directors\\";

        /// <summary>
        /// Directory of actors images
        /// </summary>
        public static readonly string ActorMovieDirectory = Path.GetTempPath() + "TVShow\\Actors\\";

        /// <summary>
        /// Directory of torrents
        /// </summary>
        public static readonly string TorrentDirectory = Path.GetTempPath() + "TVShow\\Torrents\\";

        /// <summary>
        /// Directory of downloaded movies
        /// </summary>
        public static readonly string MovieDownloads = Path.GetTempPath() + "TVShow\\Downloads\\";

        /// <summary>
        /// YtsApiEndpoint Endpoint to YTS
        /// </summary>
        public static readonly string YtsApiEndpoint = "http://yts.to/api/v2";

        /// <summary>
        /// Maximum width of the movie title in the main interface until sliding
        /// </summary>
        public const int MaxWidthBeforeSlidingTitle = 160;

        /// <summary>
        /// Font size of the movie title in the main interface
        /// </summary>
        public const int MovieTitleFontSize = 18;

        /// <summary>
        /// We want at least 5 rows to be able to scroll inside the main window
        /// </summary>
        public const int NumberOfRowsPerPage = 5;

        /// <summary>
        /// ConnectionErrorPropertyName
        /// </summary>
        public const string ConnectionErrorPropertyName = "ConnectionError";

        /// <summary>
        /// SearchMoviesFilterPropertyName
        /// </summary>
        public const string SearchMoviesFilterPropertyName = "SearchMoviesFilter";

        /// <summary>
        /// IsDownloadingMoviePropertyName
        /// </summary>
        public const string IsDownloadingMoviePropertyName = "IsDownloadingMovie";
        
        public enum FileType
        {
            BackgroundImage = 0,
            PosterImage = 1,
            CoverImage = 2,
            DirectorImage = 3,
            ActorImage = 4,
            TorrentFile = 5
        }
    }
}
