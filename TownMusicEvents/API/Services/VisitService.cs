using API.Models;
using API.Validation;
using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;

namespace API.Services
{
    public class VisitService
    {
        public Visit AddVisit(int artistId, int fanId, bool hasClickedALink)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var visitRepository = unitOfWork.GetRepository<Visit>();
                Visit visit = new Visit { ArtistId = artistId, FanId = fanId, Date = DateTime.Now, HasClickedALink = hasClickedALink };
                visitRepository.Add(visit);
                unitOfWork.Save();
                return visit;
            }

        }

        public ArtistStatisticsModel GetNumberOfVisitsForArtist(int artistId, int when)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var visitRepository = unitOfWork.GetRepository<Visit>();
                if (when == (int)WhenEnum.LASTWEEK)
                {
                    var lastWeek = DateTime.Today.AddDays(-7);
                    int numberOfVisits = visitRepository.GetAll().Where(r => r.ArtistId == artistId && r.Date >= lastWeek).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                if (when == (int)WhenEnum.LASTMONTH)
                {
                    var lastMonth = DateTime.Today.AddDays(-30);
                    int numberOfVisits = visitRepository.GetAll().Where(r => r.ArtistId == artistId && r.Date >= lastMonth).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                if (when == (int)WhenEnum.LASTYEAR)
                {
                    var lastYear = DateTime.Today.AddDays(-356);
                    int numberOfVisits = visitRepository.GetAll().Where(r => r.ArtistId == artistId && r.Date >= lastYear).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                throw new InvalidModelException("Invalid time period");
            }
        }

        public ArtistStatisticsModel GetNumberOfClickedLinksForArtist(int artistId, int when)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var visitRepository = unitOfWork.GetRepository<Visit>();
                if (when == (int)WhenEnum.LASTWEEK)
                {
                    var lastWeek = DateTime.Today.AddDays(-7);
                    int numberOfVisits = visitRepository.GetAll().Where(r => r.ArtistId == artistId 
                    && r.Date >= lastWeek && r.HasClickedALink == true).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                if (when == (int)WhenEnum.LASTMONTH)
                {
                    var lastMonth = DateTime.Today.AddDays(-30);
                    int numberOfVisits = visitRepository.GetAll().Where(r => r.ArtistId == artistId 
                    && r.Date >= lastMonth && r.HasClickedALink == true).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                if (when == (int)WhenEnum.LASTYEAR)
                {
                    var lastYear = DateTime.Today.AddDays(-356);
                    int numberOfVisits = visitRepository.GetAll().Where(r => r.ArtistId == artistId 
                    && r.Date >= lastYear && r.HasClickedALink == true).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                throw new InvalidModelException("Invalid time period");
            }
        }
    }
}