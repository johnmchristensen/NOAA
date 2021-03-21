using System;
using AutoMapper;
using NOAA.GHCND.Data;

namespace NOAA.GHCND.Adapters
{
    public class AdaptersProfile : Profile
    {   
        public AdaptersProfile()
        {
            this.CreateMap<IDataPointDTOGetter, DataPointDTO>();
        }
    }
}
    