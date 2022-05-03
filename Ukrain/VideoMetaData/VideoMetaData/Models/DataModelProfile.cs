using System.Collections.Generic;
using AutoMapper;
using MediaInfoDotNet;
using MediaInfoDotNet.Models;

namespace VideoMetaData.Models
{
    public class DataModelProfile : Profile
    {
        public DataModelProfile()
        {
            CreateMap<MediaFile, MediaFileModel>();
            CreateMap<VideoStream, VideoInfo>();
        }
    }
}