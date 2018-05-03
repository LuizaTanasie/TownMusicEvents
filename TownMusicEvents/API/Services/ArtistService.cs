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
                foreach (var artist in artistRepository.GetAll().Where(a=>a.LastFmId==null))
                {
                    artistModels.Add(new ArtistModel { ArtistId = artist.ArtistId,
                     Biography=artist.Biography, Facebook=artist.Facebook, Instagram=artist.Instagram, Name=artist.User.Name,
                     PictureUrl=artist.PictureUrl, Twitter=artist.Twitter, Website=artist.Website, YouTube= artist.YouTube});
                }
                return artistModels;
            }
        }

        public List<ArtistModel> SearchForArtist(string artistName)
        {
            var artistModels = new List<ArtistModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                foreach (var artist in artistRepository.GetAll()
                    .Where(a => a.LastFmId == null && a.User.Name.ToLower().StartsWith(artistName.ToLower())))
                {
                    artistModels.Add(new ArtistModel
                    {
                        ArtistId = artist.ArtistId,
                        Name = artist.User.Name,
                        PictureUrl = artist.PictureUrl
                    });
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
                    PictureUrl = artist.PictureUrl,
                    Twitter = artist.Twitter,
                    Website = artist.Website,
                    YouTube = artist.YouTube,
                    SoundCloud = artist.SoundCloud,
                    Spotify = artist.Spotify
                };

            }
        }

        public ArtistModel UpdateArtist(ArtistModel updatedArtist)
        {

            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var userRepository = unitOfWork.GetRepository<User>();
                var artist = artistRepository.Find(updatedArtist.ArtistId);
                var user = userRepository.Find(updatedArtist.ArtistId);
                if (artist == null || user == null) 
                {
                    return null;
                }
                if (updatedArtist.Biography != "")
                {
                    artist.Biography = updatedArtist.Biography;
                }
                if (updatedArtist.Facebook != "")
                {
                    artist.Facebook = updatedArtist.Facebook;
                }
                if (updatedArtist.Instagram != "")
                {
                    artist.Instagram = updatedArtist.Instagram;
                }
                if (updatedArtist.Name != "")
                {
                    user.Name = updatedArtist.Name;
                }
                if (updatedArtist.PictureUrl != "")
                {
                    artist.PictureUrl = updatedArtist.PictureUrl;
                }
                if (updatedArtist.Twitter != "")
                {
                    artist.Twitter = updatedArtist.Twitter;
                }
                if (updatedArtist.Website != "")
                {
                    artist.Website = updatedArtist.Website;
                }
                if (updatedArtist.YouTube != "")
                {
                    artist.YouTube = updatedArtist.YouTube;
                }
                if (updatedArtist.Spotify != "")
                {
                    artist.Spotify = updatedArtist.Spotify;
                }
                if (updatedArtist.SoundCloud != "")
                {
                    artist.SoundCloud = updatedArtist.SoundCloud;
                }
                userRepository.Update(user);
                artistRepository.Update(artist);
                unitOfWork.Save();

                return updatedArtist;
            }
        }

    }
}