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
            CreateMap<User, UserViewModel>();
            CreateMap<Repo, RepoViewModel>();
        }
    }
}