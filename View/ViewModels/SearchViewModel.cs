﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Enums;

namespace View.ViewModels
{
    public class SearchViewModel
    {
        [DisplayName("Sort by:")]
        public SortBy SortBy { get; set; }

        [DisplayName("Genre:")]
        public int GenreId { get; set; }

        [DisplayName("Released after:")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleasedAfter { get; set; } = new DateTime(1887, 01, 01);

        [DisplayName("Released before:")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleasedBefore { get; set; } = DateTime.MaxValue;

        [DisplayName("Search")]
        public string SearchTerm { get; set; }

        public List<GenreViewModel> AllGenres { get; set; } = new List<GenreViewModel>();

        public SearchViewModel()
        {

        }
    }
}
