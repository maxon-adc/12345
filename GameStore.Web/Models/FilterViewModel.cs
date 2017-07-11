﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GameStore.Web.Infrastructure.Enums;

namespace GameStore.Web.Models
{
    public class FilterViewModel
    {
        public List<GenreViewModel> GenresData { get; set; }

        [DisplayName("Genre")]
        public List<int> GenresInput { get; set; }

        public List<PlatformTypeViewModel> PlatformTypesData { get; set; }

        [DisplayName("Platform")]
        public List<int> PlatformTypesInput { get; set; }

        public List<PublisherViewModel> PublishersData { get; set; }

        [DisplayName("Publisher")]
        public List<int> PublishersInput { get; set; }

        [DisplayName("Sort by")]
        public SortOptions SortData { get; set; }

        [DisplayName("Minimum price")]
        public decimal MinPrice { get; set; }

        [DisplayName("Maximum price")]
        public decimal MaxPrice { get; set; }

        [DisplayName("Published date")]
        public DateOptions PublishedDate { get; set; }

        [DisplayName("Name")]
        public string GameName { get; set; }
    }
}