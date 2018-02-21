using API.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;

namespace API.Services
{
    public class ArtistService
    {
        public List<ArtistModel> GetAllArtists()
        {
            var artistModels = new List<ArtistModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                foreach (var artist in artistRepository.GetAll())
                {
                    artistModels.Add(new ArtistModel { ArtistId = artist.ArtistId,
                      Biography=artist.Biography, Facebook=artist.Facebook, Instagram=artist.Instagram, Name=artist.User.Name,
                     Picture1Url=artist.Picture1Url, Picture2Url=artist.Picture2Url, Picture3Url=artist.Picture3Url, Picture4Url=artist.Picture4Url,
                     Picture5Url=artist.Picture5Url, Twitter=artist.Twitter, Website=artist.Website, YouTube= artist.YouTube});
                }
                return artistModels;
            }
        }

        public ArtistModel GetArtist(int id)
        {

            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var artist = artistRepository.Find(id);
                if (artist == null)
                {
                    return null;
                }
                return new ArtistModel
                {
                    ArtistId = artist.ArtistId,
                    Biography = artist.Biography,
                    Facebook = artist.Facebook,
                    Instagram = artist.Instagram,
                    Name = artist.User.Name,
                    Picture1Url = artist.Picture1Url,
                    Picture2Url = artist.Picture2Url,
                    Picture3Url = artist.Picture3Url,
                    Picture4Url = artist.Picture4Url,
                    Picture5Url = artist.Picture5Url,
                    Twitter = artist.Twitter,
                    Website = artist.Website,
                    YouTube = artist.YouTube
                };

            }
        }
    }
}