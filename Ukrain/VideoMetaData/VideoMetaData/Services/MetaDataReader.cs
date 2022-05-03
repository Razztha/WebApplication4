using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using MediaInfoDotNet;
using MediaToolkit;
using MetadataExtractor;
using MetadataExtractor.Formats.Avi;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.FileSystem;
using MetadataExtractor.Formats.QuickTime;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using VideoMetaData.Models;
using Directory = MetadataExtractor.Directory;

namespace VideoMetaData.Services
{
    public class MetaDataReader : IMetaDataReader
    {
        private readonly IMapper _mapper;

        public MetaDataReader(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<List<VideoInfo>> ReadVideoMetaData()
        {
            // using media info
            string fileNameAndPath = "./video2.mp4";
            var myVideo = new MediaFile(fileNameAndPath);
            var mappedData = _mapper.Map<List<VideoInfo>>(myVideo.Video);
            return mappedData;
        }
        
        public async Task<IReadOnlyList<Directory>> ReadVideoMetaDataLocal()
        {
            // using metadata extractor
            var byteArray = await File.ReadAllBytesAsync("./meta_video1.mp4");
            Stream stream = new MemoryStream(byteArray);

            var directories = QuickTimeMetadataReader.ReadMetadata(stream);
            var directories2 = new List<Directory>();

            // using (var stream1 = new FileStream("./meta_video.mp4", FileMode.Open, FileAccess.Read, FileShare.Read))
            //     directories2.AddRange(ImageMetadataReader.ReadMetadata(stream1));
            var directories1 = ImageMetadataReader.ReadMetadata("./meta3.jpg");
            
            // using tag lib
            var tfile = TagLib.File.Create(@"./meta_video.mp4");

            // using media tool kit
            var inputFile = new MediaToolkit.Model.MediaFile {Filename = @"./meta_video.mp4"};
            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }
            var json =  JsonSerializer.Serialize(directories1);
            await File.WriteAllTextAsync("metadata.json", json);
                
            var inputFile1 = new MediaToolkit.Model.MediaFile {Filename = @"./test1.mp4"};
            var tfile1 = TagLib.File.Create(@"./meta_video2.mkv");

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }

            Console.WriteLine(inputFile.Metadata);    
            return directories;
        }
    }
}