using JoelHiltonMovies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JoelHiltonMovies.Controllers
{
    public class HomeController : Controller
    {
        private MovieResponseContext _context { get; set; }

        public HomeController(MovieResponseContext con)
        {
            _context = con;
        }

        //Get for index
        public IActionResult Index()
        {
            return View();
        }

        //Get for podcasts
        public IActionResult MyPodcasts()
        {
            return View();
        }

        //Get for entering movie
        [HttpGet("EnterMovie")]
        public IActionResult EnterMovie()
        {
            return View();
        }

        //Post for entering movie
        [HttpPost("EnterMovie")]
        public IActionResult EnterMovie(MovieResponse movieResponse)
        {
            if (ModelState.IsValid)
            {
                if (movieResponse.Category != null &&
                    movieResponse.Title != null &&
                    movieResponse.Year != 0 &&
                    movieResponse.Director != null &&
                    movieResponse.Rating != null &&
                    movieResponse.Title != "Independence Day")
                {
                    _context.MovieResponses.Add(movieResponse);
                    _context.SaveChanges();
                }
            }
            return View();
        }

        // GET: MovieResponses
        public async Task<IActionResult> List()
        {
            return View(await _context.MovieResponses.ToListAsync());
        }

        // GET: MovieResponses/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieResponse = await _context.MovieResponses.FindAsync(id);
            if (movieResponse == null)
            {
                return NotFound();
            }
            return View(movieResponse);
        }

        // POST: MovieResponses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MovieId,Category,Title,Year,Director,Rating,Edited,LentTo,Notes")] MovieResponse movieResponse)
        {
            if (id != movieResponse.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieResponse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieResponseExists(movieResponse.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(movieResponse);
        }

        // GET: MovieResponses/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieResponse = await _context.MovieResponses
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieResponse == null)
            {
                return NotFound();
            }

            return View(movieResponse);
        }

        // POST: MovieResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var movieResponse = await _context.MovieResponses.FindAsync(id);
            _context.MovieResponses.Remove(movieResponse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool MovieResponseExists(string id)
        {
            return _context.MovieResponses.Any(e => e.MovieId == id);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
