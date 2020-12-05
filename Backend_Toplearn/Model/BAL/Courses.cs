using System;

namespace Backend_Toplearn.Model.BAL
{
    public class Courses
    {
        public class Fields : Field_List
        {

        }
        public class Select
        {
            public int CourseId { get; set; }
            public string Title { get; set; }
            public string ImageUrl { get; set; }
            public decimal Price { get; set; }
            public string Descriptions { get; set; }
            public string CourseTime { get; set; }
            public bool IsActive { get; set; }
            public DateTime InsertDate { get; set; }
            public TimeSpan InsertTime { get; set; }
        }
    }
}