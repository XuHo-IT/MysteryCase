using Mapster;
using MysteryCaseDomain;
using MysteryCaseShared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryCaseShared.Mapping
{
    public static class MappingConfig
    {
        public static void Configure()
        {
          
            TypeAdapterConfig<Case, CaseListDto>.NewConfig()
                .Ignore(dest => dest.IsSolved);

            TypeAdapterConfig<Clue, ClueDto>.NewConfig()
                .Ignore(dest => dest.IsUnlocked)
                .Map(dest => dest.Content, src => src.IsHidden ? null : src.Content)
                .Map(dest => dest.ImageUrl, src => src.IsHidden ? null : src.ImageUrl);
            
            TypeAdapterConfig<Case, CaseDetailDto>.NewConfig()
                .Ignore(dest => dest.UserPoints)
                .Ignore(dest => dest.CluesFoundCount)
                .Ignore(dest => dest.HasBeenSolved);
        }
    }
}
