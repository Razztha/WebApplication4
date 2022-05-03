using System.Collections.Generic;
using System.Threading.Tasks;
using MetadataExtractor;
using VideoMetaData.Models;

namespace VideoMetaData.Services
{
    public interface IMetaDataReader
    {
        Task<List<VideoInfo>> ReadVideoMetaData();
        Task<IReadOnlyList<MetadataExtractor.Directory>> ReadVideoMetaDataLocal();
    }
}