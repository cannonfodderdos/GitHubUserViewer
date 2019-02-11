using AutoMapper;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserViewer.ViewModels;

namespace UserViewer.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(z => z.Name == null ? z.Login : z.Name)); // If name is null change to username.

            CreateMap<Repo, RepoViewModel>();
        }
    }
}