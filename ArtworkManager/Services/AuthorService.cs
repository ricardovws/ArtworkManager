﻿using ArtworkManager.Models;
using ArtworkManager.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Services
{
    public class AuthorService
    {
        private readonly ArtworkManagerContext _context;

        public AuthorService(ArtworkManagerContext context)
        {
            _context = context;
        }

        public List<Author> ShowAllAuthors()
        {
            return _context.Author.Include(obj => obj.Team).ToList();
        }

        public List<Author> ShowOnlyAAuthor(Author author)
        {
            return _context.Author.Include(obj => obj.Team).Where(obj => obj.Id == author.Id).ToList();
        }


        public List<Artwork> ShowAllArtworks(Author author, string sortColumn, string sortColumnDirection, string searchValue, int skip, int pageSize, out int recordsTotal)
        {

            var artwork = _context.Artwork.Where(obj => obj.Owner == author);
            recordsTotal = artwork.Count();
            //Sorting  
            if (sortColumn== "code")
            {
                if (sortColumnDirection.ToUpper() == "ASC")
                {
                    artwork = artwork.OrderBy(x => x.Code);
                }
                else
                {
                    artwork = artwork.OrderByDescending(x => x.Code);
                }
            }
            else
            {
                if (sortColumnDirection.ToUpper() == "ASC")
                {
                    artwork = artwork.OrderBy(x => x.Status).ThenBy(x => x.Code);
                }
                else
                {
                    artwork = artwork.OrderByDescending(x => x.Status).ThenBy(x => x.Code);
                }
            }
            var data = artwork.Skip(skip).Take(pageSize).ToList();
            return data;
        }

        public void AddPublicationCode(Author owner, string publicationcode)
        {
            
            var objj = _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);

            var obj1 = objj;

            _context.Artwork.Remove(objj);
            _context.SaveChanges();
            
            
            obj1.PublicationCode = publicationcode;


            _context.Artwork.Add(obj1);
            _context.SaveChanges();

        }

        public Author FindAuthorById (int id)
        {
            return _context.Author.First(obj => obj.Id == id);
            
        }

        public Author FindAuthorByIdFullObject(int id)
        {
            
            return _context.Author.Include(obj => obj.Artworks).First(obj => obj.Id == id);
        }

        public User FindUserById(int id)
        {
            return _context.User.First(obj => obj.Id == id);

        }

        public Author FindAuthorByUser(User user)
        {
            return _context.Author.First(obj => obj.Id == user.OwnerId);

        }

        public Artwork GetACode (Author owner)
        {
            return _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);

        }

        //This method below uses a code from a existing publication code! (using for reference the last publication code used...)

        public void UseCode (Author owner)
        {
            var objj = _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);
            
            var obj1 = objj;
            
            _context.Artwork.Remove(objj);
            _context.SaveChanges();
            obj1.Status = Models.Enums.ArtworkStatus.Used;
            obj1.BirthDate = DateTime.Now;
           try { 
            obj1.PublicationCode = _context.Artwork.Last(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.Used).PublicationCode;
            }
            catch
            {
            obj1.PublicationCode = _context.Artwork.Last(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse).PublicationCode;
            }


            _context.Artwork.Add(obj1);
            _context.SaveChanges();
        }


        //This method below uses as reference a new publication code...
        public void UseNewCode(Author owner)
        {
            var objj = _context.Artwork.First(obj => obj.Owner == owner && obj.Status == Models.Enums.ArtworkStatus.FreeToUse);

            var obj1 = objj;

            _context.Artwork.Remove(objj);
            _context.SaveChanges();
            
            obj1.Status = Models.Enums.ArtworkStatus.Used;
            obj1.BirthDate = DateTime.Now;
            



            _context.Artwork.Add(obj1);
            _context.SaveChanges();
        }

        public void Update(int id, Artwork obj)
        {
            if (!_context.Artwork.Any(x=> x.Id == obj.Id && x.Status == Models.Enums.ArtworkStatus.Used))
            {
                throw new NotFoundException("You cannot update information from a non-used artwork.");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
     
        }

        public Artwork FindArtworkById(int id)
        {
            return _context.Artwork.First(obj => obj.Id == id);

        }

        public void InsertAuthor(Author author)
        {
                     
            _context.Add(author);
            _context.SaveChanges();

        }

        public int GetIdFree()
        {
            var authoriD = _context.Author.Last();
            int iD = authoriD.Id;
            int Id = iD + 1;
            return Id;
        }

        public void UpdateAuthor (Author author)
        {
            if (!_context.Author.Any(x=> x.Id == author.Id))
            {
                throw new NotFoundException("Id not found!");
            }
            try { 
            _context.Update(author);
            _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        public void Pot (Author author)
        {
            int n = 2000; // i < "n" ---> "n" represents total number of codes ownered by the author.

            for (int i=0;i<n;i++)
            {
                var someArtworkCode = _context.ArtworkCode.First();
                _context.ArtworkCode.Remove(someArtworkCode);
                Artwork artwork = new Artwork();
                artwork.Id = someArtworkCode.Id;
                artwork.Code = someArtworkCode.ArtworkCodeCode;
                artwork.OwnerID = author.Id;
                author.AddCodeFromPot(artwork);
                _context.SaveChanges();
            }



        }
       


    }
}
