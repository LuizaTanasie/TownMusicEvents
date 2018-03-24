﻿using API.Models;
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
                if (updatedArtist.Picture1Url != "")
                {
                    artist.Picture1Url = updatedArtist.Picture1Url;
                }
                if (updatedArtist.Picture2Url != "")
                {
                    artist.Picture2Url = updatedArtist.Picture2Url;
                }
                if (updatedArtist.Picture3Url != "")
                {
                    artist.Picture3Url = updatedArtist.Picture3Url;
                }
                if (updatedArtist.Picture4Url != "")
                {
                    artist.Picture4Url = updatedArtist.Picture4Url;
                }
                if (updatedArtist.Picture5Url != "")
                {
                    artist.Picture5Url = updatedArtist.Picture5Url;
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
                userRepository.Update(user);
                artistRepository.Update(artist);
                unitOfWork.Save();

                return updatedArtist;
            }
        }

    }
}