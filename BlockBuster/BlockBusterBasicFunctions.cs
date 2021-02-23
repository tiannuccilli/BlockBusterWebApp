using BlockBuster.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlockBuster
{
    public class BlockBusterBasicFunctions
    {
        public static List<Movie> GetAllMoviesFull()
        {
            using (var db = new SE407_BlockBusterContext())
            {
                var movies = db.Movies
                    .Include(movies => movies.Director)
                    .Include(movies => movies.Genre)
                    .ToList();

                return movies;
            }
        }
        public static Movie GetMovieById(int id)
        {
            using(var db = new SE407_BlockBusterContext())
            {
                return db.Movies.Find(id);

            }
        }

        public static List<Movie> GetAllMovies()
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Movies.ToList();
            }
        }
        public static List<Movie> GetAllCheckedOutMovies()
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Movies
                .Join(db.Transactions,
                 m => m.MovieId,
                 t => t.Movie.MovieId,
                 (m, t) => new
                 {
                     MovieId = m.MovieId,
                     Title = m.Title,
                     ReleaseYear = m.ReleaseYear,
                     GenreId = m.GenreId,
                     DirectorId = m.DirectorId,
                     checkedIn = t.CheckedIn
                 }).Where(w => w.checkedIn == "N")
                 .Select(m => new Movie
                 {
                     MovieId = m.MovieId,
                     Title = m.Title,
                     ReleaseYear = m.ReleaseYear,
                     GenreId = m.GenreId,
                     DirectorId = m.DirectorId
                 }).ToList();
            }
        }
        public static List<Movie> GetAllGenreMovies(String genre)
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Movies
                .Join(db.Genres,
                 m => m.GenreId,
                 g => g.GenreId,
                 (m, g) => new
                 {
                     MovieId = m.MovieId,
                     Title = m.Title,
                     ReleaseYear = m.ReleaseYear,
                     GenreId = m.GenreId,
                     DirectorId = m.DirectorId,
                     genreDescr = g.GenreDescr
                 }).Where(w => w.genreDescr == genre)
                 .Select(m => new Movie
                 {
                     MovieId = m.MovieId,
                     Title = m.Title,
                     ReleaseYear = m.ReleaseYear,
                     GenreId = m.GenreId,
                     DirectorId = m.DirectorId
                 }).ToList();
            }
        }
        public static List<Movie> GetAllDirectorMovies(String director)
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Movies
                .Join(db.Directors,
                 m => m.DirectorId,
                 d => d.DirectorId,
                 (m, d) => new
                 {
                     MovieId = m.MovieId,
                     Title = m.Title,
                     ReleaseYear = m.ReleaseYear,
                     GenreId = m.GenreId,
                     DirectorId = m.DirectorId,
                     dirLastName = d.LastName
                 }).Where(w => w.dirLastName == director)
                 .Select(m => new Movie
                 {
                     MovieId = m.MovieId,
                     Title = m.Title,
                     ReleaseYear = m.ReleaseYear,
                     GenreId = m.GenreId,
                     DirectorId = m.DirectorId
                 }).ToList();
            }
        }
    }

}
